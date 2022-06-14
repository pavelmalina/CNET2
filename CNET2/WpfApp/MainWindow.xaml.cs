using Data;
using Model;
using System.IO;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filesDir = @"C:\Users\StudentEN\source\repos\Malina\BigFiles\";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadFiles_Click(object sender, RoutedEventArgs e)
        {
            txbInfo.Text = "Načítám soubory..";
            frmMain.Title = "Paralelní načítátko";

            var files = Directory.GetFiles(filesDir, "*.txt");

            foreach (var file in files)
            {
                var words = FreqAnalysis.FreqAnalysisFromFile(file);

                txbInfo.Text += words.TenMostFrequentWordsOutput;
            }
        }
    }
}
