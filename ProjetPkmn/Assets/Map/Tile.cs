using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetPkmn.Assets.Trainers;
using ProjetPkmn.Assets.Mons;

namespace ProjetPkmn.Assets.Map
{
    abstract public class Tile
    {
        [JsonProperty("c")]
        public string C { get; set; }
        [JsonProperty("z")]
        public int Z { get; set; }
        [JsonProperty("spawnRate")]
        public int SpawnRate { get; set; }


       
        abstract public void Effect(Trainer player, params object[] parameters);
       
        public bool IsWalkable()
        {
            if (Z == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}