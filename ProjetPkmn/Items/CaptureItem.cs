using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    public class CaptureItem : Item
    {
        public int Rate { get; set; }


        public CaptureItem(string _name, int _cost, int _rate)
        {
            Name = _name;
            Cost = _cost;
            Rate = _rate;

        }

        public override Pokemon Use(Pokemon target)
        {
            if ((1 - 2 / 3 * (target.Health / target.MaxHealth)) * 200 * Rate >= 160)
            {
                return target;
            }
            else
            {
                return null;
            }
        }
    }
}
