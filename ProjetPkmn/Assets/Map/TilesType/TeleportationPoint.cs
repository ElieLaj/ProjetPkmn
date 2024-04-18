using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.TilesType
{
    public class TeleportationPoint : Tile
    {
        [JsonProperty("DestinationX")]
        public int DestinationX { get; set; }
        [JsonProperty("DestinationY")]
        public int DestinationY { get; set; }

        [JsonProperty("DestinationMap")]
        public Plan DestinationMap { get; set; }


        public TeleportationPoint(string _c, int _destX, int _destY, Plan _destMap)
        {

            C = _c;
            DestinationX = _destX;
            DestinationY = _destY;
            DestinationMap = _destMap;
        }

        override public void Effect(Trainer player, params object[] parameters)
        {
            player.Position.X = DestinationX;
            player.Position.Y = DestinationY;
            DestinationMap.Display();
        }
    }
}
