using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.TilesType
{
    public class TrainerTile : Tile
    {
        public TrainerTile(string _c, int _z)
        {
            C = _c;
            Z = _z;
        }

        public override void Effect(Trainer player, params object[] parameters)
        {

        }
    }
}
