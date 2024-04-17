using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using ProjetPkmn.Trainers;

namespace ProjetPkmn.TCP
{
    public class Server
    {
        public int Port { get; set; }
        public IPAddress LocalAdress { get; set; }
        public TcpListener Self { get; set; }
        public TcpClient ClientsHandler { get; set; }
        public NetworkStream Stream { get; set; }
        private List<Client> ConnectedClients { get; set; }

        public Server(int _port)
        {
            Port = _port;
            LocalAdress = IPAddress.Any;
            ConnectedClients = new List<Client>();

            Self = new TcpListener(LocalAdress, Port);
            Self.Start();
            Console.WriteLine("Server started. Waiting for connections...");

            ClientsHandler = Self.AcceptTcpClient();
            string clientIP = ((IPEndPoint)ClientsHandler.Client.RemoteEndPoint).Address.ToString();
            Console.Write(clientIP);
            Client client = new Client(clientIP, Port);
            ConnectedClients.Add(client);

            Stream = ClientsHandler.GetStream();
        }
        public PlayerPosition ListenInput()
        {
            int i;
            Byte[] bytes = new Byte[256];

            if ((i = Stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                string jsonData = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                PlayerPosition playerPosition = JsonConvert.DeserializeObject<PlayerPosition>(jsonData);
                return playerPosition;
            }
            return null;
        }

        public void SendMovementToAllClients(int player1X, int player1Y, int player2X, int player2Y)
        {
            PlayerPositions positions = new PlayerPositions
            {
                Player1 = new PlayerPosition { X = player1X, Y = player1Y },
                Player2 = new PlayerPosition { X = player2X, Y = player2Y }
            };

            string jsonData = JsonConvert.SerializeObject(positions);

            byte[] data = System.Text.Encoding.ASCII.GetBytes(jsonData);
            //Console.WriteLine("P1: " + player1X + " " + player1Y + " | P2: " + player2X + " " + player2Y);
            Stream.Write(data, 0, data.Length);
        }

        public void ListenForName(ref string name)
        {
            int i;
            Byte[] bytes = new Byte[256];
            Console.WriteLine("Trying to get the other user name");
            while ((i = Stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                    string data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    if(data.Contains("Name:"))
                    {
                        name = data.Split(":")[1];
                        return ; 
                    }

            }
        }
        
    }
}
