﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Xml.Linq;
using ProjetPkmn.Trainers;
using Newtonsoft.Json;

namespace ProjetPkmn.TCP
{
    public class Client
    {
        public string HostIP { get; set; }
        public TcpClient Self { get; set; }
        public int Port { get; set; }
        public NetworkStream NetworkStream { get; set; }

        public Client(string _hostIP, int _port) { 
            HostIP = _hostIP;
            Self = new TcpClient();
            Port = _port;
        }
        public void Start()
        {
            Self.Connect(HostIP, Port);
            NetworkStream = Self.GetStream();
        }
        public void SendMovement(PlayerPosition playerPosition)
        {
            string jsonData = JsonConvert.SerializeObject(playerPosition);

            Byte[] data = System.Text.Encoding.ASCII.GetBytes(jsonData);

            NetworkStream.Write(data, 0, data.Length);
        }

        public PlayerPositions ReceiveMovements(PlayerPositions oldPositions)
        {
            byte[] bytes = new byte[1024];

            int bytesRead = NetworkStream.Read(bytes, 0, bytes.Length);
            if (bytesRead > 0)
            {
                string jsonData = Encoding.ASCII.GetString(bytes, 0, bytesRead);

                PlayerPositions positions = JsonConvert.DeserializeObject<PlayerPositions>(jsonData);
                return positions;
            }
            else
            {
                return oldPositions;
            }
        }

        public void SendName(string name)
        {
            Console.WriteLine("Sent name");
            Byte[] data = System.Text.Encoding.ASCII.GetBytes("Name: " + name);
            NetworkStream.Write(data, 0, data.Length);
        }

        public void ConnectHost()
        {
            try
            {
                Self.Connect(HostIP, Port);
                using NetworkStream networkStream = Self.GetStream();
                string message = "Hello";
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                networkStream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", message);

                string responseData = string.Empty;

                Int32 bytes = networkStream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

       
    }
}
