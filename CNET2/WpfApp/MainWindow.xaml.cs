using Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
            Mouse.OverrideCursor = Cursors.Wait;
            var sw = Stopwatch.StartNew();

            txbInfo.Text = $"Načítám soubory..{Environment.NewLine}";
            frmMain.Title = "Paralelní načítátko";

            var files = Directory.GetFiles(filesDir, "*.txt");

            pbInfo.Value = 0;
            pbInfo.Maximum = files.Length;

            foreach (var file in files)
            {
                pbInfo.Value++;
                var words = FreqAnalysis.FreqAnalysisFromFile(file);

                txbInfo.Text += words.TenMostFrequentWordsOutput;
                txbInfo.Text += Environment.NewLine;

            }

            Mouse.OverrideCursor = null;
            sw.Stop();
            frmMain.Title = $"Trvalo to {sw.Elapsed.TotalMilliseconds}";
        }

        private void btnParallel1_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var sw = Stopwatch.StartNew();

            txbInfo.Text = $"Načítám soubory..{Environment.NewLine}";
            frmMain.Title = "Paralelní načítátko";

            var files = Directory.GetFiles(filesDir, "*.txt");

            pbInfo.Value = 0;
            pbInfo.Maximum = files.Length;

            IProgress<string> progressStr = new Progress<string>(message =>
            {
                pbInfo.Value++;

                txbInfo.Text += message;
                txbInfo.Text += Environment.NewLine;
            });

            // Zde použití Parallel
            Parallel.ForEach(files, file =>
            {
                var words = FreqAnalysis.FreqAnalysisFromFile(file);

                progressStr.Report(words.TenMostFrequentWordsOutput);
            });

            Mouse.OverrideCursor = null;
            sw.Stop();
            frmMain.Title = $"Trvalo to {sw.Elapsed.TotalMilliseconds}";
        }

        private async void btnParallel2_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var sw = Stopwatch.StartNew();

            txbInfo.Text = $"Načítám soubory..{Environment.NewLine}";
            frmMain.Title = "Paralelní načítátko";

            var files = Directory.GetFiles(filesDir, "*.txt");

            pbInfo.Value = 0;
            pbInfo.Maximum = files.Length;

            IProgress<string> progressStr = new Progress<string>(message =>
            {
                pbInfo.Value++;

                txbInfo.Text += message;
                txbInfo.Text += Environment.NewLine;
            });

            // Zde použití Parallel
            await Parallel.ForEachAsync(files, async (file, cancellationToken) =>
            {
                var words = FreqAnalysis.FreqAnalysisFromFile(file);

                progressStr.Report(words.TenMostFrequentWordsOutput);
            });

            Mouse.OverrideCursor = null;
            sw.Stop();
            frmMain.Title = $"Trvalo to {sw.Elapsed.TotalMilliseconds}";
        }
    }
}
