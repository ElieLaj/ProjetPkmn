﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPkmn.Choose;

namespace ProjetPkmn.Mons
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<string> Type { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int IVAttack { get; set; }
        public int IVDefense { get; set; }
        public int IVHealth { get; set; }
        public int IVSpeed { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseHealth { get; set; }
        public int BaseSpeed { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int ExpThreshold { get; set; }
        public int Exp { get; set; }
        public int BaseExp { get; set; }

        public List<Move> Moves { get; set; }
        public Dictionary<int, Move> LearnSet { get; set; }


        public Pokemon(string _name, List<string> _type, int _attack, int _defense, int _speed, int _health, int _level, int _baseExp, Dictionary<int, Move> _learnset)
        {
            Level = _level;
            Name = _name;
            Type = _type;

            BaseHealth = _health;
            BaseAttack = _attack;
            BaseDefense = _defense;
            BaseSpeed = _speed;

            IVAttack = new Random().Next(0, 31);
            IVDefense = new Random().Next(0, 31);
            IVHealth = new Random().Next(0, 31);
            IVSpeed = new Random().Next(0, 31);

            Health = (int)(Math.Floor(0.01 * (2 * BaseHealth + IVHealth) * Level) + Level + 10);
            MaxHealth = Health;

            Attack = statCalculator(BaseAttack, IVAttack);
            Defense = statCalculator(BaseDefense, IVDefense);
            Speed = statCalculator(BaseSpeed, IVSpeed);

            Moves = new List<Move>();
            Exp = 0;
            BaseExp = _baseExp;
            ExpThreshold = (int)(1.2 * (Math.Pow(Level, 3) - 15 * (Level * Level) + 100 * Level - 140));

            LearnSet = _learnset;
            if (_learnset.Count < 4)
            {
                Moves.AddRange(_learnset.Values);
            }
            else
            {
                int j = 0;
                for (int i = Level; i > 0; i--)
                {
                    if (_learnset.ContainsKey(i))
                    {
                        Moves.Add(_learnset[i]);
                        j++;
                    }
                    if(j == 4)
                    {
                        break;
                    }
                }
            }
        }
        private void calculateStats()
        {
            int newHealth = (int)(Math.Floor(0.01 * (2 * BaseHealth + IVHealth) * Level) + Level + 10);
            int newAttack = statCalculator(BaseAttack, IVAttack);
            int newDefense = statCalculator(BaseDefense, IVDefense);
            int newSpeed = statCalculator(BaseSpeed, IVSpeed);

            Console.WriteLine(Name + " has gained " + (newHealth - MaxHealth) + " health");
            Console.WriteLine(Name + " has gained " + (newAttack - Attack) + " attack");
            Console.WriteLine(Name + " has gained " + (newDefense - Defense) + " defense");
            Console.WriteLine(Name + " has gained " + (newSpeed - Speed) + " speed");

            MaxHealth = newHealth;
            Health += newHealth - MaxHealth;
            Attack = newAttack;
            Defense = newDefense;
            Speed = newSpeed;
        }
        private int statCalculator(int stat, int IV)
        {
            return (int)Math.Floor(0.01 * (stat * 2 + IV) * Level + 5);
        }

        public void Summary()
        {
            Console.WriteLine("Health: " + Health + " / " + MaxHealth);
            Console.WriteLine("Attack: " + Attack);
            Console.WriteLine("Defense: " + Defense);
            Console.WriteLine("Speed: " + Speed);
            Console.WriteLine("\nPress enter to leave");

            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        private void takeDamage(Move move, Pokemon attacker)
        {
            Random random = new Random();
            double stab = 1;
            double effectiveness = TypeCalculator.Calculate(Type, move);
            int damage = 0;

            foreach (string type in attacker.Type)
            {
                if (move.Type == type)
                {
                    stab = 1.3;
                }
            }
            if(effectiveness != 0)
            {
                damage = (int)((((attacker.Level * 0.4 + 2) * attacker.Attack * move.Damage) / (50 * Defense) + 2) * stab * effectiveness);
                if (damage <= 0) { damage = 1; }
                double modifier = random.NextDouble() + 1;
                damage = (int)(damage * modifier);
                if (Health - damage < 0)
                {
                    Health = 0;
                }
                else
                {
                    Health -= damage;
                }
            }
            
            Console.WriteLine(Name + " has taken " + damage + " !");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }

        public void useMove(Pokemon target, ref int input)
        {
            object move = ChooseMove();
            if (move is Move)
            {
                Move _move = (Move)move;
                Console.WriteLine(Name + " used " + _move.Name);


                target.takeDamage(_move, this);
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


            target.takeDamage(move, this);
        }

        public void ShowHealthBar()
        {
            Console.WriteLine(Name + ": " + Health + " / " + MaxHealth);
        }
        public void gainExp(int exp, int opponentLevel)
        {
            int _exp = exp * opponentLevel / 7;
            Console.WriteLine(Name + " has gained " + _exp + " EXP!");

            if (Exp + _exp < ExpThreshold)
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

                calculateStats();

                checkMoveLvlUp();

                ExpThreshold = (int)(1.2 * (Math.Pow(Level, 3) - 15 * (Level * Level) + 100 * Level - 140));
            }
        }

        private void checkMoveLvlUp()
        {
            if (LearnSet.ContainsKey(Level))
            {
                Move move = LearnSet[Level];
                Console.WriteLine(Name + " is trying to learn " + move.Name + " - " + move.Damage + " - " + move.Type);
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                LearnMove(move);
            }
        }

        public void LearnMove(Move move)
        {
            if (Moves.Count < 4)
            {
                Moves.Add(move);
                Console.WriteLine("You learnt " + move.Name + " !");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            }
            else
            {
                object oldMove = ChooseMove();
                if (oldMove is Move)
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
                inputs.Add(move.Name + " - " + move.Damage + " - " + move.Type);
            }
            inputs.Add("-- Cancel --");

            while (isNotFinished == true)
            {
                Console.Clear();
                Console.WriteLine("Choose a move: ");
                (isNotFinished, currentInput) = Choices.Loop(inputs, currentInput);
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
}
