using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjetPkmn
{
    internal class Request
    {
        static void Meh(string[] args)
        {
            using(var client = new HttpClient()) {
                var endpoint = client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/1");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
            }
            
        }
    }
}
