using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPkmn.Assets.Mons
{
    public class Move
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Damage { get; set; }

        public Move(string _name, string _type, int _damage)
        {
            Name = _name;
            Type = _type;
            Damage = _damage;
        }
    }

}
