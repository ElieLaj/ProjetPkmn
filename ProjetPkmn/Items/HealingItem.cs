using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    public class HealingItem : Item
    {

        public int Heal { get; set; }


        public HealingItem(string _name, int _cost, int _heal)
        {
            Name = _name;
            Cost = _cost;
            Heal = _heal;
        }

        public override Pokemon Use(Pokemon target)
        {
            target.Heal(Heal);
            return null;

        }
    }
}
