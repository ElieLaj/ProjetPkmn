using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Game;
using ProjetPkmn.Tools.Inputs;
using System.Numerics;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Map.TilesType;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Tools.TCP;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.Plans
{
    public class PlanClient : Plan
    {
        public bool moved2 { get; set; }
        public bool moved { get; set; }
        public Client Hosted { get; set; }
        public Trainer Player2 { get; set; }
        public PlayerPositions Positions { get; set; }
        public PlanClient(int _x, int _y, Tile[,] _tiles, List<TrainerNPC> _trainerTiles, List<TeleportationPoint> _tpList, Trainer _player, Trainer _player2, List<Pokemon> _encounters, Client _hosted)
        : base(_x, _y, _tiles, _trainerTiles, _tpList, _player, _encounters)
        {
            X = _x;
            Y = _y;
            Tiles = _tiles;
            Player = _player;
            Player2 = _player2;
            Encounters = _encounters;
            TrainerTiles = _trainerTiles;
            TeleportationPoints = _tpList;
            Hosted = _hosted;
            moved = false;
            moved2 = false;
            DeletedTiles = new List<Dictionary<TrainerNPC, Tile>>();
            Positions = new PlayerPositions
            {
                Player1 = new PlayerPosition { X = Player.Position.X, Y = Player.Position.Y },
                Player2 = new PlayerPosition { X = Player2.Position.X, Y = Player.Position.Y }
            };
            foreach (TrainerNPC trainerNPC in TrainerTiles)
            {
                DeletedTiles.Add(new Dictionary<TrainerNPC, Tile> { { trainerNPC, Tiles[trainerNPC.Position.X, trainerNPC.Position.Y] } });
                Tiles[trainerNPC.Position.X, trainerNPC.Position.Y] = trainerNPC.Sprite;
            }

        }


        override public void Display()
        {
            if (Player.Position.X == 0 && Player.Position.Y == 0)
            {
                Positions.Player1.X = X / 2;
                Positions.Player1.Y = Y / 2;
            }
            if (Player2.Position.X == 0 && Player2.Position.Y == 0)
            {
                Positions.Player2.X = X / 2;
                Positions.Player2.Y = Y / 2;
            }
            drawTiles();
            Thread inputThread = new Thread(() => lookForInputs());

            Thread hostThread = new Thread(() => Positions = Hosted.ReceiveMovements(
                    new PlayerPositions
                    {
                        Player1 = new PlayerPosition { X = Player.Position.X, Y = Player.Position.Y },
                        Player2 = new PlayerPosition { X = Player2.Position.X, Y = Player.Position.Y }
                    }
                ));
            hostThread.Start();


            int currentNPC = 0;

            while (true)
            {

                moved = false;
                moved2 = false;
                currentNPC = 0;

                if (!hostThread.IsAlive)
                {
                    hostThread = new Thread(() => Positions = Hosted.ReceiveMovements(
                    new PlayerPositions
                    {
                        Player1 = new PlayerPosition { X = Player.Position.X, Y = Player.Position.Y },
                        Player2 = new PlayerPosition { X = Player2.Position.X, Y = Player2.Position.Y }
                    }

                ));

                    hostThread.Start();
                }
                if (!inputThread.IsAlive)
                {
                    inputThread = new Thread(() => lookForInputs());
                    inputThread.Start();
                }



                int prevPlayer1X = Player.Position.X;
                int prevPlayer1Y = Player.Position.Y;
                int prevPlayer2X = Player2.Position.X;
                int prevPlayer2Y = Player2.Position.Y;

                Player.Position.X = Positions.Player1.X;
                Player.Position.Y = Positions.Player1.Y;
                Player2.Position.X = Positions.Player2.X;
                Player2.Position.Y = Positions.Player2.Y;

                if (Player.Position.X != prevPlayer1X || Player.Position.Y != prevPlayer1Y || Player2.Position.X != prevPlayer2X || Player2.Position.Y != prevPlayer2Y)
                {
                    moved = true;
                }

                if (moved || moved2)
                {
                    drawTiles();

                    Tiles[Player2.Position.X, Player2.Position.Y].Effect(Player2, Encounters);

                    foreach (TrainerNPC npc in TrainerTiles)
                    {

                        if (npc.BattleOnSight(Player))
                        {
                            npc.SayLine();

                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            bool isWon = Battle.Fight(Player, npc.Pokemons, true);
                            if (isWon)
                            {
                                Tiles[npc.Position.X, npc.Position.Y] = DeletedTiles[currentNPC][npc];
                                DeletedTiles.Remove(DeletedTiles[currentNPC]);
                                TrainerTiles.Remove(npc);
                                break;
                            }
                            else
                            {
                                Player.Position.X = X / 2;
                                Player.Position.Y = Y / 2;
                            }
                        }
                        else
                        {
                            currentNPC++;
                        }
                    }
                    drawTiles();

                }
                Thread.Sleep(100);

            }
        }
        override public void drawTiles()
        {
            Console.Clear();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (Player.Position.X == i && Player.Position.Y == j || Player2.Position.X == i && Player2.Position.Y == j)
                    {
                        if (Player.Position.X == i && Player.Position.Y == j && Player2.Position.X == i && Player2.Position.Y == j)
                        {
                            Console.Write(Player.Sprite.C + "" + Player2.Sprite.C + " ");
                        }
                        else if (Player2.Position.X == i && Player2.Position.Y == j)
                        {
                            Console.Write(Player2.Sprite.C + " ");
                        }
                        else if (Player.Position.X == i && Player.Position.Y == j)
                        {
                            Console.Write(Player.Sprite.C + " ");
                        }
                    }
                    else
                    {
                        Console.Write(Tiles[i, j].C + " ");

                    }
                }
                Console.Write("\n");
            }

        }
        public void lookForInputs()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                var key = keyInfo.Key;
                if (key == ConsoleKey.UpArrow)
                {
                    Hosted.SendMovement(new PlayerPosition { X = Player2.Position.X - 1, Y = Player2.Position.Y });
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    Hosted.SendMovement(new PlayerPosition { X = Player2.Position.X + 1, Y = Player2.Position.Y });
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    Hosted.SendMovement(new PlayerPosition { X = Player2.Position.X, Y = Player2.Position.Y - 1 });
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    Hosted.SendMovement(new PlayerPosition { X = Player2.Position.X, Y = Player2.Position.Y + 1 });
                }
            }
            Thread.Sleep(100);


        }
    }
}
