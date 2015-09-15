using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;

namespace PacketConnector
{
    public enum PacketType
    {
        Create,
        Sync,
        Destroy
    }

    [Serializable]
    public class GameObject
    {
        public string name;
        public string tag;
        public string owner;
        public int networkID;
        public object[] components;

        public static GameObject Find(GameObject[] list, int networkID)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i].networkID == networkID)
                    return list[i];

            return null;
        }
    }

    [Serializable]
    public class Packet
    {
        public List<GameObject> GameObjects;
        public string Message = "";
        public PacketType Type;
        private string senderUsername;
        private string senderID;

        public string SenderUsername
        {
            get
            {
                return senderUsername;
            }
            private set
            {
                senderUsername = value;
            }
        }
        public string SenderID
        {
            get
            {
                return senderID;
            }
            private set
            {
                senderID = value;
            }
        }

        public Packet(string SenderID, string SenderUsername, PacketType Type)
        {
            GameObjects = new List<GameObject>();
            this.SenderID = SenderID;
            this.SenderUsername = SenderUsername;
            this.Type = Type;
        }

        public Packet(byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);

            Packet p = (Packet)bf.Deserialize(ms);
            ms.Close();
            this.GameObjects = p.GameObjects;
            this.SenderID = p.SenderID;
            this.SenderUsername = p.SenderUsername;
        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, this);
            byte[] bytes = ms.ToArray();
            ms.Close();

            return bytes;
        }
    }
}