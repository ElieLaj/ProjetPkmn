using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Items;
using ProjetPkmn.Mons;
using ProjetPkmn.Inputs;
using ProjetPkmn.Map;

namespace ProjetPkmn.Trainers
{
    public class Trainer
    {
        public string Name { get; set; }
        public int Pokedollars { get; set; }
        public List<Pokemon> Pokemons { get; set; }
        public List<IItem> HealingItems { get; set; }
        public List<IItem> CaptureItems { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Tile Sprite { get; set; }

        public Trainer(string _name, int _pokedollars, List<Pokemon> _pokemons, List<IItem> _items, List<IItem> _captureItems, Tile _sprite)
        {
            Name = _name;
            Pokedollars = _pokedollars;
            Pokemons = _pokemons;
            HealingItems = _items;
            CaptureItems = _captureItems;
            X = 0;
            Y = 0;
            Sprite = _sprite;
        }

        public void ChooseItemType(ref int input, ref bool captured, Pokemon opponent, bool isTrainer)
        {
            input = Input.ItemType();

            switch (input)
            {
                case 0:
                    UseItem<HealingItem>(HealingItems, ref input, ref captured, opponent);
                    return;
                case 1:
                    if(!isTrainer)
                    {
                        UseItem<CaptureItem>(CaptureItems, ref input, ref captured, opponent);
                    }
                    else
                    {
                        Console.WriteLine("You can't catch a trainer's pokemon !");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        input = 4;
                    }
                    break;
                case 2:
                    input = 4;
                    return;
            }
        }

        public void UseItem<T>(List<IItem> ItemList, ref int input, ref bool captured, Pokemon opponent) where T : IItem
        {
            if (ItemList.Count > 0 && ItemList != null)
            {
                object item = Input.Item(ItemList, Pokedollars);
                if (item is HealingItem)
                {
                    HealingItem usedItem = (HealingItem)item;
                    object pkmn = Input.Pokemon(Pokemons);
                    if (pkmn is Pokemon)
                    {
                        Pokemon pokemon = (Pokemon)pkmn;
                        bool usable = usedItem.Use(pokemon);
                        if (usable)
                        {
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
                else if (item is CaptureItem)
                {
                    CaptureItem usedItem = (CaptureItem)item;

                    bool isCaptured = usedItem.Use(opponent);

                    CaptureItems.Remove(usedItem);

                    if (isCaptured)
                    {
                        Pokemons.Add(opponent);
                        Console.WriteLine("You've captured " + opponent.Name + " !");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                        captured = true;
                        return;
                    }
                    else
                    {
                        input = 4;
                        return;
                    }

                }

            }
            else
            {
                input = 4;
                Console.WriteLine("You have nothing in that pocket !");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                return;
            }
        }

        public bool AttemptEscape(Pokemon opponent)
        {
            int bonus = Pokemons[0].Speed - opponent.Speed;
            if (new Random().Next(0, 100) > 50 - bonus)
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
                    if(chosenPkmn.Health == 0)
                    {
                        Console.WriteLine("You can't swap to a ko pokemon");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        SwapPokemon(pkmn1, ref input);
                        return;
                    }
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
