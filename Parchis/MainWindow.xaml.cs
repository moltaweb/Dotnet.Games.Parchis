using Parchis.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parchis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // content.Content = new BoardGameControl();

            var game = new GameWindow();
            game.Show();
            game.Activate();
            game.Topmost = true;
            game.Focus();
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            var game = new GameWindow();
            game.Show();
        }

        private void BtnLoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
