using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Labb_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Bokningar.basBokning> bokningar = new List<Bokningar.basBokning>();
        public MainWindow()
        {
            InitializeComponent();
            RefreshList();
        }

        private void btnBoka_Click(object sender, RoutedEventArgs e)
        {
            if (txtNamn.Text != "")
            {
                if (dtpDatum.Text != "")
                {
                    var namnCheck = new Regex(@"^[A-Öa-ö .]{1,30}$");
                    if (namnCheck.IsMatch(txtNamn.Text))
                    {
                        Bokningar.Bokning nyBokning = new Bokningar.Bokning(txtNamn.Text, dtpDatum.Text, cmbTid.Text, Convert.ToInt32(cmbBord.Text));
                        if (new Metoder().isFörMångaBokningar(bokningar, nyBokning) == false)
                        {
                            bokningar.Add(nyBokning);
                            using (StreamWriter sw = new StreamWriter("Bokningslista.txt", true))
                            {
                                sw.WriteLine(nyBokning.SparaInfo());
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Bara bokstäver godtas i namnfältet.");
                    }
                }
                else
                {
                    MessageBox.Show("Datumet är inte valt!");
                }

            }
            else
            {
                MessageBox.Show("Namnfältet är inte ifyllt!");
            }
        }

        private async void btnLäsIn_Click(object sender, RoutedEventArgs e)
        {
            DisableButtons();
            btnLäsIn.Content = "Läser in...";
            lbxBokade.Items.Clear();
            Task läsInFil = läsInTxt();
            await läsInFil;
            DisableButtons();
            btnLäsIn.Content = "Visa Bokningar";
        }

        private void btnAvboka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int val = lbxBokade.SelectedIndex;
                lbxBokade.Items.RemoveAt(val);
                bokningar.RemoveAt(val);
                RefreshTxt();
                RefreshList();
            }
            catch
            {
                MessageBox.Show("Ingenting i bokningslistan är vald.");
            }
        }








        //#########     Metoder     #########
        public void RefreshList()
        {
            //Läser in Bokningslista.txt och lägger bokningarna i bokningar listan.
            bokningar.Clear();
            string lines = "";
            using (StreamReader sr = new StreamReader("Bokningslista.txt"))
            {
                while ((lines = sr.ReadLine()) != null)
                {
                    var array = lines.Split("###");
                    Bokningar.Bokning nyBokning = new Bokningar.Bokning(array[0], array[1], array[2], Convert.ToInt32(array[3]));
                    bokningar.Add(nyBokning);
                }
            }
        }
        public void RefreshTxt()
        {
            //Läser in bokningar listan och lägger in bokningarna i Bokningslista.txt
            using (StreamWriter sw = new StreamWriter("Bokningslista.txt"))
            {
                foreach (var item in bokningar)
                {
                    sw.WriteLine(item.SparaInfo());
                }
            }
        }
        public void DisableButtons() //Disablar eller enablar knapparna
        {
            if (btnLäsIn.IsEnabled == false || btnBoka.IsEnabled == false || btnAvboka.IsEnabled == false)
            {
                btnLäsIn.IsEnabled = true;
                btnBoka.IsEnabled = true;
                btnAvboka.IsEnabled = true;
            }
            else
            {
                btnLäsIn.IsEnabled = false;
                btnBoka.IsEnabled = false;
                btnAvboka.IsEnabled = false;
            }
        }








        //#########     Tasks     #########
        async Task läsInTxt()
        {
            string lines = "";
            using (StreamReader sr = new StreamReader("Bokningslista.txt"))
            {
                while ((lines = sr.ReadLine()) != null)
                {
                    await Task.Delay(350);
                    var array = lines.Split("###");
                    string text = $"{array[0]}, {array[1]}, kl.{array[2]}, Bord: {array[3]}";
                    lbxBokade.Items.Add(text);
                }
            }
            MessageBox.Show("Inläsning klar.");
        }
    }
}
