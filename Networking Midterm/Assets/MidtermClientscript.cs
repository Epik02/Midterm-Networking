using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lec04
using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using TMPro;
using System.Threading;

public class MidtermClientscript : MonoBehaviour
{
    private Thread chatThread;
    public bool newMSG = false;
    String userText = "test";
    public string input = "est";
    public string inputCheck; //compared to input, if different we know a new msg has been entered, we then send it in update
    public GameObject myCube;
    public GameObject osText;
    private static byte[] outBuffer = new byte[512];
    private static IPEndPoint remoteEP;
    private static IPEndPoint ChatEP;

    private static EndPoint remoteClient;

    private static Socket clientSoc;

    private byte[] byteArray;
    private float[] ArrayExample;

    private Vector3 LastPos;

    public static void StartClient()
    {
        try
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            remoteEP = new IPEndPoint(ip, 8889);
            ChatEP = new IPEndPoint(ip, 8888);

            clientSoc = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);

            remoteClient = new IPEndPoint(IPAddress.Any, 0);

        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.ToString());
        }
    }
    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }

    public void StartChatClient()
    {
        byte[] buffer = new byte[512];
        //String userText;
        bool run = true;
        // Setup our end point (server)
        try
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            //IPAddress ip = Dns.GetHostAddresses("mail.bigpond.com")[0]; //DNS will translate a URL to an IP
            IPEndPoint serverEP = new IPEndPoint(ip, 52030);
            //Setup our client socket
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // Attempt a connection
                Console.WriteLine("Connecting to server...");
                client.Connect(serverEP);
                Console.WriteLine("Connected to IP: {0}", client.RemoteEndPoint.ToString());
                int recv = client.Receive(buffer);

                //newshit
                //newshit
                while (true)
                {
                    buffer = new byte[512];
                    client.Receive(buffer);
                    String clMSG = Encoding.ASCII.GetString(buffer);
                    Console.WriteLine(clMSG);
                    userText = input;
                    userText += " -From Client";
                    byte[] userMSG = Encoding.ASCII.GetBytes(userText);
                    client.Send(userMSG);

                    if (userText == "exit")
                    {
                        break;
                    }
                }
                // Loop
                // get user input
                // send to server
                Console.WriteLine("From Server: {0}", Encoding.ASCII.GetString(buffer, 0, recv));
                // Release socket resources
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (ArgumentNullException anexc)
            {
                Console.WriteLine("ArgumentNullException: {0}", anexc.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("Socket exception: {0}", se);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unnexpected exception: {0}", e);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: {0}", e);
        }
    }

        // Start is called before the first frame update
    void Start() {
        myCube = GameObject.Find("Cube");
        chatThread = new Thread(StartChatClient);
        chatThread.Start();
        //StartClient();
        Console.ReadKey();
        //StartChatClient();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(input);
        //if (myCube.transform.position != LastPos)
        //{
        //    ArrayExample = new float[] { myCube.transform.position.x,
        //    myCube.transform.position.y, myCube.transform.position.z };
        //    byteArray = new byte[ArrayExample.Length * 4];
        //    LastPos = myCube.transform.position;
        //    Buffer.BlockCopy(ArrayExample, 0, byteArray, 0, byteArray.Length);

        //    clientSoc.SendTo(byteArray, remoteEP);

        //    Debug.Log("Cube is Moving");
        //}





        //if (inputCheck.Equals(input) == false)
        //{
        //    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    //buffer = new byte[512];
        //    //client.Receive(buffer);
        //    //String clMSG = Encoding.ASCII.GetString(buffer);
        //    //Console.WriteLine(clMSG);
        //    userText = input;
        //    userText += " -From Client";
        //    byte[] userMSG = Encoding.ASCII.GetBytes(userText);
        //    client.Send(userMSG);
        //}
        //inputCheck = input;
    }
}
