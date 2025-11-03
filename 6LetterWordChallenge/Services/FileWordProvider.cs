namespace _6LetterWordChalenge.Services
{
    public class FileWordProvider : IWordProvider
    {
        private readonly string _filePath;

        public FileWordProvider(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(_filePath)
                       .Select(w => w.Trim().ToLower())
                       .Where(w => !string.IsNullOrWhiteSpace(w));
        }
    }
}