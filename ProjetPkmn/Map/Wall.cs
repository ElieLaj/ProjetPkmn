using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPkmn.Map
{
    internal class Wall : Tile
    {

        override public int Z { get; set; }

        public Wall(string _c) : base(_c) 
        {

            Z = 1;
            C = _c;

        }
    }
}
