﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.Map
{
    internal class Wall : Tile
    {

        public Wall(string _c)
        {
            Z = 1;
            C = _c;

        }

        public override void Effect(Trainer player, params object[] parameters)
        {
           
        }
    }
}
