using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Map
{
    public class Tile 
    {

        virtual public int Z { get; set; }
        public string C { get; set; }

        public Tile(string _c)
        {

            Z = 0;
            C = _c;

        }
        public bool isWalkable()
        {
            if (Z == 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        virtual public bool encounter()
        {
            return false;
        }
    }
}
