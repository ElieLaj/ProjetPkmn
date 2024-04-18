using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.TilesType
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
