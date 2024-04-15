using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    public interface IItem
    {
        string Name { get; }
        int Cost { get; }
        Pokemon Use(Pokemon target);
        void Buy(Trainer user);
    }
}