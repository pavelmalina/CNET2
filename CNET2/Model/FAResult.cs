using System.Linq;

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

        public Dictionary<string, int> TenMostFrequentedWords() => Words.OrderByDescending(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value);

        public void PrintTenMostFrequentedWordsToConsole()
        {
            Console.WriteLine($"File: {Source}");
            Console.WriteLine($"-------------------------------");

            var tenMost = TenMostFrequentedWords();

            var i = 0;
            foreach (var t in tenMost)
            {
                i++;
                Console.WriteLine($"{i} {t.Key} - {t.Value}");
            }

            Console.WriteLine("");
        }

        public string GetTenMostFrequentWords()
        {
            var result = $"File: {Source}{Environment.NewLine}";
            result += $"---------------------------------------------{Environment.NewLine}";

            var tenMost = TenMostFrequentedWords();

            var i = 0;
            foreach (var t in tenMost)
            {
                i++;
                result += $"{i} {t.Key} - {t.Value}{Environment.NewLine}";
            }

            return result;
        }

        public string TenMostFrequentWordsOutput => GetTenMostFrequentWords();
    }
}