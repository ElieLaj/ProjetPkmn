using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Inputs;
using ProjetPkmn.Items;

namespace ProjetPkmn.Stores
{
    internal class Store
    {
        public static void PokeStore(List<Item> items, Trainer trainer)
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
                else
                {
                    if(item is HealingItem)
                    {
                        HealingItem _item = (HealingItem)item;
                        if (trainer.Pokedollars - _item.Cost >= 0)
                        {
                            trainer.HealingItems.Add(_item);
                            trainer.Pokedollars -= _item.Cost;
                            Console.WriteLine("You chose " + _item.Name);
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money for " + _item.Name);
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                    }
                    else if (item is CaptureItem)
                    {
                        CaptureItem _item = (CaptureItem)item;
                        if (trainer.Pokedollars - _item.Cost >= 0)
                        {
                            trainer.CaptureItems.Add(_item);
                            trainer.Pokedollars -= _item.Cost;
                            Console.WriteLine("You chose " + _item.Name);
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money for " + _item.Name);
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                    }
                }
            }
        }
    }
}
