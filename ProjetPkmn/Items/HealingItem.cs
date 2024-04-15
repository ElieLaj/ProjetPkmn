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


        public HealingItem(string _name, int _cost, int _heal)
        {
            Name = _name;
            Cost = _cost;
            Heal = _heal;
        }

        public Pokemon Use(Pokemon target)
        {
            target.Heal(Heal);
            return null;

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
