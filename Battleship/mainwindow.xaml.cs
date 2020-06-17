using System.Diagnostics;
using System.Windows;

namespace Battleship
{
    public partial class mainwindow : Window
    {
        public static string player;
        public static string firstPlayer;
        public static string secondPlayer;
        public mainwindow()
        {
            InitializeComponent();
        }

        public void Start1playerGame(object sender, RoutedEventArgs e)
        {
            player = playerName.Text;
            Trace.WriteLine("Start1playerGame");
            OnePlayerGameWindow onePlayerGameWindow = new OnePlayerGameWindow();
            onePlayerGameWindow.Show();
            this.Close();
        }
         
        public void Start2playerGame(object sender, RoutedEventArgs e)
        {
            firstPlayer = firstPlayerName.Text;
            secondPlayer = secondPlayerName.Text;
            Trace.WriteLine("Start2playerGame");
            TwoPlayerGameWindow twoPlayerGameWindow = new TwoPlayerGameWindow();
            twoPlayerGameWindow.Show();
            this.Close();
        }
    }
}
