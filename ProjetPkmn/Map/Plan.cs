using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Game;
using ProjetPkmn.Mons;
using ProjetPkmn.Inputs;
using ProjetPkmn.Trainers;

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

        public Plan(int _x, int _y, Tile[,] _tiles, List<TrainerNPC> _trainerTiles, Trainer _player, List<Pokemon> _encounters)
        {
            X = _x;
            Y = _y;
            Tiles = _tiles;
            Player = _player;
            Encounters = _encounters;
            TrainerTiles = _trainerTiles;
            foreach (TrainerNPC trainerNPC in TrainerTiles)
            {
                Tiles[trainerNPC.X, trainerNPC.Y] = trainerNPC.Sprite;
            }
        }

        public void Display()
        {
            if(Player.X == 0 && Player.Y == 0)
            {
                Player.X = (int)(X / 2);
                Player.Y = (int)(Y / 2);
            }
            bool moved = false;
            while (true)
            {
                Console.Clear();
                moved = false;

                drawTiles();

                foreach (TrainerNPC npc in TrainerTiles)
                {
                    npc.BattleOnSight(Player);
                }


                if (Console.ReadKey().Key == ConsoleKey.UpArrow && Player.X > 0 && Tiles[Player.X - 1, Player.Y].isWalkable())
                {
                    Player.X -= 1;
                    moved = true;
                }
                else if (Console.ReadKey().Key == ConsoleKey.DownArrow && Player.X < X - 1 && Tiles[Player.X + 1, Player.Y].isWalkable())
                {
                    Player.X += 1;
                    moved = true;

                }
                else if (Console.ReadKey().Key == ConsoleKey.LeftArrow && Player.Y > 0 && Tiles[Player.X , Player.Y - 1].isWalkable())
                {
                    Player.Y -= 1;
                    moved = true;

                }
                else if (Console.ReadKey().Key == ConsoleKey.RightArrow && Player.Y < Y - 1 && Tiles[Player.X, Player.Y + 1].isWalkable())
                {
                    Player.Y += 1;
                    moved = true;

                }
                else if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }

                if (Tiles[Player.X, Player.Y].encounter() && moved)
                {
                    Battle.Fight(Player, Encounters[new Random().Next(0, Encounters.Count - 1)]);
                    foreach (Pokemon wild in Encounters)
                    {
                        wild.Heal(wild.MaxHealth, true, true);
                    }
                }

                
            }
        }
        private void drawTiles()
        {
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
