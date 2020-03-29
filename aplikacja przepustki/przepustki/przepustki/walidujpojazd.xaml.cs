using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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
using System.IO;
using System.Windows.Media.Media3D;
using Brushes = System.Windows.Media.Brushes;

namespace przepustki
{
    /// <summary>
    /// Logika interakcji dla klasy walidujpojazd.xaml
    /// </summary>
    public partial class walidujpojazd : Window
    {
        private readonly string txt;
        readonly DateTime dateAndTime = DateTime.Now;
        private readonly string miejsca = "\n Pieczatka z bramy:                         Pieczątka odbiorcy: ";
        public string nrRejest;
        public string masa;
        public string marka;
        public string typ;
        public string osoby;
        public string firma;
        public string dowod;

        public walidujpojazd(string nrRej, string Masa, string Marka, string Typ, string Osoby, string Firma, string Dowod)
        {
            InitializeComponent();
            nrRejest = nrRej;
            masa = Masa;
            marka = Marka;
            typ = Typ;
            osoby = Osoby;
            firma = Firma;
            dowod = Dowod;
            string txt1 = "Sprawdz czy wprowadzone dane sa poprawne: \n";
            txt = " Numer rejestracyjny: " + nrRej + "\n Masa: " + Masa
                  + "\n Marka: " + Marka + "\n Typ pojazdu: " + Typ + "\n Ilosc osob na pokladzie:" + Osoby +
                  "\n Firma: " + Firma +
                  "\n Czas wjazdu: " + dateAndTime.Hour + ":" + dateAndTime.Minute + " \n Dowod wjazdu: " + Dowod;
            tbWal.Text = txt1 + txt;
        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(this.txt + miejsca, new Font("Arial", 15), new SolidBrush(System.Drawing.Color.Black),
                20, 20);
            System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            e.Graphics.DrawRectangle(blackPen, 20, 300, 200, 100);
            e.Graphics.DrawRectangle(blackPen, 400, 300, 200, 100);
        }

        private void Button_Powrot(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Wystaw(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            if (dlg.ShowDialog() == true)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintDocumentOnPrintPage;
                printDocument.Print();
            }

            this.Close();
        }

        private void Btn_dodaj(object sender, RoutedEventArgs e)
        {
            SqlConnection connection =
                new SqlConnection(
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\zapasowe, doskonczenia\aplikacja przepustki\przepustki\przepustki\Database1.mdf';Integrated Security=True");
            connection.Open();
            SqlCommand checkNumerRejestracyjny = connection.CreateCommand();
            checkNumerRejestracyjny.CommandType = CommandType.Text;
            checkNumerRejestracyjny.CommandText = "SELECT NrRej FROM Pojazdy WHERE NrRej='" + nrRejest + "'";
            object o = checkNumerRejestracyjny.ExecuteScalar();
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
                btnDodaj.Content = "Juz istnieje!";
            }
            else // nie istnieje
            {
                connection.Open();
                SqlCommand dodaj = connection.CreateCommand();
                dodaj.CommandType = CommandType.Text;
                dodaj.CommandText =
                    "insert Into Pojazdy (NrRej,Masa,Marka,Typ,Osoby,Firma,Dowod_wjazdu) values ('" + nrRejest + "','" +
                    masa + "'," + "'" + marka + "','" + typ + "','" + osoby + "','" + firma + "','" + dowod + "')";
                dodaj.ExecuteNonQuery();
                connection.Close();
                btnDodaj.Content = "Dodano!";
            }
        }
    }
}