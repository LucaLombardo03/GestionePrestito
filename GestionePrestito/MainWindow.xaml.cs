using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionePrestito
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            InitializeComponent();
        }
        string combobox = "";
        string namefile = "file.csv";
        private void Calcola_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatePicker datePicker = new DatePicker();
                string nascita = null;
                if (rbfemmina.IsChecked == true)
                {
                    nascita = "nata";
                }
                if (rbmaschio.IsChecked == true)
                {
                    nascita = "nato";
                }
                combobox = cmbresidenza.Text;
                double importorichiesto = double.Parse(txtImportoRichiesto.Text);
                double rate = double.Parse(txtrate.Text);
                double percentuale = double.Parse(txtpercentuale.Text);
                double interesse = (importorichiesto / 100) * percentuale;
                double restituire = interesse + importorichiesto;
                double rata = restituire / rate;
                txtrestituire.Text = ($"{restituire} €");
                txtimportorata.Text = ($"{rata} €");
                lblfinale.Content = $"{txtCognome.Text} {txtNome.Text}, residente in {combobox} {nascita} il {dataPicker.SelectedDate.Value.ToShortDateString()}. Prestito di {importorichiesto} ad un tasso del {percentuale}% da restituire in {rate} rate da {rata} ciascuna, per un totale di {restituire} € \n";
                btnnuovo.IsEnabled = true;
            }
            catch
            {
                throw new Exception("Inserire i dati mancanti");
            }
        }

        private void Nuovo_Click(object sender, RoutedEventArgs e)
        {
            txtCognome.Clear();
            txtNome.Clear();
            rbfemmina.IsChecked = false;
            rbmaschio.IsChecked = false;
            txtImportoRichiesto.Clear();
            txtpercentuale.Clear();
            txtrate.Clear();
            lblfinale.Content = "";
            txtrestituire.Clear();
            txtimportorata.Clear();
            dataPicker.SelectedDate = null;
            cmbresidenza.SelectedItem = null;
        }

        private void Stampa_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(namefile, false, Encoding.UTF8))
            {
                sw.WriteLine("Cognome;Nome;Città;Sesso;Data di nascita;Importo richiesto (€);% di interesse; Numero di rate;Importo Rata (€);Totale € da restituire");
                if (rbfemmina.IsChecked == true)
                {
                    sw.WriteLine($"{txtCognome.Text}; {txtNome.Text};{combobox};F;{dataPicker.SelectedDate.Value.ToShortDateString()};{txtImportoRichiesto.Text};{txtpercentuale.Text};{txtrate.Text};{txtimportorata.Text};{txtrestituire.Text}");
                }
                else
                {
                    sw.WriteLine($"{txtCognome.Text}; {txtNome.Text};{combobox};M;{dataPicker.SelectedDate.Value.ToShortDateString()};{txtImportoRichiesto.Text};{txtpercentuale.Text};{txtrate.Text};{txtimportorata.Text};{txtrestituire.Text}");
                }
            }
        }
    }
}
