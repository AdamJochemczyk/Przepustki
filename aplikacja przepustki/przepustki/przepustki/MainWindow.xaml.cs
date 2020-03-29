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

namespace przepustki
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Osobowe(object sender, RoutedEventArgs e)
        {
            Osobowe osobowe = new Osobowe();
            osobowe.Show();
            this.Close();
        }

        private void Button_Click_Pojazd(object sender, RoutedEventArgs e)
        {
            Pojazd pojazd = new Pojazd();
            pojazd.Show();
            this.Close();
        }
    }
}
