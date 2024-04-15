using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Items;
using ProjetPkmn.Mons;
using ProjetPkmn.Game;
using ProjetPkmn.Map;

namespace ProjetPkmn.Trainers
{
    public class TrainerNPC : Trainer
    {
        public int SightRange { get; set; }
        public TrainerNPC(string _name, int _pokedollars, List<Pokemon> _pokemons, List<IItem> _items, List<IItem> _captureItems, Tile _sprite, int _x, int _y, int _sightRange)
            : base(_name, _pokedollars, _pokemons, _items, _captureItems, _sprite)
        {
            X = _x;
            Y = _y;
            SightRange = _sightRange;
        }

        public bool BattleOnSight(Trainer player)
        {
            if(player.X  == X && player.Y  - Y <= SightRange)
            {
               return true;
            }
            else return false;
        }
    }
}
