using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PacketConnector;

namespace Server
{
    class Client
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;
        public string username;

        public Client(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();

            clientThread = new Thread(Program.ReciveDataFromClient);
            clientThread.Start(this);
            Console.WriteLine("Client Accepted: " + ((IPEndPoint)clientSocket.RemoteEndPoint).Address);
            sendRegistrationPacketToCLient();
        }

        /// <summary>
        /// Send packet with client id to new client registreted client
        /// </summary>
        private void sendRegistrationPacketToCLient()
        {
            Packet p = new Packet(id, username, PacketType.Create);

            for (int i = 0; i < Program.objects.Count; i++)
                p.GameObjects.Add(Program.objects[i]);

            p.Message = "Welcome To Server";
            clientSocket.Send(p.ToBytes());
        }
    }
}