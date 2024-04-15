using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;

namespace ProjetPkmn.Items
{
    internal class ItemPokemon : Pokemon, IItem
    {
        public int Cost { get; set; }

        public ItemPokemon(int _cost, string _name, List<string> _type, int _attack, int _defense, int _speed, int _health, int _level, int _baseExp, Dictionary<int, Move> _learnset)
            : base(_name, _type, _attack, _defense, _speed, _health, _level, _baseExp, _learnset)
        {
            Cost = _cost;
        }

        public Pokemon Use(Pokemon target)
        {
            return this;
        }
    }
}
