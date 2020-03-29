using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;

namespace przepustki
{
    /// <summary>
    /// Logika interakcji dla klasy Osobowe.xaml
    /// </summary>
    public partial class Osobowe : Window
    {
        public Osobowe()
        {
            InitializeComponent();
        }

        private void Button_Powrot(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Button_Dodaj_Bazy(object sender, RoutedEventArgs e)
        {
            string imie=tbImie.Text;
            string nazwisko = tbNazwisko.Text;
            string cel = tbCel.Text;
            string strefa = tbStrefa.Text;
            string numerdowodu = tbDowod.Text;
            WalidacjaOsoby wal = new WalidacjaOsoby(imie,nazwisko,strefa,numerdowodu,cel);
            wal.Show();
        }
        private void Button_Wystaw(object sender, RoutedEventArgs e)
        {
            string imie = tbImie.Text;
            string nazwisko = tbNazwisko.Text;
            string cel = tbCel.Text;
            string strefa = tbStrefa.Text;
            string numerdowodu = tbDowod.Text;
            WalidacjaOsoby wal = new WalidacjaOsoby(imie, nazwisko, strefa, numerdowodu, cel);
            wal.Show();
        }

        private void tbImie_GotFocus(object sender, RoutedEventArgs e)
        {
            tbImie.Clear();
        }

        private void tbNazwisko_GotFocus(object sender, RoutedEventArgs e)
        {
            tbNazwisko.Clear();
        }

        private void tbCel_GotFocus(object sender, RoutedEventArgs e)
        {
            tbCel.Clear();
        }

        private void tbStrefa_GotFocus(object sender, RoutedEventArgs e)
        {
            tbStrefa.Clear();
        }

        private void tbDowod_GotFocus(object sender, RoutedEventArgs e)
        {
            tbDowod.Clear();
        }

        private void Btn_Szukaj(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\zapasowe, doskonczenia\aplikacja przepustki\przepustki\przepustki\Database1.mdf';Integrated Security=True");
            connection.Open();
            SqlCommand checkNumerDowodu = connection.CreateCommand();
            checkNumerDowodu.CommandType = CommandType.Text;
            checkNumerDowodu.CommandText = "SELECT Numer_dowodu FROM Osoby WHERE Numer_dowodu='" + tbDowod.Text + "'";
            object o = checkNumerDowodu.ExecuteScalar();
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
                BtnSzukaj.Content = "Mamy to!";
                //komendy
                //imie
                connection.Open();
                SqlCommand getImie = connection.CreateCommand();
                getImie.CommandType = CommandType.Text;
                getImie.CommandText = "SELECT Imie FROM Osoby WHERE Numer_dowodu='" + tbDowod.Text + "'";
                string imie = getImie.ExecuteScalar().ToString();
                connection.Close();
                tbImie.Text = imie;
                //nazwisko
                connection.Open();
                SqlCommand getNazwisko = connection.CreateCommand();
                getNazwisko.CommandType = CommandType.Text;
                getNazwisko.CommandText = "SELECT Nazwisko FROM Osoby WHERE Numer_dowodu='" + tbDowod.Text + "'";
                string nazwisko = getNazwisko.ExecuteScalar().ToString();
                connection.Close();
                tbNazwisko.Text = nazwisko;
                //cel
                connection.Open();
                SqlCommand getCel = connection.CreateCommand();
                getCel.CommandType = CommandType.Text;
                getCel.CommandText = "SELECT Cel_podrozy FROM Osoby WHERE Numer_dowodu='" + tbDowod.Text + "'";
                string cel = getCel.ExecuteScalar().ToString();
                connection.Close();
                tbCel.Text = cel;
                //strefa poruszania
                connection.Open();
                SqlCommand getStrefa = connection.CreateCommand();
                getStrefa.CommandType = CommandType.Text;
                getStrefa.CommandText = "SELECT Strefa_Poruszania FROM Osoby WHERE Numer_dowodu='" + tbDowod.Text + "'";
                string strefa = getImie.ExecuteScalar().ToString();
                connection.Close();
                tbStrefa.Text = strefa;
            }
            else // nie istnieje
            {
                BtnSzukaj.Content = "Brak w bazie!";
            }
        }
    }
}
