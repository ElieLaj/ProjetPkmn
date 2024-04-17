﻿using System;
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
    public class PlanClient : Plan
    {
        public bool moved2 { get; set; }
        public bool moved { get; set; }
        public Client Hosted { get; set; }
        public Trainer Player2 { get; set; }
        public PlayerPositions Positions { get; set; }
        public PlanClient(int _x, int _y, Tile[,] _tiles, List<TrainerNPC> _trainerTiles, List<TeleportationPoint> _tpList, Trainer _player, Trainer _player2, List<Pokemon> _encounters, Client _hosted)
        :base(_x, _y, _tiles, _trainerTiles, _tpList, _player, _encounters)
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
                Player1 = new PlayerPosition { X = Player.X, Y = Player.Y },
                Player2 = new PlayerPosition { X = Player2.X, Y = Player.Y }
            };
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
                Positions.Player1.X = (int)(X / 2);
                Positions.Player1.Y = (int)(Y / 2);
            }
            if (Player2.X == 0 && Player2.Y == 0)
            {
                Positions.Player2.X = (int)(X / 2);
                Positions.Player2.Y = (int)(Y / 2);
            }
            drawTiles();
            Thread inputThread = new Thread(() => lookForInputs());
            inputThread.Start();

            Thread hostThread = new Thread(() => Positions = Hosted.ReceiveMovements(
                    new PlayerPositions
                    {
                        Player1 = new PlayerPosition { X = Player.X, Y = Player.Y },
                        Player2 = new PlayerPosition { X = Player2.X, Y = Player.Y }
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
                        Player1 = new PlayerPosition { X = Player.X, Y = Player.Y },
                        Player2 = new PlayerPosition { X = Player2.X, Y = Player.Y }
                    }
                    
                ));
                    
                    hostThread.Start();
                }

                int prevPlayer1X = Player.X;
                int prevPlayer1Y = Player.Y;
                int prevPlayer2X = Player2.X;
                int prevPlayer2Y = Player2.Y;

                Player.X = Positions.Player1.X;
                Player.Y = Positions.Player1.Y;
                Player2.X = Positions.Player2.X;
                Player2.Y = Positions.Player2.Y;

                if (Player.X != prevPlayer1X || Player.Y != prevPlayer1Y || Player2.X != prevPlayer2X || Player2.Y != prevPlayer2Y)
                {
                    moved = true; 
                }


                Player.X = Positions.Player1.X;
                Player.Y = Positions.Player1.Y;
                Player2.X = Positions.Player2.X;
                Player2.Y = Positions.Player2.Y;


                if (moved || moved2){
                    drawTiles();

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
                System.Threading.Thread.Sleep(100);

            }
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
                        if((Player.X == i && Player.Y == j) && (Player2.X == i && Player2.Y == j))
                        {
                            Console.Write(Player.Sprite.C +""+Player2.Sprite.C+" ");
                        }
                        else if (Player2.X == i && Player2.Y == j)
                        {
                            Console.Write(Player2.Sprite.C+ " ");
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
            while(true){
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    var key = keyInfo.Key;
                    if (key == ConsoleKey.UpArrow)
                    {
                        Hosted.SendMovement(new PlayerPosition { X = Player2.X - 1, Y = Player2.Y });
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        Hosted.SendMovement(new PlayerPosition { X = Player2.X + 1, Y = Player2.Y });
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        Hosted.SendMovement(new PlayerPosition { X = Player2.X, Y = Player2.Y - 1 });
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        Hosted.SendMovement(new PlayerPosition { X = Player2.X, Y = Player2.Y + 1 });
                    }
                }
                System.Threading.Thread.Sleep(100);

            }
        }
    }
}
