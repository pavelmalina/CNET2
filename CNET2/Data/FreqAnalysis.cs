namespace Data
{
    public class FreqAnalysis
    {
        public static Dictionary<string, int> FreqAnalysisFromString(string input)
        {
            var result = new Dictionary<string, int>();
            var strAr = input.Split(new String[] { " ", "\t", "\r", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

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

        public static async Task<Dictionary<string, int>> FreqAnalysisFromUrlAsync(string url)
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);

            return FreqAnalysisFromString(content);
        }

        public static Dictionary<string, int> FreqAnalysisFromFile(string fileName)
        {
            var content = File.ReadAllText(fileName);

            return FreqAnalysisFromString(content);
        }
    }
}