using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Mons;
using ProjetPkmn.Inputs;
using ProjetPkmn.Items;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.Game
{
    internal class Battle
    {
        public static void Fight(Trainer trainer, Pokemon pkmn2)
        {
            Pokemon pkmn1 = trainer.Pokemons[0];
            bool escaped = false;
            bool captured = false;

            Console.WriteLine("You encountered a wild " + pkmn2.Name + " !");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            while (pkmn1.Health > 0 && pkmn2.Health > 0 && !escaped && !captured)
            {

                Console.Clear();
                pkmn1.ShowHealthBar();
                pkmn2.ShowHealthBar();

                if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped || captured)
                { break; }

                if (pkmn2.Speed > pkmn1.Speed)
                {
                    pkmn2.useRandomMove(pkmn1);

                    PerformTurn(pkmn1, pkmn2, trainer, ref escaped, ref captured);
                    if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped || captured)
                        { break; }
                    pkmn1 = trainer.Pokemons[0];

                }
                else if (pkmn1.Speed > pkmn2.Speed)
                {

                    PerformTurn(pkmn1, pkmn2, trainer, ref escaped, ref captured);
                    if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped || captured)
                    { break; }
;
                    pkmn1 = trainer.Pokemons[0];

                    pkmn2.useRandomMove(pkmn1);

                }
                else
                {
                    if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped || captured)
                    { break; }

                    if (new Random().Next(0, 2) == 0)
                    {
                        PerformTurn(pkmn1, pkmn2, trainer, ref escaped, ref captured);
                        if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped || captured)
                        { break; }
                        pkmn1 = trainer.Pokemons[0];

                        pkmn2.useRandomMove(pkmn1);
                    }
                    else
                    {
                        pkmn2.useRandomMove(pkmn1);

                        if (pkmn1.Health <= 0 || pkmn2.Health <= 0 || escaped || captured)
                        { break; }

                        PerformTurn(pkmn1, pkmn2, trainer, ref escaped, ref captured);
                        pkmn1 = trainer.Pokemons[0];
                    }
                }
            }

            Console.Clear();
            if (pkmn1.Health <= 0)
            {
                Console.WriteLine(pkmn1.Name + " has fainted !");
                Console.WriteLine("You lost...");

            }
            else if (pkmn2.Health <= 0)
            {
                Console.WriteLine(pkmn2.Name + " has fainted !");
                pkmn1.gainExp((int)(pkmn2.BaseExp * 5), pkmn2.Level);
                Console.WriteLine("You won 500 Pokedollars");
                trainer.Pokedollars += 500;
            }
            else if(escaped)
            {
                Console.WriteLine("You managed to run away but lost 200 Pokedollars... ");
                trainer.Pokedollars -= 200;
            }

            Console.WriteLine("Press Enter to start again");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }



        private static void PerformTurn(Pokemon trainerMon, Pokemon pkmn2, Trainer trainer, ref bool escaped, ref bool captured)
        {
            int input = 4;

            do
            {
                input = Input.Battle(trainerMon, pkmn2);

                switch (input)
                {
                    case 0:
                        trainerMon.useMove(pkmn2, ref input);
                        break;
                    case 1:
                        trainer.ChooseItemType(ref input, ref captured, pkmn2);
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
    }
}
