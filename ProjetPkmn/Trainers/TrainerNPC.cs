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
        public string Text { get; set; }

        public TrainerNPC(string _name, int _pokedollars, List<Pokemon> _pokemons, List<IItem> _items, List<IItem> _captureItems, string _text, TrainerTile _sprite, PlayerPosition _position, int _sightRange)
            : base(_name, _pokedollars, _pokemons, _items, _captureItems, _sprite)
        {
            Position = _position;
            SightRange = _sightRange;
            Text = _text;
        }

        public bool BattleOnSight(Trainer player)
        {
            if(player.Position.X  == Position.X && Position.Y - player.Position.Y <= SightRange)
            {
               return true;
            }
            else return false;
        }

        public void SayLine()
        {
            Console.WriteLine(Name + ": " + Text);
        }
    }
}
