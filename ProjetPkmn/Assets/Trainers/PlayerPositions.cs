using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjetPkmn.Assets.Trainers
{
    public class PlayerPositions
    {
        [JsonProperty("Player1")]
        public PlayerPosition Player1 { get; set; }
        [JsonProperty("Player2")]
        public PlayerPosition Player2 { get; set; }
    }
}
