using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.Places.PokeCenters
{
    public class PokeCenter
    {
        public int Price { get; set; }
        PokeCenter(int _price)
        {
            Price = _price;

        }
        public void HealPokemons(Trainer user)
        {
            if (user.Pokedollars >= 900)
            {
                Console.Write("Healing in process");

                for (int i = 0; i < 3; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
                foreach (Pokemon pokemon in user.Pokemons)
                {
                    pokemon.Heal(pokemon.MaxHealth, true, true);
                }
                Console.WriteLine("\nNurse: Your pokemons are healed up and ready to go !");
                Console.WriteLine("You lost 900 Pokedollars");
                user.Pokedollars -= 900;
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }
            else
            {
                Console.WriteLine("You don't have enough money for that...");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }
        }
    }


}
