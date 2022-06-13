namespace Model
{
    public enum SourceType
    {
        Url,
        File
    }

    /// <summary>
    /// výsledek frekvenční analýzy pro jeden zdroj
    /// soubor / url
    /// </summary>
    public class FAResult
    {
        /// <summary>
        /// Zdroj textu.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Typ zdroje.
        /// </summary>
        public SourceType SourceType { get; set; }

        /// <summary>
        /// Výsledná frekvenční analýza slov.
        /// </summary>
        public Dictionary<string, int> Words { get; set; } = new Dictionary<string, int>();

        public override string ToString() => $"{SourceType} / {Source} - pocet: {Words?.Count}";

        public void PrintTenMostFrequentedWords()
        {
            Console.WriteLine($"File: {Source}");
            Console.WriteLine($"-------------------------------");

            var tenMost = Words.OrderByDescending(x => x.Value).Take(10);

            var i = 0;
            foreach (var t in tenMost)
            {
                i++;
                Console.WriteLine($"{i} {t.Key} - {t.Value}");
            }

            Console.WriteLine("");
        }
    }
}