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

namespace ProjetPkmn.Map
{
    public class Plan
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Tile[,] Tiles { get; set; }
        public Trainer Player { get; set; }
        public List<Pokemon> Encounters { get; set; }
        public List<TrainerNPC> TrainerTiles { get; set; }
        public List<TeleportationPoint> TeleportationPoints { get; set; }
        public List<Dictionary<TrainerNPC, Tile>> DeletedTiles { get; set; }

        
        public Plan(int _x, int _y, Tile[,] _tiles, List<TrainerNPC> _trainerTiles, List<TeleportationPoint> _tpList, Trainer _player, List<Pokemon> _encounters)
        {
            X = _x;
            Y = _y;
            Tiles = _tiles;
            Player = _player;
            Encounters = _encounters;
            TrainerTiles = _trainerTiles;
            TeleportationPoints = _tpList;
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

        virtual public void AddTeleportation(TeleportationPoint tp) 
        {
            TeleportationPoints.Add(tp);
            Tiles[tp.X, tp.Y] = tp.Sprite;

        }

        virtual public void Display()
        {
            if(Player.X == 0 && Player.Y == 0)
            {
                Player.X = (int)(X / 2);
                Player.Y = (int)(Y / 2);
            }
            int currentNPC = 0;
            bool moved = false;
            while (true)
            {
                moved = false;
                currentNPC = 0;

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    moved = true;
                    if (key == ConsoleKey.UpArrow && Player.X > 0 && Tiles[Player.X - 1, Player.Y].IsWalkable())
                        Player.X -= 1;
                    else if (key == ConsoleKey.DownArrow && Player.X < X - 1 && Tiles[Player.X + 1, Player.Y].IsWalkable())
                        Player.X += 1;
                    else if (key == ConsoleKey.LeftArrow && Player.Y > 0 && Tiles[Player.X, Player.Y - 1].IsWalkable())
                        Player.Y -= 1;
                    else if (key == ConsoleKey.RightArrow && Player.Y < Y - 1 && Tiles[Player.X, Player.Y + 1].IsWalkable())
                        Player.Y += 1;
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                
                if (moved) {
                    drawTiles();

                if (Tiles[Player.X, Player.Y].Encounter() && moved)
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

            }
        }
        virtual public void drawTiles()
        {
            Console.Clear();
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (Player.X == i && Player.Y == j)
                    {
                        Console.Write(Player.Sprite.C);
                    }
                    else
                    {
                        Console.Write(Tiles[i, j].C + " ");

                    }

                }
                Console.Write("\n");
            }
            
        }
    }
}
