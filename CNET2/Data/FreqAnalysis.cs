using Model;

namespace Data
{
    public class FreqAnalysis
    {
        public static Dictionary<string, int> FreqAnalysisFromString(string input)
        {
            var result = new Dictionary<string, int>();

            var strAr = input.Split(Environment.NewLine);

            foreach (var item in strAr)
            {
                if (result.ContainsKey(item))
                {
                    result[item]++;
                }
                else
                {
                    result.Add(item, 1);
                }
            }

            return result;
        }

        public static async Task<FAResult> FreqAnalysisFromUrlAsync(string url)
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);

            var dict = FreqAnalysisFromString(content);

            return new FAResult
            {
                Source = url,
                SourceType = SourceType.Url,
                Words = dict,
            };
        }

        public static FAResult FreqAnalysisFromFile(string fileName)
        {
            var content = File.ReadAllText(fileName);

            var dict = FreqAnalysisFromString(content);

            return new FAResult
            {
                Source = fileName,
                SourceType = SourceType.File,
                Words = dict,
            };
        }
    }
}