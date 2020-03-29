using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace przepustki
{
    /// <summary>
    /// Logika interakcji dla klasy Pojazd.xaml
    /// </summary>
    public partial class Pojazd : Window
    {
        public Pojazd()
        {
            InitializeComponent();
            cbTyp.Items.Add("Ciezarowy");
            cbTyp.Items.Add("Osobowy");
            cbTyp.Items.Add("Dostawczy/Bus");
        }

        private void Button_Powrot(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Wystaw(object sender, RoutedEventArgs e)
        {
            string nrRej = tbNrRej.Text;
            string Masa = tbMasa.Text;
            string Marka = tbMarka.Text;
            string Typ = cbTyp.Text;
            string Osoby = tbOsoby.Text;
            string Firma = tbFirma.Text;
            string Dowod = tbDowod.Text;
            walidujpojazd walp = new walidujpojazd(nrRej, Masa, Marka, Typ, Osoby, Firma, Dowod);
            walp.Show();
        }


        private void tbDowod_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbDowod.Clear();
        }

        private void tbNrRej_GotFocus(object sender, RoutedEventArgs e)
        {
            tbNrRej.Clear();
        }

        private void tbMasa_GotFocus(object sender, RoutedEventArgs e)
        {
            tbMasa.Clear();
        }

        private void tbMarka_GotFocus(object sender, RoutedEventArgs e)
        {
            tbMarka.Clear();
        }

        private void tbOsoby_GotFocus(object sender, RoutedEventArgs e)
        {
            tbOsoby.Clear();
        }

        private void tbFirma_GotFocus(object sender, RoutedEventArgs e)
        {
            tbFirma.Clear();
        }

        private void tbDowod_GotFocus(object sender, RoutedEventArgs e)
        {
            tbDowod.Clear();
        }

        private void Button_Szukaj(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\zapasowe, doskonczenia\aplikacja przepustki\przepustki\przepustki\Database1.mdf';Integrated Security=True");
            connection.Open();
            SqlCommand checkNrRej = connection.CreateCommand();
            checkNrRej.CommandType = CommandType.Text;
            checkNrRej.CommandText = "SELECT NrRej FROM Pojazdy WHERE NrRej='" + tbNrRej.Text + "'";
            object o = checkNrRej.ExecuteScalar();
            int czyjest;
            if ((o == null) || (o == DBNull.Value))
            {
                czyjest = -1;
            }
            else
            {
                czyjest = 1;
            }
            connection.Close();
            if (czyjest > 0) //istnieje
            {
                btnSzukaj.Content = "Mamy to!";
                //komendy
                //marka
                connection.Open();
                SqlCommand getMarka = connection.CreateCommand();
                getMarka.CommandType = CommandType.Text;
                getMarka.CommandText = "SELECT Marka FROM Pojazdy WHERE NrRej='" + tbNrRej.Text + "'";
                string marka = getMarka.ExecuteScalar().ToString();
                connection.Close();
                tbMarka.Text = marka;
                //typ
                connection.Open();
                SqlCommand getTyp = connection.CreateCommand();
                getTyp.CommandType = CommandType.Text;
                getTyp.CommandText = "SELECT Typ FROM Pojazdy WHERE NrRej='" + tbNrRej.Text + "'";
                string typ = getTyp.ExecuteScalar().ToString();
                connection.Close();
                cbTyp.Text = typ;
                //firma
                connection.Open();
                SqlCommand getFirma = connection.CreateCommand();
                getFirma.CommandType = CommandType.Text;
                getFirma.CommandText = "SELECT Firma FROM Pojazdy WHERE NrRej='" + tbNrRej.Text + "'";
                string firma = getFirma.ExecuteScalar().ToString();
                connection.Close();
                tbFirma.Text = firma;
                //dowod
                connection.Open();
                SqlCommand getDowod = connection.CreateCommand();
                getDowod.CommandType = CommandType.Text;
                getDowod.CommandText = "SELECT Dowod_wjazdu FROM Pojazdy WHERE NrRej='" + tbNrRej.Text + "'";
                string dowod= getDowod.ExecuteScalar().ToString();
                connection.Close();
                tbDowod.Text =dowod;
            }
            else // nie istnieje
            {
                btnSzukaj.Content = "Brak w bazie!";
            }
        }
    }
}
