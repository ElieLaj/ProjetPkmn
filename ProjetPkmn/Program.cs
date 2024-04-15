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

            string username = "";

            do
            {
                Console.Clear();

                Console.WriteLine("Hello user, welcome to the pokemon-like");
                Console.WriteLine("Choose your username before the game starts");
                Console.WriteLine("Please enter a username longer than 3 characters: ");

                username = Console.ReadLine();
            } while (username.Length <= 3);


            Trainer user = new Trainer(username, 4000, new List<Pokemon>(), new List<IItem>(), new List<IItem>());

            Console.WriteLine("Welcome back " + user.Name);

            Move tackle = new Move("Tackle", "Normal", 40 );
            Move scratch = new Move("Scratch", "Normal", 40);
            Move lick = new Move("Lick", "Ghost", 20);
            Move waterGun = new Move("Water Gun", "Water", 40);
            Move rapidSpin = new Move("Rapid Spin", "Normal", 20);
            Move waterPulse = new Move("Water Pulse", "Water", 60);

            Move ember = new Move("Ember", "Fire", 40);
            Move fireFang = new Move("Fire Fang", "Fire", 65);

            Move vineWhip = new Move("Vine Whip", "Grass", 40);
            Move acid = new Move("Acid", "Poison", 50);

            Move machPunch = new Move("Mach Punch", "Fighting", 50);
            Move bite = new Move("Bite", "Dark", 60);

            Dictionary<int, Move> carapuceMoveSet =  new Dictionary<int, Move>{ { 1, lick }, { 3, scratch }, { 4, rapidSpin }, { 5, waterGun }, {6, waterPulse } } ;
            Dictionary<int, Move> salemecheMoveSet = new Dictionary<int, Move> { { 1, lick }, { 3, scratch }, { 4, rapidSpin }, { 5, ember }, { 6, fireFang } };
            Dictionary<int, Move> bulbizarreMoveSet = new Dictionary<int, Move> { { 1, lick }, { 3, tackle }, { 4, rapidSpin }, { 5, vineWhip }, { 6, acid } };

            Dictionary<int, Move> basicMoveSet = new Dictionary<int, Move> { { 1, lick }, { 3, tackle }, { 4, rapidSpin }, { 5, machPunch }, { 6, bite } };


            Pokemon carapuce = new Pokemon("Carapuce", ["Water"], 48, 65, 43, 44, 5, 64, carapuceMoveSet);
            Pokemon salemeche = new Pokemon("Salamèche", ["Fire"], 52, 43, 65, 39, 5, 64, salemecheMoveSet);
            Pokemon bulbizarre = new Pokemon("Bulbizarre", ["Grass", "Poison"], 49, 49, 45, 45, 5, 64, bulbizarreMoveSet);

            Pokemon rattata = new Pokemon("Rattata", ["Normal"], 48, 65, 43, 44, 5, 64, basicMoveSet);
            Pokemon roucool = new Pokemon("Roucool", ["Normal", "Flying"], 52, 43, 65, 39, 5, 64, basicMoveSet);
            Pokemon ferosinge = new Pokemon("Férosinge", ["Fighting"], 49, 49, 45, 45, 5, 64, basicMoveSet);

            HealingItem potion = new HealingItem("Potion", 300, 20, false);
            HealingItem superPotion = new HealingItem("Super Potion", 700, 50, false);

            CaptureItem pokeBall = new CaptureItem("Pokeball", 200, 1);
            CaptureItem superBall = new CaptureItem("Super Ball", 500, 1.5);

            ItemPokemon gobou = new ItemPokemon(3000, "Gobou", ["Water"], 48, 65, 43, 44, 5, 64, carapuceMoveSet);

            List<Pokemon> pokedex = new List<Pokemon>();
            pokedex.Add(carapuce);
            pokedex.Add(bulbizarre);
            pokedex.Add(salemeche);

            List<Pokemon> wildPokemons = new List<Pokemon>();
            wildPokemons.Add(rattata);
            wildPokemons.Add(roucool);
            wildPokemons.Add(ferosinge);

            List<IItem> items = new List<IItem>();
            items.Add(potion);
            items.Add(superPotion);
            items.Add(pokeBall);
            items.Add(superBall);
            items.Add(gobou);

            while (true)
            {
                List<string> inputs = ["Battle with your pokemons", "Go to the PokeStore", "Go to the PokeCenter (900 Pokedollars)", "Pokemon summary", "Use an item" ,"Leave the game"];

                int value = Input.Menu(inputs, user);

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

                        Battle.Fight(user, wildPokemons[new Random().Next(0, wildPokemons.Count - 1)]);
                        foreach (Pokemon wild in wildPokemons)
                        {
                            wild.Heal(wild.MaxHealth, true, true);
                        }
                        break;

                    case 1:
                        Console.Clear();
                        Store.PokeStore(items, user);
                        break;  
                        
                    case 2:
                        if (user.Pokedollars >= 900)
                        {
                        Console.Write("Healing in process");

                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write(".");
                            System.Threading.Thread.Sleep(1000);
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
                        break;

                    case 3:

                        object pkmnSum = Input.Pokemon(user.Pokemons);

                        if (pkmnSum is string)
                        {
                            break;
                        }
                        else
                        {
                            Pokemon chosenPkmn = (Pokemon)pkmnSum;
                            chosenPkmn.Summary();
                            break;
                        }

                    case 4:
                        object item = Input.Item(user.HealingItems, user.Pokedollars);

                        if (item is IItem)
                        {
                            IItem _item = (IItem)item;
                            if(user.Pokemons.Count > 0)
                            {
                                object pkmnHeal = Input.Pokemon(user.Pokemons);
                                if(pkmnHeal is Pokemon)
                                {
                                    _item.Use((Pokemon)pkmnHeal);
                                }
                            }
                            else
                            {
                                Console.WriteLine("You have no pokemon to heal");
                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                            }
                        }
                        break;

                    case 5: Console.WriteLine("See you next time");
                                break;
                }
                if (value == 5)
                {
                    break;
                }
            }
        } 
    }
}