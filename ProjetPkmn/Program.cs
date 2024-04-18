using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Game;
using static System.Net.Mime.MediaTypeNames;
using static ProjetPkmn.Program;
using ProjetPkmn.Assets.Map;
using ProjetPkmn.Assets.Map.Plans;
using ProjetPkmn.Assets.Map.TilesType;
using ProjetPkmn.Tools.Inputs;
using ProjetPkmn.Assets.Items;
using ProjetPkmn.Assets.Items.HealingItems;
using ProjetPkmn.Assets.Items.PokeBall;
using ProjetPkmn.Assets.Items.Pokemons;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Map.Places.Stores;
using ProjetPkmn.Tools.TCP;
using ProjetPkmn.Assets.Trainers;

namespace ProjetPkmn
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Move tackle = new Move("Tackle", "Normal", 40);
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

            Dictionary<int, Move> carapuceMoveSet = new Dictionary<int, Move> { { 1, lick }, { 3, scratch }, { 4, rapidSpin }, { 5, waterGun }, { 6, waterPulse } };
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

            List<Pokemon> pokedex = new List<Pokemon>() {
                carapuce,
                bulbizarre,
                salemeche
            };


            List<Pokemon> wildPokemons = new List<Pokemon>()
            {
                rattata,
                roucool,
                ferosinge
            };

            List<IItem> items = new List<IItem>() {
                potion,
                superPotion,
                pokeBall,
                superBall,
                gobou
            };

            Bush bush = new Bush("#", 30);

            Wall sideWall = new Wall("!");
            Wall longsideWall = new Wall(" !");

            Wall upWall = new Wall("-");
            Wall downWall = new Wall( "_");

            Ground leftDirt = new Ground("| ");
            Ground middleDirt = new Ground( " ");
            Ground rightDirt = new Ground(" |");
            Ground upDirt = new Ground("_");
            Ground downDirt = new Ground("-");
            Ground diagRightDirt = new Ground("\\ ");
            Ground diagLeftDirt = new Ground(" /");

            PokeCenter center = new PokeCenter("H");

            Tile[,] map1Tiles = { 
                { upWall, upWall, upWall, upWall, upWall, upWall, upWall }, 
                { sideWall, bush, leftDirt, center, rightDirt, bush, sideWall }, 
                { sideWall, bush, leftDirt, middleDirt, rightDirt, bush, sideWall }, 
                { sideWall, bush, leftDirt, middleDirt, rightDirt, bush, sideWall }, 
                { sideWall, bush, leftDirt, middleDirt, rightDirt, bush, sideWall }, 
                { sideWall, bush, leftDirt, middleDirt, rightDirt, bush, sideWall }, 
                { downWall, downWall, downWall, downWall, downWall, downWall, downWall } };

            Tile[,] arena1Tiles = {
                { upWall, upWall, upWall, upWall, upWall, upWall, upWall, upWall, upWall, upWall, upWall, upWall },
                { sideWall, bush, leftDirt, middleDirt, rightDirt, bush, bush, bush, bush, bush, bush, sideWall },
                { sideWall, bush, leftDirt, middleDirt, rightDirt, upDirt, upDirt, upDirt, upDirt, upDirt, upDirt, sideWall },
                { sideWall, bush, diagRightDirt, middleDirt, middleDirt, middleDirt, middleDirt, middleDirt, middleDirt, middleDirt, middleDirt, sideWall },
                { sideWall, bush, bush, downDirt, downDirt, downDirt, downDirt, downDirt, downDirt, downDirt, downDirt, longsideWall },
                { downWall, downWall, downWall, downWall, downWall, downWall, downWall, downWall, downWall, downWall, downWall, downWall } };

            string username = "";

            do
            {
                Console.Clear();

                Console.WriteLine("Hello user, welcome to the pokemon-like");
                Console.WriteLine("Choose your username before the game starts");
                Console.WriteLine("Please enter a username longer than 3 characters: ");

               // username = Console.ReadLine();
                username = "Azene";
            } while (username.Length <= 3);

           
            Trainer user = new Trainer(username, 4000, new List<Pokemon>(), new List<IItem>(), new List<IItem>(), new TrainerTile("U", 0));
            TrainerNPC fargas = new TrainerNPC("Fargas", 700, new List<Pokemon>(), new List<IItem>(), new List<IItem>(), "Welcome to my gym, you'll have to win if you want my badge !", new TrainerTile("V ", 1), new PlayerPosition { X = 3, Y = 10}, 1);
            fargas.Pokemons.Add(rattata);
            fargas.Pokemons.Add(ferosinge);

            Plan arena1 = new Plan(6, 12, arena1Tiles, new List<TrainerNPC> { fargas }, new List<TeleportationPoint> { }, user, wildPokemons);
            Plan map1 = new Plan(7, 7, map1Tiles, new List<TrainerNPC> {  },new List<TeleportationPoint> { new TeleportationPoint("\\/", 1, 3, arena1)} ,user, wildPokemons);

            arena1.Tiles[0, 3] = new TeleportationPoint("\\/", 5, 3, map1);
            map1.Tiles[6, 3] = new TeleportationPoint("\\/", 1, 3, arena1);

            Console.WriteLine("Welcome back " + user.Name);

            while (true)
            {
                List<string> inputs = ["Start the game", "Go to the PokeStore", "Go to the PokeCenter (900 Pokedollars)", "Pokemon summary", "Use an item", "Connect to a Host", "Host a game", "Leave the game"];

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

                        map1.Display();
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
                    case 5:
                        //Console.WriteLine("What is the host IP: ");
                        //string ip = Console.ReadLine();
                        //Console.WriteLine("What is the host port: ");
                        // string tmp = Console.ReadLine();
                        try
                        {
                            //int port = Int32.Parse(tmp);
                            Client myClient = new Client("127.0.0.1", 13000);
                            myClient.Start();
                            //myClient.SendName(username);

                            Trainer user2 = user;
                            user = new Trainer(username, 4000, new List<Pokemon> { bulbizarre }, new List<IItem>(), new List<IItem>(), new TrainerTile("A", 0));
                            Plan arenaMP1 = new Plan(6, 12, arena1Tiles, new List<TrainerNPC> { fargas }, new List<TeleportationPoint> { }, user, wildPokemons);
                            PlanClient mapMP1 = new PlanClient(7, 7, map1Tiles, new List<TrainerNPC> { }, new List<TeleportationPoint> { new TeleportationPoint("\\/", 1, 3, arenaMP1) }, user2, user, wildPokemons, myClient);
                            user2.Pokemons.Add(carapuce);

                            arenaMP1.Tiles[0, 3] = new TeleportationPoint("\\/", 5, 3, mapMP1);
                            mapMP1.Tiles[6, 3] = new TeleportationPoint("\\/", 1, 3, arenaMP1);

                            
                            Console.WriteLine("What is the host port: ");

                            mapMP1.Display();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Port isn't a number '{13000}'");
                        }
                        break;
                    case 6:
                        //string tmp2 = Console.ReadLine();
                        try
                        {
                            //int port = Int32.Parse(tmp2);
                            Server myServer = new Server(13000);
                            myServer.Start();
                            string username2 = "A";
                            //myServer.ListenForName(ref username2);
                            Trainer user2 = new Trainer(username2, 4000, new List<Pokemon> { bulbizarre }, new List<IItem>(), new List<IItem>(), new TrainerTile("A", 0));
                            Plan arenaMP1 = new Plan(6, 12, arena1Tiles, new List<TrainerNPC> { fargas }, new List<TeleportationPoint> { }, user, wildPokemons);
                            PlanHost mapMP1 = new PlanHost(7, 7, map1Tiles, new List<TrainerNPC> { }, new List<TeleportationPoint> { new TeleportationPoint("\\/", 1, 3, arenaMP1) }, user, user2, wildPokemons, myServer);
                            user.Pokemons.Add(carapuce);


                            arenaMP1.Tiles[0, 3] = new TeleportationPoint("\\/", 5, 3, mapMP1);
                            mapMP1.Tiles[6, 3] = new TeleportationPoint("\\/", 1, 3, arenaMP1);

                            mapMP1.Display();
                            myServer.Self.Stop();
                            //myServer.ListenInput();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Port isn't a number '{13000}'");
                        }
                        break; 
                    case 7: 
                        Console.WriteLine("See you next time");
                        break;
                }
                if (value == 7)
                {
                    break;
                }
            }
        } 
    }
}