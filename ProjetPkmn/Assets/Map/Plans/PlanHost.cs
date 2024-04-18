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
    public class PlanHost : Plan
    {

        public Server Host { get; set; }
        public Trainer Player2 { get; set; }
        public bool moved2 { get; set; }
        public bool moved { get; set; }
        public PlayerPosition P2 { get; set; }


        public PlanHost(int _x, int _y, Tile[,] _tiles, List<TrainerNPC> _trainerTiles, List<TeleportationPoint> _tpList, Trainer _player, Trainer _player2, List<Pokemon> _encounters, Server _host)
        : base(_x, _y, _tiles, _trainerTiles, _tpList, _player, _encounters)
        {
            X = _x;
            Y = _y;
            Tiles = _tiles;
            Player = _player;
            Player2 = _player2;
            P2 = new PlayerPosition { X = X / 2, Y = Y / 2 };
            Encounters = _encounters;
            TrainerTiles = _trainerTiles;
            TeleportationPoints = _tpList;
            Host = _host;
            moved = false;
            moved2 = false;
            DeletedTiles = new List<Dictionary<TrainerNPC, Tile>>();

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
                Player.Position.X = X / 2;
                Player.Position.Y = Y / 2;
            }
            if (Player2.Position.X == 0 && Player2.Position.Y == 0)
            {
                Player2.Position.X = X / 2;
                Player2.Position.Y = Y / 2;
            }
            drawTiles();
            Thread inputThread = new Thread(() => lookForInputs());
            inputThread.Start();


            Thread clientThread = new Thread(() => P2 = Host.ListenInput());

            int currentNPC = 0;

            while (true)
            {
                currentNPC = 0;

                if (!clientThread.IsAlive)
                {
                    clientThread = new Thread(() => P2 = Host.ListenInput());
                    clientThread.Start();
                }
                if (!inputThread.IsAlive)
                {
                    inputThread = new Thread(() => lookForInputs());
                    inputThread.Start();
                }

                if (P2 != null)
                {
                    if (P2.X <= X - 1 && P2.X >= 0 && P2.Y <= Y - 1 && P2.Y >= 0)
                    {
                        //Console.WriteLine("P2.X: " + P2.X + " | " + P2.Y);
                        if (Tiles[P2.X, P2.Y].IsWalkable())
                        {
                            Player2.Position.X = P2.X;
                            Player2.Position.Y = P2.Y;
                            moved2 = true;
                            Host.SendMovementToAllClients(Player.Position.X, Player.Position.Y, Player2.Position.X, Player2.Position.Y);
                        }
                    }
                    P2 = null;
                }
                if (moved || moved2)
                {
                    drawTiles();
                    moved2 = false;
                    moved = false;

                    Tiles[Player.Position.X, Player.Position.Y].Effect(Player, Encounters);

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
                Thread.Sleep(50);
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
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && Player.Position.X > 0 && Tiles[Player.Position.X - 1, Player.Position.Y].IsWalkable())
                {
                    Player.Position.X -= 1;
                    moved = true;
                    Console.WriteLine(moved);
                    Host.SendMovementToAllClients(Player.Position.X, Player.Position.Y, Player2.Position.X, Player2.Position.Y);
                }
                else if (key == ConsoleKey.DownArrow && Player.Position.X < X - 1 && Tiles[Player.Position.X + 1, Player.Position.Y].IsWalkable())
                {

                    Player.Position.X += 1;
                    moved = true;
                    Host.SendMovementToAllClients(Player.Position.X, Player.Position.Y, Player2.Position.X, Player2.Position.Y);
                }
                else if (key == ConsoleKey.LeftArrow && Player.Position.Y > 0 && Tiles[Player.Position.X, Player.Position.Y - 1].IsWalkable())
                {
                    Player.Position.Y -= 1;
                    moved = true;
                    Host.SendMovementToAllClients(Player.Position.X, Player.Position.Y, Player2.Position.X, Player2.Position.Y);
                }
                else if (key == ConsoleKey.RightArrow && Player.Position.Y < Y - 1 && Tiles[Player.Position.X, Player.Position.Y + 1].IsWalkable())
                {
                    Player.Position.Y += 1;
                    moved = true;
                    Host.SendMovementToAllClients(Player.Position.X, Player.Position.Y, Player2.Position.X, Player2.Position.Y);
                }
            }

        }
    }
}
