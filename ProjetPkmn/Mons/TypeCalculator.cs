using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPkmn.Mons
{
    internal class TypeCalculator
    {
        public static double Calculate(List<string> pkmnType, Move move)
        {
            double effectiveness = 1;
            foreach (string type in pkmnType)
            {
                if (type == "Water")
                {
                    if (move.Type == "Water")
                    {
                        effectiveness /= 2;
                    }
                    if (move.Type == "Grass")
                    {
                        effectiveness *= 2;

                    }
                    if (move.Type == "Fire")
                    {
                        effectiveness /= 2;

                    }
                }
                if (type == "Fire")
                {
                    if (move.Type == "Water")
                    {
                        effectiveness *= 2;

                    }
                    if (move.Type == "Grass")
                    {
                        effectiveness /= 2;

                    }
                    if (move.Type == "Fire")
                    {
                        effectiveness /= 2;

                    }
                }
                if (type == "Grass")
                {
                    if (move.Type == "Water")
                    {
                        effectiveness /= 2;

                    }
                    if (move.Type == "Grass")
                    {
                        effectiveness /= 2;

                    }
                    if (move.Type == "Fire")
                    {
                        effectiveness *= 2;

                    }
                    if (move.Type == "Poison")
                    {
                        effectiveness /= 2;
                    }
                }
                if (type == "Poison")
                {
                    if (move.Type == "Grass")
                    {
                        effectiveness /= 2;

                    }
                }
            }
            return effectiveness;
        }
    }
}
