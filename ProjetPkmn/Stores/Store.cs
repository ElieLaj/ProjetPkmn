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
        public static void PokeStore(List<IItem> items, Trainer trainer)
        {
            object item = "";
            while (true)
            {
                Console.WriteLine("Merchand: What do you want to buy traveler ?");
                item = Input.Item(items, trainer.Pokedollars);
                if (item is string)
                {
                    break;
                }
                else
                {
                    IItem _item = (IItem) item;
                    _item.Buy(trainer);
                }
            }
        }
    }
}
