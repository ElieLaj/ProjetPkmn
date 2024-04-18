using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Assets.Items;
using ProjetPkmn.Assets.Mons;
using ProjetPkmn.Assets.Trainers;
using ProjetPkmn.Tools.Choose;

namespace ProjetPkmn.Tools.Inputs
{
    internal class Input
    {
        public static int Menu(List<string> inputs, Trainer user)
        {

            int currentInput = 0;

            bool isNotFinished = true;

            while (isNotFinished == true)
            {
                Console.Clear();
                Console.WriteLine("What do you want to do " + user.Name + " ?");
                Console.WriteLine("Balance: " + user.Pokedollars + " Pokedollars");
                (isNotFinished, currentInput) = Choices.Loop(inputs, currentInput);
            }

            return currentInput;
        }
        public static int Battle(Pokemon pkmn1, Pokemon pkmn2)
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
                (isNotFinished, currentInput) = Choices.Loop(inputs, currentInput);
            }

            return currentInput;
        }



        public static object Pokemon(List<Pokemon> pokemonList)
        {
            bool isNotFinished = true;
            int currentInput = 0;
            List<string> inputs = new List<string>();
            foreach (Pokemon pokemon in pokemonList)
            {
                inputs.Add(pokemon.Name);
            }
            inputs.Add("-- Leave --");

            while (isNotFinished == true)
            {
                Console.Clear();
                Console.WriteLine("Very well then ! Please choose a Pokemon: ");
                (isNotFinished, currentInput) = Choices.Loop(inputs, currentInput);
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



        public static object Item(List<IItem> itemList, int currency)
        {
            bool isNotFinished = true;
            int currentInput = 0;
            List<string> inputs = new List<string>();
            foreach (IItem item in itemList)
            {
                inputs.Add(item.Name + " " + item.Cost + " Pokedollars");
            }
            inputs.Add("-- Leave --");

            while (isNotFinished == true)
            {
                Console.Clear();
                Console.WriteLine("You have: " + currency + " Pokedollars");
                (isNotFinished, currentInput) = Choices.Loop(inputs, currentInput);
            }

            if (currentInput == itemList.Count)
            {
                return inputs[currentInput];
            }
            else
            {
                return itemList[currentInput];


            }

        }
        public static int ItemType()
        {
            bool isNotFinished = true;
            int currentInput = 0;

            List<string> inputs = ["Healing Items", "Pokeballs"];

            inputs.Add("-- Leave --");

            while (isNotFinished == true)
            {
                Console.Clear();
                (isNotFinished, currentInput) = Choices.Loop(inputs, currentInput);
            }

            return currentInput;

        }
    }
}
