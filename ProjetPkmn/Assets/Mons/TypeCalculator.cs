using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPkmn.Assets.Mons
{
    internal class TypeCalculator
    {
        public static double Calculate(List<string> pkmnType, Move move)
        {
            double effectiveness = 1;

            if (pkmnType.Contains("Water"))
            {
                if (move.Type == "Water")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Fire")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Ice")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Steel")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Grass")
                {
                    effectiveness *= 2;
                }
                else if (move.Type == "Electric")
                {
                    effectiveness *= 2;
                }
            }
            if (pkmnType.Contains("Fire"))
            {
                if (move.Type == "Water")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ice")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Steel")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Fairy")
                {
                    effectiveness /= 2;
                }
                else if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fire")
                {
                    effectiveness /= 2;

                }
            }
            if (pkmnType.Contains("Grass"))
            {
                if (move.Type == "Water")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Electric")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fire")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness *= 2;
                }
                else if (move.Type == "Ice")
                {
                    effectiveness *= 2;
                }
                else if (move.Type == "Bug")
                {
                    effectiveness *= 2;
                }
                else if (move.Type == "Flying")
                {
                    effectiveness *= 2;
                }
            }
            if (pkmnType.Contains("Poison"))
            {
                if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fairy")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Psy")
                {
                    effectiveness *= 2;

                }
            }
            if (pkmnType.Contains("Normal"))
            {
                if (move.Type == "Fighting")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ghost")
                {
                    effectiveness *= 0;
                    return effectiveness;
                }
            }
            if (pkmnType.Contains("Flying"))
            {
                if (move.Type == "Fighting")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Electric")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ice")
                {
                    effectiveness *= 2;

                }
            }
            if (pkmnType.Contains("Fighting"))
            {
                if (move.Type == "Flying")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Psy")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fairy")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Dark")
                {
                    effectiveness /= 2;

                }
            }
            if (pkmnType.Contains("Electric"))
            {
                if (move.Type == "Flying")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Steel")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Electric")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness *= 2;

                }
            }
            if (pkmnType.Contains("Ice"))
            {
                if (move.Type == "Fire")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Steel")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness *= 2;

                }
            }
            if (pkmnType.Contains("Ground"))
            {
                if (move.Type == "Grass")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Water")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ice")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Electric")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }
            }
            if (pkmnType.Contains("Psy"))
            {
                if (move.Type == "Dark")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ghost")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Psy")
                {
                    effectiveness /= 2;

                }

            }
            if (pkmnType.Contains("Bug"))
            {
                if (move.Type == "Fire")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Flying")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness /= 2;

                }
            }
            if (pkmnType.Contains("Rock"))
            {
                if (move.Type == "Ground")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Water")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Grass")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Steel")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Normal")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fire")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Flying")
                {
                    effectiveness /= 2;

                }
            }
            if (pkmnType.Contains("Ghost"))
            {
                if (move.Type == "Ghost")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Dark")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }
                else if (move.Type == "Normal")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }
            }
            if (pkmnType.Contains("Dragon"))
            {
                if (move.Type == "Dragon")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ice")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fairy")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fire")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Water")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Electric")
                {
                    effectiveness /= 2;

                }
            }
            if (pkmnType.Contains("Dark"))
            {
                if (move.Type == "Bug")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fairy")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Dark")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ghost")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Psy")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }

            }
            if (pkmnType.Contains("Steel"))
            {
                if (move.Type == "Fire")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Ground")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Dragon")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Rock")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Flying")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Grass")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fairy")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Psy")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Steel")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Ice")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }
            }
            if (pkmnType.Contains("Fairy"))
            {
                if (move.Type == "Steel")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Poison")
                {
                    effectiveness *= 2;

                }
                else if (move.Type == "Dark")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Fighting")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Bug")
                {
                    effectiveness /= 2;

                }
                else if (move.Type == "Dragon")
                {
                    effectiveness *= 0;
                    return effectiveness;

                }
            }
            return effectiveness;
        }
    }
}
