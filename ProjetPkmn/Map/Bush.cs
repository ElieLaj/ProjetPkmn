using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Map;
using ProjetPkmn.Mons;
using ProjetPkmn.Trainers;
using ProjetPkmn.Game;

namespace ProjetPkmn.Map
{
    internal class Bush : Tile
    {
        public Bush(string _c, int _spawnRate)
        {
            C = _c;
            SpawnRate = _spawnRate;
            Z = 0;
        }
        public override void Effect(Trainer player, params object[] parameters)
        {
            if(new Random().Next(0, 100) > SpawnRate) {
                if (parameters.Length == 1 && parameters[0] is List<Pokemon>)
                {
                    List<Pokemon> pokemon = (List<Pokemon>)parameters[0];
                    Battle.Fight(player, new List<Pokemon> { pokemon[new Random().Next(0, pokemon.Count - 1)] }, false);
                }
                else
                {
                    throw new ArgumentException("Les paramètres ne correspondent pas à l'action Bush.");
                }
            }
        }
    }
}
