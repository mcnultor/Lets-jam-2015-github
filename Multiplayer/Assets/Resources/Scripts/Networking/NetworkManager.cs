using UnityEngine;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PacketConnector;

public class NetworkManager
{
    public static bool IsConnected
    {
        get
        {
            return isConnected;
        }
    }

    public static List<NetworkObject> no;
    private static bool isConnected = false;
    private static Socket socket;
    private static Thread thread;
    private string id;

    public static bool Connect(string ip, int port)
    {
        IPAddress ipAddress;
        if (IPAddress.TryParse(ip, out ipAddress))
        { 
            no = new List<NetworkObject>();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                socket.Connect(ipEndPoint);
                isConnected = true;
                UnityEngine.GameObject[] everything = Object.FindObjectsOfType<UnityEngine.GameObject>();
                for (int i = 0; i < everything.Length; i++)
                    everything[i].BroadcastMessage("OnConnectedToServer", SendMessageOptions.DontRequireReceiver);
                thread = new Thread(new ThreadStart(ReciveFromServer));
                thread.Start();
                return true;
            }
            catch (System.Exception ex)
            {
                UnityEngine.GameObject[] everything = Object.FindObjectsOfType<UnityEngine.GameObject>();
                for (int i = 0; i < everything.Length; i++)
                    everything[i].BroadcastMessage("OnFailedToConnect", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Error during connecting to server:\n" + ex + "\n\n");
                return false;
            }
        }
        else
        {
            UnityEngine.GameObject[] everything = Object.FindObjectsOfType<UnityEngine.GameObject>();
            for (int i = 0; i < everything.Length; i++)
                everything[i].BroadcastMessage("OnFailedToConnect", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Invalid IP Adress");
            return false;
        }
    }

    private static void ReciveFromServer()
    {
        byte[] buffer;
        int readBytes;
        while (true)
        {
            buffer = new byte[socket.SendBufferSize];
            readBytes = socket.Receive(buffer);

            if (readBytes > 0)
            {
                Packet p = new Packet(buffer);
                if (p.Type == PacketType.Sync)
                {
                    for (int i = 0; i < no.Count; i++)
                    {
                        for (int j = 0; j < p.GameObjects.Count; j++)
                        {
                            if (no[i].GetComponent<NetworkObject>().NetworkID == p.GameObjects[j].networkID)
                            {
                                if (Settings.Network.ID != p.SenderID)
                                    no[i].GetComponent<NetworkObject>().UnloadComponents(p.GameObjects[j].components);
                            }
                        }
                    }
                }
                else if (p.Type == PacketType.Create)
                {
                    for (int i = 0; i < p.GameObjects.Count; i++)
                    {
                        UnityEngine.GameObject g = new UnityEngine.GameObject();
                        g.name = p.GameObjects[i].name;
                        g.tag = p.GameObjects[i].tag;
                        g.AddComponent<NetworkObject>();
                        g.GetComponent<NetworkObject>().NetworkID = p.GameObjects[i].networkID;
                        g.GetComponent<NetworkObject>().Owner = p.GameObjects[i].owner;
                        g.GetComponent<NetworkObject>().UnloadComponents(p.GameObjects[i].components);
                    }
                }
                else if (p.Type == PacketType.Destroy)
                {
                    for (int i = 0; i < no.Count; i++)
                    {
                        for (int j = 0; j < p.GameObjects.Count; j++)
                        {
                            if (no[i].GetComponent<NetworkObject>().NetworkID == p.GameObjects[j].networkID)
                            {
                                no[i].GetComponent<NetworkObject>().RemoveObject();
                            }
                        }
                    }
                }
            }
        }
    }

    public static void Instantiate(UnityEngine.GameObject original, Vector3 position, Quaternion rotation)
    {
        UnityEngine.GameObject copy = new UnityEngine.GameObject(original.name);
        copy.transform.position = position;
        copy.transform.rotation = rotation;
    }

    public static void SendToServer(Packet p)
    {
        socket.Send(p.ToBytes());
    }

    public static void CloseConnection()
    {
        UnityEngine.GameObject[] everything = Object.FindObjectsOfType<UnityEngine.GameObject>();
        for (int i = 0; i < everything.Length; i++)
            everything[i].BroadcastMessage("OnDisconnectFromServer", SendMessageOptions.DontRequireReceiver);

        socket.Close();
        thread.Abort();
        isConnected = false;
        socket = null;
        Application.LoadLevel("Menu");
    }

    public void StateChange()
    {
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode && UnityEditor.EditorApplication.isPlaying)
        {
            Debug.Log("test");
        }
    }
}