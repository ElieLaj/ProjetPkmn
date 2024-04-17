using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.Map
{
    public class Tile
    {

        public string C { get; set; }
        public int Z { get; set; }
        public int SpawnRate { get; set; }


        public Tile(string _c, int _z)
        {
            C = _c;
            Z = _z;
            SpawnRate = 0;
        }



        virtual public bool Encounter()
        {
            if (SpawnRate > 0)
            {
                if (new Random().Next(0, 100) >= 100 - SpawnRate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return false;
        }

        public bool IsWalkable()
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

    }
}