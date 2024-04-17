using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.Map
{
    public class TeleportationPoint : Tile
    {
        public int DestinationX { get; set; }
        public int DestinationY { get; set; }
        //public int X { get; set; }
        //public int Y { get; set; }
        public Plan DestinationMap { get; set; }
       // public Tile Sprite { get; set; }


        public TeleportationPoint(string _c, int _destX, int _destY, Plan _destMap)
        {
            //X = _x;
            //Y = _y;
            C = _c;
            DestinationX = _destX;
            DestinationY = _destY;
            DestinationMap = _destMap;
            //Sprite =_sprite;
        }
        /*
        public void Teleport(Trainer player)
        {
            player.Position.X = DestinationX;
            player.Position.Y = DestinationY;
            DestinationMap.Display();
        }*/
        override public void Effect(Trainer player, params object[] parameters)
        {
            player.Position.X = DestinationX;
            player.Position.Y = DestinationY;
            DestinationMap.Display();
        }
    }
}
