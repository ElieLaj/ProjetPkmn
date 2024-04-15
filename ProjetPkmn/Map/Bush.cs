using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Map;

namespace ProjetPkmn.Map
{
    internal class Bush : Tile
    {
        public Bush(string _c) :base(_c) 
        {
            Z = 0;
            C = _c;

        }

        override public bool encounter()
        {
            if (new Random().Next(0, 100) >= 70)
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
    }
}
