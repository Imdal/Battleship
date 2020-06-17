using System.Data;
using System.Windows;

namespace Battleship
{
    public partial class OnePlayerFinalWindow : Window
    {
        DataSet dataSet = new DataSet();

        public OnePlayerFinalWindow()
        {
            InitializeComponent();
            dataSet.ReadXml("OnePlayerState.xml");
            Results.ItemsSource = dataSet.Tables[0].DefaultView;
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            mainwindow mainwindow = new mainwindow();
            mainwindow.Show();
            this.Close();
        }
    }
}
