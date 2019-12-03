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

        public static void ExampleServer()
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

                    // Data buffer 

                    string data = null;


                    /*   using (var output = File.Create("test.zip"))
                       {
                       Console.WriteLine("Client connected. Starting to receive the file");
                       // read the file in chunks of 1KB
                       var buffer = new byte[1024];
                       Console.WriteLine("stuff happening1");
                       int bytesRead = 1024;
                       Console.WriteLine("stuff happening2");
                       clientSocket.Receive(buffer);
                       Console.WriteLine("stuff happening3");
                       while (buffer != null )
                       {
                           Console.WriteLine("stuff happening4");
                           output.Write(buffer, 0, bytesRead);
                           Console.WriteLine("stuff happening5");
                       }
                       } */

                    byte[] bytes = new byte [1024];
                    File.Create("test.zip");
                    clientSocket.Receive(bytes);
                    File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "test.txt"), bytes);

                    Console.WriteLine("File Received");



                    //File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "test.txt"), bytes);
                    //File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "test.png"), bytes);
                    //ZipOutputStream stream = new ZipOutputStream(Path.Combine(Directory.GetCurrentDirectory(), "test.zip"));
                    //FileStream s1 = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "test.zip"), FileMode.OpenOrCreate);

                    //while (true)
                    //{

                    //    int numByte = clientSocket.Receive(bytes);

                    //    s1.Write(clientSocket.Receive(bytes), 0, 1048576);

                    //    if (data.IndexOf("<EOF>") > -1)
                    //        break;
                    //}

                    //stream.Close();

                    Console.WriteLine("Text received -> {0} ", data);
                    byte[] message = Encoding.ASCII.GetBytes("Test Server");

                  //  File.WriteAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "test.zip"), bytes);

                    // Send a message to Client  
                    // using Send() method 
                    clientSocket.Send(message);

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
