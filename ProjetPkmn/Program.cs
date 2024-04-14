using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Items;
using ProjetPkmn.Mons;
using ProjetPkmn.Inputs;
using ProjetPkmn.Game;
using ProjetPkmn.Stores;

using static System.Net.Mime.MediaTypeNames;
using static ProjetPkmn.Program;

namespace ProjetPkmn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello user, welcome to the pokemon-like");
            Console.WriteLine("Choose your username before the game starts");
            string username = Console.ReadLine();
            Console.Clear();

            Trainer user = new Trainer(username, 3000, new List<Pokemon>(), new List<Item>(), new List<Item>());

            Console.WriteLine("Welcome back " + user.Name);

            Move tackle = new Move("Tackle", "Normal", 40 );
            Move scratch = new Move("Scratch", "Normal", 40);
            Move lick = new Move("Lick", "Ghost", 20);
            Move waterGun = new Move("Water Gun", "Water", 40);
            Move rapidSpin = new Move("Rapid Spin", "Normal", 20);
            Move waterPulse = new Move("Water Pulse", "Water", 60);

            Move ember = new Move("Ember", "Fire", 40);

            Dictionary<int, Move> carapuceMoveSet =  new Dictionary<int, Move>{ { 1, lick }, { 3, scratch }, { 4, rapidSpin }, { 5, waterGun }, {6, waterPulse } } ;
            Pokemon carapuce = new Pokemon("Carapuce", ["Water"], 48, 65, 43, 44, 5, 64, carapuceMoveSet);
            Pokemon salemeche = new Pokemon("Salameche", ["Fire"], 52, 43, 65, 39, 5, 64, carapuceMoveSet);
            Pokemon bulbizarre = new Pokemon("Bulbizarre", ["Grass", "Poison"], 49, 49, 45, 45, 5, 64, carapuceMoveSet);

            HealingItem potion = new HealingItem("Potion", 300, 20);
            HealingItem superPotion = new HealingItem("Super Potion", 700, 50);

            CaptureItem pokeBall = new CaptureItem("Pokeball", 200, 1);
            CaptureItem superBall = new CaptureItem("Super Ball", 500, 1.5);

            List<Pokemon> pokedex = new List<Pokemon>();
            pokedex.Add(carapuce);
            pokedex.Add(bulbizarre);
            pokedex.Add(salemeche);

            List<Item> items = new List<Item>();
            items.Add(potion);
            items.Add(superPotion);
            items.Add(pokeBall);
            items.Add(superBall);

            while (true)
            {

                int value = Input.Menu(user);


                switch (value)
                {
                    case 0:
                        if(user.Pokemons.Count == 0)
                        {

                            object pkmn = Input.Pokemon(pokedex);

                            if (pkmn is string)
                            {
                                break;
                            }
                            else
                            {
                                Pokemon chosenPkmn = (Pokemon)pkmn;
                                user.Pokemons.Add(chosenPkmn);

                                Console.WriteLine("You chose " + chosenPkmn.Name);
                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            }
                        }
                        Console.WriteLine("May luck be on your side !");
                        Battle.Fight(user, pokedex[new Random().Next(0, pokedex.Count - 1)]);
                        break;
                    case 1:
                        Console.Clear();
                        Store.PokeStore(items, user);
                        break;   
                        case 2:
                        Console.Write("Healing in process");

                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write(".");
                            System.Threading.Thread.Sleep(1000);
                        }
                        foreach (Pokemon pokemon in user.Pokemons)
                            {
                                pokemon.Heal(pokemon.MaxHealth);
                            }
                        Console.WriteLine("\nNurse: Your pokemons are healed up and ready to go !");
                        Console.WriteLine("You lost 900 Pokedollars");
                        user.Pokedollars -= 900;
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                        break;
                       
                            case 3: Console.WriteLine("Merchand: Come again !");
                                break;
                }
                if (value == 3)
                {
                    break;
                }
            }
        } 
    }
}