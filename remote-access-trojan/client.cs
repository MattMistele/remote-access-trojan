using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunClient();
        }

        static void RunClient()
        {
            try
            {

                // Establish the remote endpoint  
                // for the socket. 
                ipAddr = //fill this in 
                port_num = //fill this in 

                // Creation TCP/IP Socket using  
                // Socket Class Costructor 
                Socket sock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the remote  
                    // endpoint using method Connect() 
                    sock.Connect(localEndPoint);

                    // We print EndPoint information  
                    // that we are connected 
                    byte[] message = ("HI PLEASE WORK");
                    sock.Send(message);

                    //int byteRecv = sock.Receive(messageReceived);
                    //Console.WriteLine("Message from Server -> {0}", Enoding.ASCII.GetString(messageReceived, 0, byteRecv));

                    // Close Socket
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }
        }
    }
}

