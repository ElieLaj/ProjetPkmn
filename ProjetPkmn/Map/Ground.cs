using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.Map
{
    internal class Ground : Tile
    {


        public Ground(string _c)
        {
            Z = 0; 
            C = _c;

        }
        public override void Effect(Trainer player, params object[] parameters)
        {
            
        }
    }
}
