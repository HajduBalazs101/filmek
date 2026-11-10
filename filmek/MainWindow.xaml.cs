using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace filmek
{
    public class Film
    {
        public string Cim { get; set; }
        public string Mufaj { get; set; }
        public int Hossz { get; set; }
        public double Ertekeles { get; set; }
        public Film(string cim, string mufaj, int hossz, double ertekeles)
        {
            Cim = cim;
            Hossz = hossz;
            Mufaj = mufaj;
            Ertekeles = ertekeles;
        }
    }

    public partial class MainWindow : Window
    {
        List<Film> filmek = new List<Film>();

                
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void betolt(object sender, RoutedEventArgs e)
        {
            filmek.Clear();
            filmek.Add(new Film("Csillagok háborúja", "sci-fi", 121, 8.6));
            filmek.Add(new Film("Titanic", "dráma", 195, 7.8));
            filmek.Add(new Film("A Gyűrűk Ura", "fantasy", 201, 8.8));
            filmek.Add(new Film("Mátrix", "sci-fi", 136, 8.7));
            filmek.Add(new Film("Shrek", "animáció", 90, 7.9));
            dgrid.ItemsSource = filmek;
            dgrid.Items.Refresh();
        }

        private void hanyfilm(object sender, RoutedEventArgs e)
        {
            if (dgrid.ItemsSource == null)
            {
                MessageBox.Show("Nincs betöltve film lista!");
            }
            else
            { 
                MessageBox.Show($"{filmek.Count()} db film van");
            }   
        }

        private void leghosszabb(object sender, RoutedEventArgs e)
        {
            if (dgrid.ItemsSource == null)
            {
                MessageBox.Show("Nincs betöltve film lista!");
            }
            else
            { 
            var hossz = filmek.OrderByDescending(f => f.Hossz).Take(1).ToList();
            dgrid.ItemsSource = hossz;
            }
        }

        private void atlag(object sender, RoutedEventArgs e)
        {
            if(dgrid.ItemsSource == null)
            {
                MessageBox.Show("Nincs betöltve film lista!");

            }
            else 
            { 
                var atlag = filmek.Average(f => f.Ertekeles);
                MessageBox.Show($"A filmek átlag értékelése: {atlag}");
            }
        }

        private void scifi(object sender, RoutedEventArgs e)
        {
            var scifik = filmek.Where(f => f.Mufaj == "sci-fi").ToList();
            dgrid.ItemsSource = scifik;
        }

        private void cimek(object sender, RoutedEventArgs e)
        {
            if (dgrid.ItemsSource == null)
            {
                MessageBox.Show("Nincs betöltve film lista!");
            }
            else
            { 
            var cimek = filmek.Select(f => f.Cim).ToList();
            MessageBox.Show($"Könyvek címei: {string.Join(", ", cimek)}");
            }
        }

        private void cimszerint(object sender, RoutedEventArgs e)
        {
            var rendezett = filmek.OrderBy(f => f.Cim).ToList();
            dgrid.ItemsSource = rendezett;
        }

        private void dgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ert = filmek.OrderByDescending(f => f.Hossz).Take(1).ToList();
            var kivalasztott = (Film)dgrid.SelectedItem;
            pbar.Minimum = 0;
            pbar.Maximum = ert[0].Hossz;
            pbar.Value = kivalasztott.Hossz;

            MessageBox.Show($"Kiválasztott film: \n{kivalasztott.Cim} \nÉrtékelés: {kivalasztott.Ertekeles}");
        }
    }
}