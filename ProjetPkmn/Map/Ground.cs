using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPkmn.Map
{
    internal class Ground : Tile
    {


        public Ground(string _c) :base( _c )
        {
           
            Z = 0;
            C = _c;

        }
       
    }
}
