using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.Map
{
    public class TeleportationPoint
    {
        public int DestinationX { get; set; }
        public int DestinationY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Plan DestinationMap { get; set; }
        public Tile Sprite { get; set; }


        public TeleportationPoint(int _x, int _y, int _destX, int _destY, Plan _destMap, Tile _sprite)
        {
            X = _x;
            Y = _y;
            DestinationX = _destX;
            DestinationY = _destY;
            DestinationMap = _destMap;
            Sprite =_sprite;
        }

        public void Teleport(Trainer player)
        {
            player.X = DestinationX;
            player.Y = DestinationY;
            DestinationMap.Display();
        }
    }
}
