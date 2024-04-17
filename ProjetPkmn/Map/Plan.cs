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
                DeletedTiles.Add(new Dictionary<TrainerNPC, Tile>{ {trainerNPC, Tiles[trainerNPC.Position.X, trainerNPC.Position.Y] } });
                Tiles[trainerNPC.Position.X, trainerNPC.Position.Y] = trainerNPC.Sprite;
            }

            
        }


        virtual public void Display()
        {
            if(Player.Position.X == 0 && Player.Position.Y == 0)
            {
                Player.Position.X = (int)(X / 2);
                Player.Position.Y = (int)(Y / 2);
            }
            int currentNPC = 0;
            bool moved = false;
            while (true)
            {
                currentNPC = 0;

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    moved = true;
                    if (key == ConsoleKey.UpArrow && Player.Position.X > 0 && Tiles[Player.Position.X - 1, Player.Position.Y].IsWalkable()){
                        Player.Position.X -= 1;
                        moved = true;

                    }
                    else if (key == ConsoleKey.DownArrow && Player.Position.X < X - 1 && Tiles[Player.Position.X + 1, Player.Position.Y].IsWalkable()){
                        moved = true;

                        Player.Position.X += 1;
                    }
                    else if (key == ConsoleKey.LeftArrow && Player.Position.Y > 0 && Tiles[Player.Position.X, Player.Position.Y - 1].IsWalkable()){
                        moved = true;

                        Player.Position.Y -= 1;
                    }
                    else if (key == ConsoleKey.RightArrow && Player.Position.Y < Y - 1 && Tiles[Player.Position.X, Player.Position.Y + 1].IsWalkable()){
                        moved = true;

                        Player.Position.Y += 1;
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                
                if (moved) {
                    drawTiles();
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
                            Player.Position.X = (int)(X / 2);
                            Player.Position.Y = (int)(Y / 2);
                        }
                    }
                    else
                    {
                        currentNPC++;
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
                    if (Player.Position.X == i && Player.Position.Y == j)
                    {
                        Console.Write(Player.Sprite.C + " ");
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
