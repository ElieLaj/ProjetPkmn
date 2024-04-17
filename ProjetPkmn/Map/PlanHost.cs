using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Game;
using ProjetPkmn.Mons;
using ProjetPkmn.Inputs;
using ProjetPkmn.Trainers;
using System.Numerics;
using ProjetPkmn.TCP;

namespace ProjetPkmn.Map
{
    public class PlanHost : Plan
    {

        public Server Host { get; set; }
        public Trainer Player2 { get; set; }
        public bool moved2 { get; set; }
        public bool moved { get; set; }
        public PlayerPosition P2 { get; set; }


        public PlanHost(int _x, int _y, Tile[,] _tiles, List<TrainerNPC> _trainerTiles, List<TeleportationPoint> _tpList, Trainer _player, Trainer _player2, List<Pokemon> _encounters, Server _host)
        :base(_x, _y, _tiles, _trainerTiles, _tpList, _player, _encounters)
        {
            X = _x;
            Y = _y;
            Tiles = _tiles;
            Player = _player;
            Player2 = _player2;
            P2 = new PlayerPosition { X = X/2, Y = Y/2 };
            Encounters = _encounters;
            TrainerTiles = _trainerTiles;
            TeleportationPoints = _tpList;
            Host = _host;
            moved = false;
            moved2 = false;
            DeletedTiles = new List<Dictionary<TrainerNPC, Tile>>();

            foreach (TrainerNPC trainerNPC in TrainerTiles)
            {
                DeletedTiles.Add(new Dictionary<TrainerNPC, Tile>{ {trainerNPC, Tiles[trainerNPC.X, trainerNPC.Y] } });
                Tiles[trainerNPC.X, trainerNPC.Y] = trainerNPC.Sprite;
            }
            if (TeleportationPoints.Count > 0)
            {
                foreach (TeleportationPoint tp in TeleportationPoints)
                {
                    Tiles[tp.X, tp.Y] = tp.Sprite;
                }
            }

    }

    override public void AddTeleportation(TeleportationPoint tp) 
        {
            TeleportationPoints.Add(tp);
            Tiles[tp.X, tp.Y] = tp.Sprite;

        }

        override public void Display()
        {
            if(Player.X == 0 && Player.Y == 0)
            {
                Player.X = (int)(X / 2);
                Player.Y = (int)(Y / 2);
            }
            if (Player2.X == 0 && Player2.Y == 0)
            {
                Player2.X = (int)(X / 2);
                Player2.Y = (int)(Y / 2);
            }
            drawTiles();
            Thread inputThread = new Thread(() => lookForInputs());
            inputThread.Start();


            Thread clientThread = new Thread(() => P2 = Host.ListenInput());

            int currentNPC = 0;
            Game(currentNPC, inputThread, clientThread);
            drawTiles();

        }

        public void Game(int currentNPC, Thread inputThread, Thread clientThread)
        {

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
                            Player2.X = P2.X;
                            Player2.Y = P2.Y;
                            moved2 = true;
                            Host.SendMovementToAllClients(Player.X, Player.Y, Player2.X, Player2.Y);
                        }
                    }
                    P2 = null;
                }
                if (moved || moved2){
                    drawTiles();
                    moved2 = false;
                    moved = false;
                    if (Tiles[Player.X, Player.Y].Encounter())
                    {
                        Battle.Fight(Player, new List<Pokemon> { Encounters[new Random().Next(0, Encounters.Count - 1)] }, false);
                        foreach (Pokemon wild in Encounters)
                        {
                            wild.Heal(wild.MaxHealth, true, true);
                        }
                    }

                    foreach (TrainerNPC npc in TrainerTiles)
                    {

                        if (npc.BattleOnSight(Player))
                        {
                            npc.SayLine();

                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            bool isWon = Battle.Fight(Player, npc.Pokemons, true);
                            if (isWon)
                            {
                                Tiles[npc.X, npc.Y] = DeletedTiles[currentNPC][npc];
                                DeletedTiles.Remove(DeletedTiles[currentNPC]);
                                TrainerTiles.Remove(npc);
                                break;
                            }
                            else
                            {
                                Player.X = (int)(X / 2);
                                Player.Y = (int)(Y / 2);
                            }
                        }
                        else
                        {
                            currentNPC++;
                        }
                    }

                    foreach (TeleportationPoint tp in TeleportationPoints)
                    {
                        if (tp.X == Player.X && tp.Y == Player.Y)
                        {
                            tp.Teleport(Player);
                        }
                    }
                }
                System.Threading.Thread.Sleep(50);
            }
            inputThread.Interrupt();

        }
        override public void drawTiles()
        {
            Console.Clear();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if ((Player.X == i && Player.Y == j) || (Player2.X == i && Player2.Y == j))
                    {
                        if ((Player.X == i && Player.Y == j) && (Player2.X == i && Player2.Y == j))
                        {
                            Console.Write(Player.Sprite.C + "" + Player2.Sprite.C + " ");
                        }
                        else if (Player2.X == i && Player2.Y == j)
                        {
                            Console.Write(Player2.Sprite.C + " ");
                        }
                        else if (Player.X == i && Player.Y == j)
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
                    if (key == ConsoleKey.UpArrow && Player.X > 0 && Tiles[Player.X - 1, Player.Y].IsWalkable())
                    {
                        Player.X -= 1;
                        moved = true;
                    Console.WriteLine(moved);
                        Host.SendMovementToAllClients(Player.X, Player.Y, Player2.X, Player2.Y);
                    }
                    else if (key == ConsoleKey.DownArrow && Player.X < X - 1 && Tiles[Player.X + 1, Player.Y].IsWalkable())
                    {

                        Player.X += 1;
                        moved = true;
                        Host.SendMovementToAllClients(Player.X, Player.Y, Player2.X, Player2.Y);
                    }
                    else if (key == ConsoleKey.LeftArrow && Player.Y > 0 && Tiles[Player.X, Player.Y - 1].IsWalkable())
                    {
                        Player.Y -= 1;
                        moved = true;
                        Host.SendMovementToAllClients(Player.X, Player.Y, Player2.X, Player2.Y);
                    }
                    else if (key == ConsoleKey.RightArrow && Player.Y < Y - 1 && Tiles[Player.X, Player.Y + 1].IsWalkable())
                    {
                        Player.Y += 1;
                        moved = true;
                        Host.SendMovementToAllClients(Player.X, Player.Y, Player2.X, Player2.Y);
                    }
                }
            
        }
    }
}
