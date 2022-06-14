using System;
using System.IO;
using System.Net.Http;

namespace WpfApp
{
    public class WebLoad
    {
        public static (int? Size, string Url, bool Success) LoadUrl(string url, IProgress<string> progress)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);

            try
            {
                var content = httpClient.GetStringAsync(url).Result;
                progress.Report($"success: {url}");
                return (content.Length, url, true);
            }
            catch (Exception ex)
            {
                File.AppendAllText("./errors.txt", $"{DateTime.Now}\t{ex.Message}{Environment.NewLine}");

                progress.Report($"failed: {url}");
                return (null, url, false);
            }
        }
    }
}
