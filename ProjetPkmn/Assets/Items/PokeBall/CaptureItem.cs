﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjetPkmn.Assets.Items;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Items.PokeBall
{
    public class CaptureItem : IItem
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }
        [JsonProperty("cost")]
        public int Cost { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public CaptureItem(string _name, int _cost, double _rate)
        {
            Name = _name;
            Cost = _cost;
            Rate = _rate;
        }

        public bool Use(Pokemon target)
        {
            if ((1 - 2 / 3 * (target.Health / target.MaxHealth)) * 200 * Rate >= 160)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Buy(Trainer trainer)
        {
            if (trainer.Pokedollars - Cost >= 0)
            {
                trainer.CaptureItems.Add(this);
                trainer.Pokedollars -= Cost;
                Console.WriteLine("You chose " + Name);
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }
            else
            {
                Console.WriteLine("You don't have enough money for " + Name);
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }
        }
    }


}
