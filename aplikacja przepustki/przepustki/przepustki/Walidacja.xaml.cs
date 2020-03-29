using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace przepustki
{
    /// <summary>
    /// Logika interakcji dla klasy Walidacja.xaml
    /// </summary>
    public partial class WalidacjaOsoby : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='E:\zapasowe, doskonczenia\aplikacja przepustki\przepustki\przepustki\Database1.mdf';Integrated Security=True");
        private readonly string txt;
        private readonly string miejsca = "\n Pieczatka z bramy:                                 Pieczątka odbiorcy: ";

        //to INSERT
        public string imie;
        public string nazwisko;
        public string strefa;
        public string numerdowodu;
        public string cel;
        public WalidacjaOsoby(string iimie,string inazwisko, string istrefa, string inumerdowodu, string icel)
        {
            InitializeComponent();
            DateTime data=DateTime.Now;
            imie = iimie;
            nazwisko = inazwisko;
            strefa = istrefa;
            numerdowodu = inumerdowodu;
            cel = icel;
            string txt1 = "Sprawdz czy wprowadzone dane sa poprawne \n";
            txt= " Imie: " + iimie + "\n Nazwisko: " + inazwisko
                        + "\n Strefa: " + istrefa + "\n Numer dowodu: " + inumerdowodu + "\n Cel podrozy:" + icel+"czas: "+data.Hour+":"+data.Minute;
            BtnWystaw.IsEnabled = true;
            ButtonWpiszDoBazy.IsEnabled = true;
            if (inumerdowodu.Length != 9)
            {
                txt += "\n Numer dowodu jest nieprawidlowy!";
                BtnWystaw.IsEnabled = false;
                ButtonWpiszDoBazy.IsEnabled = false;
            }
            else
            {
                string letterpart = inumerdowodu.Substring(0, 3);
                string numerpart = inumerdowodu.Substring(3);
                foreach (char c in letterpart)
                {
                    if (!Char.IsLetter(c))
                    {
                        txt += "\n Czesc znakowa numeru dowodu jest nieprawidlowa!";
                        BtnWystaw.IsEnabled =false;
                        ButtonWpiszDoBazy.IsEnabled = false;
                        break;
                    }
                }

                foreach (char c in numerpart)
                {
                    if (!Char.IsDigit(c))
                    {
                        txt += "\n Czesc numeryczna numeru dowodu jest nieprawidlowa!";
                        BtnWystaw.IsEnabled = false;
                        ButtonWpiszDoBazy.IsEnabled = false;
                        break;
                    }
                }
            }
            tbWaliduj.Text = txt1+txt;
        }
        
        private void Button_Powrot(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(this.txt + miejsca, new Font("Arial", 15), new SolidBrush(System.Drawing.Color.Black), 20, 20);
            System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            e.Graphics.DrawRectangle(blackPen, 20, 300, 200, 100);
            e.Graphics.DrawRectangle(blackPen, 400, 300, 200, 100);
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
        private void Button_DoBazy(object sender, RoutedEventArgs e)
        {
            connection.Open();
            SqlCommand checkNumerDowodu = connection.CreateCommand();
            checkNumerDowodu.CommandType = CommandType.Text;
            checkNumerDowodu.CommandText = "SELECT Numer_dowodu FROM Osoby WHERE Numer_dowodu='"+numerdowodu+"'";
            object o=checkNumerDowodu.ExecuteScalar();
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
            if (czyjest >0) //istnieje
            {
                ButtonWpiszDoBazy.Content = "Juz istnieje!";
            }
            else // nie istnieje
            {
                connection.Open();
                SqlCommand dodaj = connection.CreateCommand();
                dodaj.CommandType = CommandType.Text;
                dodaj.CommandText =
                    "insert Into [Osoby] (Imie,Nazwisko,Strefa_poruszania,Cel_podrozy,Numer_dowodu) values ('" + imie + "','" + nazwisko + "'," +
                    "'" + strefa + "','" + cel + "','" + numerdowodu + "')";
                dodaj.ExecuteNonQuery();
                connection.Close();
                ButtonWpiszDoBazy.Content = "Dodano!";
            }
        }
    }
}
