using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn.Assets.Map.TilesType
{
    public class PokeCenter : Tile
    {
        public PokeCenter(string _c)
        {
            Z = 0;
            C = _c;

        }
        public override void Effect(Trainer player, params object[] parameters)
        {
            Console.WriteLine("Do you wish to heal ? -> Press Enter to Heal -> Press Escape to Leave");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    var key = keyInfo.Key;
                    if (key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    else if (key == ConsoleKey.Enter)
                    {
                        if (player.Pokedollars >= 900)
                        {
                            Console.Write("Healing in process");

                            for (int i = 0; i < 3; i++)
                            {
                                Console.Write(".");
                                Thread.Sleep(1000);
                            }
                            foreach (Pokemon pokemon in player.Pokemons)
                            {
                                pokemon.Heal(pokemon.MaxHealth, true, true);
                            }
                            Console.WriteLine("\nNurse: Your pokemons are healed up and ready to go !");
                            Console.WriteLine("You lost 900 Pokedollars");
                            player.Pokedollars -= 900;
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money for that...");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        }
                        break;
                    }
                }
            }
        }
    }
}
