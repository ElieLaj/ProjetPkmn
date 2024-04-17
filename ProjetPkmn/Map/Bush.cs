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
        public Bush(string _c, int _spawnRate) :base(_c, 0) 
        {
            SpawnRate = _spawnRate;
        }  
    }
}
