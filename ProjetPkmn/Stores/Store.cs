using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Inputs;
using ProjetPkmn.Items;

namespace ProjetPkmn.Stores
{
    internal class Store
    {
        public static void PokeStore(List<HealingItem> items, Trainer trainer)
        {
            int totalItem = trainer.HealingItems.Count;
            object item = "";
            while (true)
            {
                Console.WriteLine("Merchand: What do you want to buy traveler ?");
                item = Input.Item(items);
                if (item is string)
                {
                    break;
                }
                else if (item is HealingItem)
                {
                    HealingItem item2 = (HealingItem)item;
                    if (trainer.Pokedollars - item2.Cost >= 0)
                    {
                        trainer.HealingItems.Add(item2);
                        trainer.Pokedollars -= item2.Cost;
                        Console.WriteLine("You chose " + item2.Name);
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough money for " + item2.Name);
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }

                }


            }
        }
    }
}
