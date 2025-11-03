namespace _6LetterWordChalenge.Models
{
    public class WordCombination
    {
        public string Result { get; }
        public IReadOnlyList<string> Parts { get; }

        public WordCombination(string result, IEnumerable<string> parts)
        {
            Result = result;
            Parts = parts.ToList();
        }
    }
}