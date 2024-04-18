using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPkmn.Tools.Choose
{
    internal class Choices
    {
        public static (bool, int) Loop(List<string> inputs, int currentInput)
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
    }
}
