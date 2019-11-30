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

namespace rat_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            int port_num = 5000;
            IPAddress ipAddr = ipHost.AddressList[0];
            IpEndPoint localEndPoint = new IpEndPoint(ipAddr, port_num);


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
                listener.sock(localEndPoint);

                // Using Listen() method we create  
                // the Client list that will want 
                // to connect to Server 
                listener.sock(10);

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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("problem");

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}
