using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Items
{
    public interface IItem
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("cost")]
        public int Cost { get; }
        bool Use(Pokemon target);
        void Buy(Trainer user);
    }
}