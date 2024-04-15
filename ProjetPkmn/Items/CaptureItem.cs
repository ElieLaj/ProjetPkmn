using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    public class CaptureItem : IItem
    {
        public double Rate { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }

        public CaptureItem(string _name, int _cost, double _rate)
        {
            Name = _name;
            Cost = _cost;
            Rate = _rate;

        }

        public Pokemon Use(Pokemon target)
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
