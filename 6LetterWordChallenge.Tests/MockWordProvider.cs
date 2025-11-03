using _6LetterWordChallenge.Services;

namespace _6LetterWordChallenge.Tests
{
    public class MockWordProvider : IWordProvider
    {
        private readonly IEnumerable<string> _words;
        public MockWordProvider(IEnumerable<string> words) => _words = words;
        public IEnumerable<string> GetWords() => _words;
    }
}
