using _6LetterWordChalenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6LetterWordChalenge.Tests
{
    public class WordCombinerServiceTests
    {
        [Fact]
        public void Finds_TwoWordCombinations()
        {
            var mockProvider = new MockWordProvider(new[] { "foobar", "fo", "obar" });
            var service = new WordCombinerService(mockProvider);

            var result = service.FindCombinations(6).ToList();

            Assert.Single(result);
            Assert.Equal("foobar", result[0].Result);
            Assert.Equal(new[] { "fo", "obar" }, result[0].Parts);
        }

        private class MockWordProvider : IWordProvider
        {
            private readonly IEnumerable<string> _words;
            public MockWordProvider(IEnumerable<string> words) => _words = words;
            public IEnumerable<string> GetWords() => _words;
        }
    }
}
