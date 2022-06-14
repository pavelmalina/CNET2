using Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp
{
    // Souhrnná statistika za všechny soubory
    // System.Collections.Concurrent => ConcurrentDictionary: TryAdd, TryUpdate

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

        private void btnTaskFirst_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var sw = Stopwatch.StartNew();

            var url1 = "https://www.seznam.cz";
            var url2 = "https://www.seznamzpravy.cz";
            var url3 = "https://www.ictpro.cz";
            pbInfo.Maximum = 3;
            pbInfo.Value = 0;

            IProgress<string> progressStr = new Progress<string>(message =>
            {
                pbInfo.Value++;

                txbInfo.Text += message;
                txbInfo.Text += Environment.NewLine;
            });

            var task1 = Task.Run(() => WebLoad.LoadUrl(url1, progressStr));
            var task2 = Task.Run(() => WebLoad.LoadUrl(url2, progressStr));
            var task3 = Task.Run(() => WebLoad.LoadUrl(url3, progressStr));

            Task.WaitAny(task1, task2, task3);

            txbInfo.Text = "Doběhl první task.";


            Mouse.OverrideCursor = null;
            sw.Stop();
            frmMain.Title = $"Trvalo to {sw.Elapsed.TotalMilliseconds}";
        }

        private async void btnTaskAll_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var sw = Stopwatch.StartNew();
            txbInfo.Text = string.Empty;

            string[] urls =
            {
                "https://www.seznam.cz",
                "https://www.seznamzpravy.cz",
                "https://www.ictpro.cz",
                "https://lidovky.cz",
                "https://www.novinky.cz",
                "https://www.bbc.co.uk",
                "https://eurowag.com",
            };

            pbInfo.Maximum = urls.Length;
            pbInfo.Value = 0;

            IProgress<string> progressStr = new Progress<string>(message =>
            {
                pbInfo.Value++;

                txbInfo.Text += message;
                txbInfo.Text += Environment.NewLine;
            });

            // Update GUI = udelat progress
            var tasks = new List<Task<(int? Size, string Url, bool Success)>>();
            foreach (string url in urls)
            {
                tasks.Add(Task.Run(() => WebLoad.LoadUrl(url, progressStr)));
            }

            // Blokující volání
            //Task.WaitAll(task1, task2, task3);
            //txbInfo.Text = "Doběhly všechny tasky.";

            // WhenFirst - WhenAll
            //var firstDone = await Task.WhenAny(task1, task2, task3);
            //txbInfo.Text = $"Doběhl první task {firstDone.Result}";
            // pouzitelne napr. redundantni volani na 2 servery (staci mi jeden z nich)

            var waitTask = await Task.WhenAll(tasks);
            txbInfo.Text += $"{Environment.NewLine}";

            //txbInfo.Text += $"Task 1: {url1} {task1.Result.Size}{Environment.NewLine}";
            //txbInfo.Text += $"Task 2: {url2} {task2.Result}{Environment.NewLine}";
            //txbInfo.Text += $"Task 3: {url3} {task3.Result}{Environment.NewLine}";
            //txbInfo.Text += $"{Environment.NewLine}";

            // Je lepší použí jako návratový typ "tuple" (int, string) -- tzv. složený typ

            foreach (var task in waitTask)
            {
                txbInfo.Text += "Task: ";
                txbInfo.Text += task.Success ? $"OK: {task.Url}: {task.Size}" : $"KO, error url: {task.Url}";
                txbInfo.Text += $"{Environment.NewLine}";
            }

            Mouse.OverrideCursor = null;
            sw.Stop();
            frmMain.Title = $"Trvalo to {sw.Elapsed.TotalMilliseconds}";
        }
    }
}
