
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Caro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        static ASCIIEncoding Encoding = new ASCIIEncoding();
        Socket client;
        IPEndPoint ipe;
        Thread ketnoi;
        bool online = false;
        bool offline = true;

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            ketnoi = new Thread(new ThreadStart(KetNoiDenServer));
            ketnoi.IsBackground = true;
            ketnoi.Start();
            online = true;
            offline = false;
        }

        public void KetNoiDenServer()
        {
            ipe = new IPEndPoint(IPAddress.Parse("192.168.1.97"), 2016);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Connect(ipe);

            Thread langnghe = new Thread(LangNgheDuLieu);
            langnghe.IsBackground = true;
            langnghe.Start(client);
        }

        public void LangNgheDuLieu(object obj)
        {
            Socket sket = (Socket)obj;
            while (true)
            {
                byte[] buff = new byte[1024];
                int recv = client.Receive(buff);
                MaHoaDuLieu(buff);
            }
        }

        private void MaHoaDuLieu(byte[] buff)
        {
            string command = System.Text.Encoding.ASCII.GetString(buff);
            string[] tokens = command.Split(new Char[] { '|' });
            if (tokens[0] == "Name")
            {
                string messageinform = "Player " + tokens[1] + " is connected!!!\n";
                Dispatcher.Invoke(new Action(() =>
                {
                    _chatbox.AppendText(messageinform);
                }));
            }

            if (tokens[0] == "Message")//nếu gửi chuỗi tin nhắn
            {
                string Message = tokens[1] + ":";
                Message += tokens[2] + "   (";
                Message += DateTime.Now.ToString("hh:mm  dd/MM/yyyy)\n");//ngày giờ hệ thống
                Dispatcher.Invoke(new Action(() =>
                {
                    _chatbox.AppendText(Message);
                }));

            }     
        }
        //------------------------------------------------------------------------------------------------------
        bool vs_player = false;

        bool vs_Com = false;

        AI point_AI = new AI();

        public Player CurrPlayer = Player.Player1;

        private void VS_Computer_Click(object sender, RoutedEventArgs e)
        {
            vs_player = false;
            vs_Com = true;
            CurrPlayer = Player.Computer;//chuyen quyen choi cho may
            Player_with_Com();
            
        }

        private void VS_Human_Click(object sender, RoutedEventArgs e)
        {
            vs_player = true;
            vs_Com = false;
  
        }

        public void Draw_ChessBoard()//tạo các button để tạo bàn cờ
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Button btChess = new Button();
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            btChess.Background = Brushes.White;
                        }
                        else
                        {
                            btChess.Background = Brushes.Brown;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            btChess.Background = Brushes.Brown;
                        }
                        else
                        {
                            btChess.Background = Brushes.White;
                        }

                    }
                    Chess.Children.Add(btChess);
                    Grid.SetRow(btChess, i);
                    Grid.SetColumn(btChess, j);
                    btChess.Click += Caro_Click;//tao su kien click cho button

                }
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Draw_ChessBoard();    
        }
        //-------------------------------------------
      public  void Tick(Button btn, int Type)//nước cờ
        {
            Image img = new Image();
            int column = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);
            if (Type == 1 || Type == 3)
            {
                img.Source = new BitmapImage(new Uri(@"../image/black.png", UriKind.Relative));
                CurrPlayer = Player.Player2;
                if (Type == 1)
                {
                    point_AI.Array[row, column] = 1;
                }
                if (Type == 3)
                {
                    point_AI.Array[row, column] = 3;
                }
            }
            if (Type == 2)
            {
                img.Source = new BitmapImage(new Uri(@"../image/white.png", UriKind.Relative));
                if (vs_player)
                {
                    CurrPlayer = Player.Player1;
                }
                else if (vs_Com)
                {
                    CurrPlayer = Player.Computer;
                }

                point_AI.Array[row, column] = 2;//nguoi choi 2
            }
            StackPanel stackpn = new StackPanel();
            stackpn.Orientation = Orientation.Horizontal;
            stackpn.Margin = new Thickness();
            stackpn.Children.Add(img);
            btn.Content = stackpn;
        }

        private void Caro_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (vs_player)
            {
                Player_with_player(btn);//người chơi vs người
            }
            else if (vs_Com)
            {
                Player_with_Com(btn);//người chơi vs máy
            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            _name.Clear();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            byte[] buff = new byte[1024];
            string buffmess = "Message|" + _name.Text + '|' + _message.Text  + '|';
            buff = Encoding.GetBytes(buffmess);
            client.Send(buff, buff.Length, SocketFlags.None);
            //------------------------------------------------

            string name = _name.Text.ToString();
            string message = _message.Text.ToString();
            string date = DateTime.Now.ToString("hh:mm      dd/MM/yyyy)\n");
            string chat = "";
            chat += name;
            chat += ": ";
            chat += message;
            chat += "\t(";
            chat += date;
                _chatbox.AppendText(chat);
                _message.Clear();            
        }
        //-----------------------------------------------------------
        public void Player_with_player(Button btn)//2 player choi luan phien
        {
            Check check = new Check();
            int Type = (int)CurrPlayer;
            Tick(btn, Type);
            int column = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);

            if (check.five_row(point_AI.Array, row, column) || check.five_column(point_AI.Array, row, column)
                || check.five_diagonal_down(point_AI.Array, row, column) || check.five_diagonal_up(point_AI.Array, row, column))
            {

                if (point_AI.Array[row, column] == 1)
                {
                    MessageBox.Show("Player 1 win!!!");
                }
                else if (point_AI.Array[row, column] == 2)
                {
                    MessageBox.Show("Player 2 win!!!");
                }
                Chess.Children.Clear();
                Draw_ChessBoard();//tao lai ban co moi
                point_AI.Array = new int[12, 12];
                return;
            }
        }

        public void Player_with_Com()//máy chơi vs người
        {
            Check check = new Check();
           
            
           if (Chess_clear()) //nếu bàn cờ trống
            {
                 UIElement element = Chess.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetColumn(e) ==  5&& Grid.GetRow(e) == 5);
                Tick((Button)element, 3);
            }
            
           else
           {
               Point point = point_AI.Find_moveoptimal(); //Xác định vị trí của button cần đánh
               int x = int.Parse(point.X.ToString());
               int y = int.Parse(point.Y.ToString());
               UIElement element = Chess.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetColumn(e) == y && Grid.GetRow(e) ==x );//lấy button trên grid với column và row tìm được
               Tick((Button)element, 3);//đánh cờ

               if (check.five_row(point_AI.Array, x, y) || check.five_column(point_AI.Array, x, y)//kiểm tra thắng
                || check.five_diagonal_down(point_AI.Array, x, y) || check.five_diagonal_up(point_AI.Array, x, y))
               {
                   MessageBox.Show("You lost!!!Computer win!!!");//máy thắng
                   Chess.Children.Clear();
                   point_AI.Array = new int[12, 12];
                   Draw_ChessBoard();//tao lai ban co moi
                   return;
               }
           }
            
            
        }

        public void Player_with_Com(Button btn)
        {
            Check check = new Check();
            Tick(btn, 2);//người chơi đánh cờ với máy
            int column = Grid.GetColumn(btn);
            int row = Grid.GetRow(btn);

            if (check.five_row(point_AI.Array, row, column) || check.five_column(point_AI.Array, row, column)
                || check.five_diagonal_down(point_AI.Array, row, column) || check.five_diagonal_up(point_AI.Array, row, column))
            {
                MessageBox.Show("Congratulation!!! Player win Computer!!!");//nguoi thang
                Chess.Children.Clear();
                Draw_ChessBoard();//tao lai ban co moi
                point_AI.Array = new int[12, 12];
                return;
            }
            else
            {

                Player_with_Com();
            }
        }

        public bool Chess_clear()
        {
            for (int i=0; i<12; i++)
                for(int j=0; j<12; j++)
                {
                    if(point_AI.Array[i,j]==1||point_AI.Array[i,j]==2||point_AI.Array[i,j]==3)
                    {
                        return false;
                    }
                }
            return true;
        }



        public bool CheckForIllegalCrossThreadCalls { get; set; }

        public SocketFlags None { get; set; }
    }
    
}
