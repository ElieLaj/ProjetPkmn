using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    public abstract class Item : IItem
    {
        public string Name { get; protected set; }
        public int Cost { get; protected set; }

        public abstract Pokemon Use(Pokemon target);
    }
}
