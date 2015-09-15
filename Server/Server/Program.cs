using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PacketConnector;

namespace Server
{
    class Program
    {
        public static List<GameObject> objects;
        private static List<Client> clients;
        private static Socket listener;

        private static void Main(string[] args)
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<Client>();
            objects = new List<GameObject>();

            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 25565);
            listener.Bind(ip);

            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();

            Console.WriteLine("Server Started");

            while (true)
            {
                string cmd = Console.ReadLine();

                if (cmd == "stop" || cmd == "exit" || cmd == "quit")
                {
                    Environment.Exit(0);
                }
            }
        }

        static void ListenThread()
        {
            while (true)
            {
                listener.Listen(0);
                clients.Add(new Client(listener.Accept()));
            }
        }

        public static void ReciveDataFromClient(object cSocket)
        {
            Socket clientSocket = ((Client)cSocket).clientSocket;

            byte[] Buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    Buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(Buffer);

                    if (readBytes > 0)
                    {
                        try
                        {
                            Packet p = new Packet(Buffer);
                            ((Client)cSocket).username = p.SenderUsername;
                            if (p.Type == PacketType.Sync)
                            {
                                for (int i = 0; i < p.GameObjects.Count; i++)
                                {
                                    if (((Client)cSocket).id == p.GameObjects[i].owner)
                                    {
                                        for (int j = 0; j < objects.Count; j++)
                                        {
                                            if (p.GameObjects[i].networkID == objects[j].networkID)
                                            {
                                                objects[i] = p.GameObjects[i];
                                            }
                                        }
                                    }
                                }
                            }
                            else if (p.Type == PacketType.Create)
                            {
                                for (int i = 0; i < p.GameObjects.Count; i++)
                                    if (((Client)cSocket).id == p.GameObjects[i].owner)
                                        objects.Add(p.GameObjects[i]);
                            }
                            else if (p.Type == PacketType.Destroy)
                            {
                                for (int i = 0; i < p.GameObjects.Count; i++)
                                    if (((Client)cSocket).id == p.GameObjects[i].owner)
                                        objects.Remove(p.GameObjects[i]);
                            }
                            else
                            {
                                Console.WriteLine("Packet recived but unknown packet type");
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Packet recived but unknown packet type");
                        }
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Client Disconnected.");
                    CloseClientConnection(((Client)cSocket));
                }
            }
        }


        public static void SendMessageToCLient(int client, Packet p)
        {
            Program.clients[client].clientSocket.Send(p.ToBytes());
        }
        public static void SendMessageToCLient(Client client, Packet p)
        {
            client.clientSocket.Send(p.ToBytes());
        }
        public static void SendMessageToCLients(Packet p)
        {
            foreach (Client c in clients)
            {
                c.clientSocket.Send(p.ToBytes());
            }
        }
        private static void CloseClientConnection(Client c)
        {
            c.clientSocket.Close();
            clients.Remove(c);
            c.clientThread.Abort();
        }
    }
}