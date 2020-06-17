using System.Data;
using System.Windows;

namespace Battleship
{
    public partial class TwoPlayerFinalWindow : Window
    {
        DataSet dataSet = new DataSet();
        public TwoPlayerFinalWindow()
        {
            InitializeComponent();
            dataSet.ReadXml("TwoPlayerState.xml");
            Results.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        public void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            mainwindow mainwindow = new mainwindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
