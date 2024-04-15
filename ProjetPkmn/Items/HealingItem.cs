using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    public class HealingItem : IItem
    {

        public int Heal { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public bool Revive { get; set; }

        public HealingItem(string _name, int _cost, int _heal, bool _revive)
        {
            Name = _name;
            Cost = _cost;
            Heal = _heal;
            Revive = _revive;

        }

        public bool Use(Pokemon target)
        {
            bool isOk = target.Heal(Heal, Revive, false);
            return isOk;

        }
        public void Buy(Trainer trainer)
        {
            if (trainer.Pokedollars - Cost >= 0)
            {
                trainer.HealingItems.Add(this);
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
