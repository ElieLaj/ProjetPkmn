using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static Pokemon_like.Program;

namespace Pokemon_like
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello user, welcome to the pokemon-like");
            Console.WriteLine("Choose your username before the game starts");
            string username = Console.ReadLine();
            Console.Clear();

            Trainer user = new Trainer(username, 3000, new List<Pokemon>(), new List<HealingItem>());

            Console.WriteLine("Welcome back " + user.Name);

            Move tackle = new Move("Tackle", "Normal", 20 );
            Move scratch = new Move("Scratch", "Normal", 20);
            Move lick = new Move("Lick", "Ghost", 20);
            Move waterGun = new Move("Water Gun", "Water", 40);

            Move ember = new Move("Ember", "Fire", 40);

            List<Move> moveSetBasic = [tackle, scratch, lick, waterGun];
            Pokemon carapuce = new Pokemon("Carapuce", ["Water"], 16, 8, 20, 30, 5, 64, moveSetBasic);
            Pokemon salemeche = new Pokemon("Salameche", ["Fire"], 20, 5, 25, 27, 5, 64, moveSetBasic);
            Pokemon bulbizarre = new Pokemon("Bulbizarre", ["Grass"], 14, 10, 17, 33, 5, 64, moveSetBasic);

            HealingItem potion = new HealingItem("Potion", 300, 20);
            HealingItem superPotion = new HealingItem("Super Potion", 700, 50);


            List<Pokemon> pokedex = new List<Pokemon>();
            pokedex.Add(carapuce);
            pokedex.Add(bulbizarre);
            pokedex.Add(salemeche);

            List<HealingItem> items = new List<HealingItem>();
            items.Add(potion);
            items.Add(superPotion);

            while (true)
            {

                int value = MenuInputs(user);


                switch (value)
                {
                    case 0:
                        if(user.Pokemons.Count == 0)
                        {

                            object pkmn = ChoosePokemon(pokedex);

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
                        Battle(user, pokedex[new Random().Next(0, pokedex.Count - 1)]);
                        break;
                    case 1:
                        Console.Clear();
                        PokeStore(items, user);
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

        public class Pokemon
        {
            public string Name { get; set; }
            public List<string> Type { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public int Health { get; set; }
            public int Speed { get; set; }
            public int Level { get; set; }
            public int MaxHealth { get; set; }
            public int ExpThreshold { get; set; }
            public int Exp { get; set; }
            public int BaseExp { get; set; }

            public List<Move> Moves { get; set; }
            public List<Move> LearnSet { get; set; }


            public Pokemon(string _name, List<string> _type, int _attack, int _defense, int _speed, int _health, int _level, int _baseExp, List<Move> _learnset)
            {
                Name = _name;
                Type = _type;
                Attack = _attack;
                Defense = _defense;
                Speed =_speed;
                Health = _health;
                Level = _level;
                MaxHealth = _health;
                Moves = new List<Move>();
                Exp = 0;
                BaseExp = _baseExp;
                ExpThreshold = (int)(1.2 * (Math.Pow(Level, 3) - 15 * (Level * Level) + 100 * Level - 140));

                LearnSet = _learnset;
                if(_learnset.Count < 4)
                {
                    Moves = _learnset;
                }
                else
                {
                    Moves = _learnset.GetRange(0, 4);
                }
            }

            public void gainExp(int exp, int opponentLevel) {  
                int _exp = exp * opponentLevel / 7;
                Console.WriteLine(Name + " has gained " + _exp + " EXP!");

                if(Exp + _exp < ExpThreshold)
                {
                    Exp += _exp;
                }
                else
                {
                    int tmpExp = Exp + _exp - ExpThreshold;
                    Level += 1;
                    Exp = 0 + tmpExp;

                    Console.WriteLine(Name + " has leveled up to the level: " + Level + " !");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                    ExpThreshold = (int)(1.2 * (Math.Pow(Level, 3) - 15 * (Level * Level) + 100 * Level - 140));
                }

            }

            public void takeDamage(Move move, int attack)
            {
                Random random = new Random();
                int damage = attack + move.Damage / 8;
                damage -= Defense;
                if(damage <= 0) { damage = 1; }
                //double modifier = (random.NextDouble() + 1);
                //Console.WriteLine(modifier);
                //Health -= Convert.ToInt32(Math.Floor(damage * modifier)) ;
                if(Health - damage < 0)
                {
                    Health = 0;
                }
                else
                {
                    Health -= damage;
                }
                Console.WriteLine(Name + " has taken " + damage + " !");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            }

            public void useMove(Pokemon target, ref int input)
            {
                object move = ChooseMove();
                if(move is Move)
                {
                    Move _move = (Move)move;
                    Console.WriteLine(Name + " used " + _move.Name);
                    target.takeDamage(_move, Attack);
                }
                else
                {
                    input = 4;
                    return;
                }
            }

            public void useRandomMove(Pokemon target)
            {
                Move move = Moves[new Random().Next(Moves.Count)];
                Console.WriteLine(Name + " used " + move.Name);
                target.takeDamage(move, Attack);
            }

            public void ShowHealthBar()
            {
                Console.WriteLine(Name + ": " + Health + " / " + MaxHealth);
            }

            public void LearnMove(Move move)
            {
                if(Moves.Count < 4) {  
                    Moves.Add(move);
                    Console.WriteLine("You learnt " + move.Name + " !");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }

                }
                else
                {
                    object oldMove = ChooseMove();
                    if(oldMove is Move)
                    {
                        Move _oldMove = (Move)oldMove;
                        int oldMoveIndex = Moves.IndexOf(_oldMove);
                        Moves[oldMoveIndex] = move;
                        Console.WriteLine("You learnt " + move.Name + " !");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }

                }
            }

            public object ChooseMove()
            {
                bool isNotFinished = true;
                int currentInput = 0;
                List<string> inputs = new List<string>();
                foreach (Move move in Moves)
                {
                    inputs.Add(move.Name + " - " + move.Type);
                }
                inputs.Add("-- Cancel --");

                while (isNotFinished == true)
                {
                    Console.Clear();
                    Console.WriteLine("Choose a move: ");
                    (isNotFinished, currentInput) = Choices(inputs, currentInput);
                }
                if (currentInput == Moves.Count)
                {
                    return inputs[currentInput];
                }
                else
                {
                    return Moves[currentInput];
                }

            }

            public void Heal(int amount)
            {
                
                
                
                    Health += amount;
                    if (Health > MaxHealth)
                    {
                        Health = MaxHealth;
                    }
                
                
            }
        }
        public class Move
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int Damage { get; set; }

            public Move(string _name, string _type, int _damage)
            {
                Name = _name;
                Type = _type;
                Damage = _damage;
            }
        }

            public class Trainer
        {
            public string Name { get; set; }
            public int Pokedollars { get; set; }
            public List<Pokemon> Pokemons { get; set; }
            public List<HealingItem> Items { get; set; }

            public Trainer( string _name, int _pokedollars, List<Pokemon> _pokemons, List<HealingItem> _items)
            {
                Name = _name;
                Pokedollars = _pokedollars;
                Pokemons = _pokemons;
                Items = _items;
            }

            public void UseItem(Pokemon pokemon, ref int input)
            {
                object item = ChooseItem(Items);
                if (item is HealingItem)
                {
                    HealingItem usedItem = (HealingItem)item;
                    usedItem.Use(pokemon);

                    Items.Remove(usedItem);
                    Console.WriteLine("You used " + usedItem.Name + " on " + pokemon.Name + " !");
                    Console.WriteLine("He gained: " + usedItem.Heal + " health");
                }
                else
                {
                    input = 4;
                    return ;
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
                        pkmn = ChoosePokemon(Pokemons);
                        if(pkmn == pkmn1)
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
        public interface IItem
        {
            string Name { get; }
            int Cost { get; }
            Pokemon Use(Pokemon target);
        }

        public class HealingItem : IItem
        {
            public string Name { get; set; }
            public int Cost { get; set; }
            public int Heal { get; set; }


            public HealingItem(string _name, int _cost, int _heal)
            {
                Name = _name;
                Cost = _cost;
                Heal = _heal;
            }

            public Pokemon Use(Pokemon target)
            {
                target.Heal(Heal);
                return null;

            }
        }
        public class CaptureItem : IItem
        {
            public string Name { get; set; }
            public int Cost { get; set; }
            public int Rate { get; set; }


            public CaptureItem(string _name, int _cost, int _rate)
            {
                Name = _name;
                Cost = _cost;
                Rate = _rate;

            }

            public Pokemon Use(Pokemon target)
            {
                if ((1 - ((2 / 3) * (target.Health / target.MaxHealth))) * 200 * Rate >= 160)
                {
                   return target;
                }
                else
                {
                    return null;
                }
            }
        }
        public static void Battle(Trainer trainer, Pokemon pkmn2)
        {
            Pokemon pkmn1 = trainer.Pokemons[0];
            bool escaped = false;

            while (pkmn1.Health > 0 && pkmn2.Health > 0 && !escaped)
            {

                Console.Clear();
                pkmn1.ShowHealthBar();
                pkmn2.ShowHealthBar();

                if (pkmn2.Speed > pkmn1.Speed)
                {
                    pkmn2.useRandomMove(pkmn1);

                    PerformTurn(pkmn1, pkmn2, trainer, ref escaped);
                    if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped)
                        break;
                    pkmn1 = trainer.Pokemons[0];

                }
                else if (pkmn1.Speed > pkmn2.Speed)
                {

                    PerformTurn(pkmn1, pkmn2, trainer, ref escaped);
                    if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped)
                        break;
                    pkmn1 = trainer.Pokemons[0];

                    pkmn2.useRandomMove(pkmn1);

                }
                else 
                {
                    if (new Random().Next(0, 2) == 0)
                    {
                        PerformTurn(pkmn1, pkmn2, trainer, ref escaped);
                        if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped)
                            break;
                        pkmn1 = trainer.Pokemons[0];

                        pkmn2.useRandomMove(pkmn1);
                    }
                    else
                    {
                        pkmn2.useRandomMove(pkmn1);

                        if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped)
                            break;

                        PerformTurn(pkmn1, pkmn2, trainer, ref escaped);
                        pkmn1 = trainer.Pokemons[0];
                    }
                }
            }

            Console.Clear();
            if(pkmn1.Health <= 0)
            {
                Console.WriteLine(pkmn1.Name + " has fainted !");
                Console.WriteLine("You lost...");

            }
            else if(pkmn2.Health <= 0)
            {
                Console.WriteLine(pkmn2.Name + " has fainted !");
                pkmn1.gainExp((int)(pkmn2.BaseExp * 1.5), pkmn2.Level);
                Console.WriteLine("You won 500 Pokedollars");
                trainer.Pokedollars += 500;
            }
            else
            {
                Console.WriteLine("You managed to run away but lost 200 Pokedollars... ");
                trainer.Pokedollars -= 200;
            }
            Console.WriteLine("Press Enter to start again");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }



        private static void PerformTurn(Pokemon trainerMon, Pokemon pkmn2, Trainer trainer, ref bool escaped)
        {
            int input = 4;
            
            do
            {
                input = BattleInputs(trainerMon, pkmn2);

                switch (input)
                {
                    case 0:
                        trainerMon.useMove(pkmn2, ref input);
                        break;
                    case 1:
                        trainer.UseItem(trainerMon, ref input);
                        break;
                    case 2:
                        escaped = trainer.AttemptEscape(pkmn2);
                        if (escaped)
                        {
                            return; 
                        }
                        break;
                    case 3:
                        trainer.SwapPokemon(trainerMon, ref input);
                        break;
                    default:
                        break;
                }
            } while (input == 4);
        }

        public static int MenuInputs(Trainer user)
        {

            List<string> inputs = ["Battle with your pokemons", "Go to the PokeStore", "Go to the PokeCenter", "Leave the game"];

            int currentInput = 0;

            bool isNotFinished = true;

            while (isNotFinished == true)
            {
                Console.Clear();
                Console.WriteLine("What do you want to do " + user.Name + " ?");
                Console.WriteLine("Balance: " + user.Pokedollars + " Pokedollars");
                (isNotFinished, currentInput) = Choices(inputs, currentInput);
            }

            return currentInput;
        }

        public static int BattleInputs(Pokemon pkmn1, Pokemon pkmn2)
        {
            string inputAttack = "Attack";
            string inputItem = "Item";
            string inputRun = "Run";
            string inputSwap = "Swap";

            List<string> inputs = [inputAttack, inputItem, inputRun, inputSwap];

            int currentInput = 0;

            bool isNotFinished = true;

            while (isNotFinished == true)
            {
                Console.Clear();
                pkmn1.ShowHealthBar();
                pkmn2.ShowHealthBar();
                Console.WriteLine("What do you want to do: ");
                (isNotFinished, currentInput) = Choices(inputs, currentInput);
            }

            return currentInput;
        }

        

        public static object ChoosePokemon(List<Pokemon> pokemonList)
        {
            bool isNotFinished = true;
            int currentInput = 0;
            List<string> inputs = new List<string>();
            foreach(Pokemon pokemon in pokemonList)
            {
                inputs.Add(pokemon.Name);
            }
            inputs.Add("-- Leave --");

            while (isNotFinished == true)
            {
                Console.Clear();
                Console.WriteLine("Very well then ! Please choose a Pokemon: ");
                (isNotFinished, currentInput) = Choices(inputs, currentInput);
            }
            if (currentInput == pokemonList.Count)
            {
                return inputs[currentInput];
            }
            else
            {
                return pokemonList[currentInput];
            }

        }

        

        public static object ChooseItem(List<HealingItem> itemList)
        {
            bool isNotFinished = true;
            int currentInput = 0;
            List<string> inputs = new List<string>();
            foreach (HealingItem item in itemList)
            {
                inputs.Add(item.Name + " " + item.Cost + " Pokedollars");
            }
            inputs.Add("-- Leave --");

            while (isNotFinished == true)
            {
                Console.Clear();
                (isNotFinished, currentInput) = Choices(inputs, currentInput);
            }
            
            if(currentInput == itemList.Count)
            {
                return inputs[currentInput];
            }
            else
            {
                return itemList[currentInput];


            }

        }

        public static (bool, int) Choices(List<string> inputs, int currentInput)
        {

            for (int i = 0; i < inputs.Count; i++)
            {
                if (currentInput == i)
                {
                    Console.WriteLine("-> " + inputs[i]);
                }
                else
                {
                    Console.WriteLine(inputs[i]);
                }
            }
            if (Console.ReadKey().Key == ConsoleKey.UpArrow && currentInput > 0)
            {
                currentInput--;
            }
            else if (Console.ReadKey().Key == ConsoleKey.DownArrow && currentInput < inputs.Count - 1)
            {
                currentInput++;
            }
            else if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                return (false, currentInput);
            }
            return (true, currentInput);
        }

        public static void PokeStore(List<HealingItem> items, Trainer trainer)
        {
            int totalItem = trainer.Items.Count;
            object item = "";
            while (true)
            {
                Console.WriteLine("Merchand: What do you want to buy traveler ?");
                item = ChooseItem(items);
                if(item is string)
                {
                    break;
                }
                else if(item is HealingItem) 
                {
                    HealingItem item2 = (HealingItem)item;
                    if(trainer.Pokedollars - item2.Cost >= 0)
                    {
                        trainer.Items.Add(item2);
                        trainer.Pokedollars -= item2.Cost;
                        Console.WriteLine("You chose " + item2.Name);
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough money for " + item2.Name);
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }
                    
                }

               
            }
        }
    }
}