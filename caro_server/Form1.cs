using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace caro_server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        static ASCIIEncoding encoding = new ASCIIEncoding();
        Socket server;
        IPEndPoint ipe;
        List<Socket> listClient = new List<Socket>();
        // string myIP = "";
        Thread ketnoiClient;
        public void Connect()
        {

            ipe = new IPEndPoint(IPAddress.Parse("192.168.1.97"), 2016);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }
        private void rtb_Server_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Connect();
            ketnoiClient = new Thread(listen);
            ketnoiClient.IsBackground = true;
            ketnoiClient.Start();
        }

        public void listen()
        {
            server.Bind(ipe);
            server.Listen(3);
            while (true)
            {
                Socket sket = server.Accept();
                listClient.Add(sket);


                Thread clientProcess = new Thread(myThreadClient);
                clientProcess.IsBackground = true;
                clientProcess.Start(sket);

                rtb_Server.SelectionFont = new Font("Arial", 14, FontStyle.Bold);
                rtb_Server.SelectionColor = Color.Black;
                rtb_Server.AppendText("Chấp nhận kết nối từ " + sket.RemoteEndPoint.ToString() + "\n");
                rtb_Server.ScrollToCaret();
            }
        }
        public void myThreadClient(object obj)
        {

            Socket clientsk = (Socket)obj;
            while (true)
            {
                byte[] buff = new byte[1024];
                int rec = clientsk.Receive(buff);
                string clientcommand = encoding.GetString(buff);
                string[] tokens = clientcommand.Split(new Char[] { '|' });

                if (tokens[0] == "Name")
                {
                    foreach (Socket sk in listClient)
                    {
                        if (sk != clientsk)
                        {
                            sk.Send(buff, buff.Length, SocketFlags.None);//gui ten dang ki
                        }
                    }
                }
                if (tokens[0] == "Message")
                {
                    foreach (Socket sk in listClient)
                    {
                        if (sk != clientsk)
                        {
                            sk.Send(buff, buff.Length, SocketFlags.None);//gui du lieu
                        }

                    }
                }

            }
        }

    }
}
