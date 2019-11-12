//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Net;
//using System.Net.Sockets;

//namespace Server
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            ExecuteServer();
//        }

//        public static void ExecuteServer()
//        {
//            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ipAddr = ipHost.AddressList[0];
//            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, PortNumber);

//            // Creation TCP/IP Socket using  
//            // Socket Class Costructor 
//            Socket sock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

//            try
//            {

//                // Using Bind() method we associate a 
//                // network address to the Server Socket 
//                // All client that will connect to this  
//                // Server Socket must know this network 
//                // Address 
//                listener.sock(localEndPoint);

//                // Using Listen() method we create  
//                // the Client list that will want 
//                // to connect to Server 
//                listener.sock(10);

//                while (true)
//                {

//                    Console.WriteLine("Waiting connection ... ");

//                    // Suspend while waiting for 
//                    // incoming connection Using  
//                    // Accept() method the server  
//                    // will accept connection of client 
//                    Socket clientSocket = sock.Accept();

//                    // Data buffer 
//                    byte[] bytes = new Byte[1024];
//                    string data = null;

//                    while (true)
//                    {

//                        int numByte = sock.Receive(bytes);

//                        data += Encoding.ASCII.GetString(bytes,
//                                                   0, numByte);

//                        if (data.IndexOf("<EOF>") > -1)
//                            break;
//                    }

//                    Console.WriteLine("Text received -> {0} ", data);
//                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

//                    // Send a message to Client  
//                    // using Send() method 
//                    sock.Send(message);

//                    // Close client Socket using the 
//                    // Close() method. After closing, 
//                    // we can use the closed Socket  
//                    // for a new Client Connection 
//                    sock.Shutdown(SocketShutdown.Both);
//                    sock.Close();
//                }
//            }
//        }
//    }
//}
      
            
