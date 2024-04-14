using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Items;
using ProjetPkmn.Mons;
using ProjetPkmn.Inputs;

namespace ProjetPkmn
{
    public class Trainer
    {
        public string Name { get; set; }
        public int Pokedollars { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        public List<HealingItem> HealingItems { get; set; }
        public List<CaptureItem> CaptureItems { get; set; }

        public Trainer(string _name, int _pokedollars, List<Pokemon> _pokemons, List<HealingItem> _items, List<CaptureItem> _captureItems)
        {
            Name = _name;
            Pokedollars = _pokedollars;
            Pokemons = _pokemons;
            HealingItems = _items;
        }

        public void UseItem(ref int input)
        {
            object item = Input.Item(HealingItems);
            if (item is HealingItem)
            {
                HealingItem usedItem = (HealingItem)item;
                object pkmn = Input.Pokemon(Pokemons);
                if (pkmn is Pokemon)
                {
                    Pokemon pokemon = (Pokemon)pkmn;
                    usedItem.Use(pokemon);

                    HealingItems.Remove(usedItem);
                    Console.WriteLine("You used " + usedItem.Name + " on " + pokemon.Name + " !");
                    Console.WriteLine("He gained: " + usedItem.Heal + " health");
                }
                else
                {
                    input = 4;
                    return;
                }
            }
            else
            {
                input = 4;
                return;
            }
        }
        public bool AttemptEscape(Pokemon opponent)
        {
            int bonus = Pokemons[0].Speed - opponent.Speed;
            if (new Random().Next(0, 100) > (50 - bonus))
            {
                Console.WriteLine("You managed to run away!");
                return true;
            }
            else
            {
                Console.WriteLine("You couldn't run away!");
                return false;
            }
        }
        public void SwapPokemon(Pokemon pkmn1, ref int input)
        {
            if (Pokemons.Count > 1)
            {
                object pkmn = "";
                do
                {
                    pkmn = Input.Pokemon(Pokemons);
                    if (pkmn == pkmn1)
                    {
                        Console.WriteLine("You can't swap the same pokemon you've choosen");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                    }

                } while (pkmn1 == pkmn);

                if (pkmn is Pokemon chosenPkmn)
                {
                    int newPokemon = Pokemons.IndexOf(chosenPkmn);
                    int oldPokemon = Pokemons.IndexOf(pkmn1);
                    //Swap(Pokemons, index, index2);

                    List<Pokemon> tmpPokemons = Pokemons;
                    Pokemon tmpNewPokemon = Pokemons[newPokemon];
                    tmpPokemons[newPokemon] = tmpPokemons[oldPokemon];
                    tmpPokemons[oldPokemon] = tmpNewPokemon;
                    Pokemons = tmpPokemons;

                    Console.WriteLine($"{chosenPkmn.Name} is now the {oldPokemon + 1}th member of your team");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                }
                else
                {
                    input = 4;
                    return;
                }
            }
            else
            {
                input = 4;
                return;
            }
        }
    }
}
