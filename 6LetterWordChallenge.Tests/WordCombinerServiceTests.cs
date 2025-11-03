using _6LetterWordChallenge.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace _6LetterWordChallenge.Tests
{
    public class WordCombinerServiceTests
    {
        [Fact]
        public void Finds_TwoWordCombinations()
        {
            var words = new[] { "foobar", "fo", "obar" };
            var service = new WordCombinerService(new MockWordProvider(words));

            var result = service.FindCombinations(6, 2).ToList();

            Assert.Single(result);
            Assert.Equal("foobar", result[0].Result);
            Assert.Equal(new[] { "fo", "obar" }, result[0].Parts);
        }

        [Fact]
        public void Finds_ThreeWordCombinations()
        {
            var words = new[] { "foobar", "foo", "b", "ar" };
            var service = new WordCombinerService(new MockWordProvider(words));

            var result = service.FindCombinations(6, 3).ToList();

            Assert.Contains(result, r => r.Result == "foobar" && r.Parts.SequenceEqual(new[] { "foo", "b", "ar" }));
        }

        [Fact]
        public void Does_Not_Return_Duplicates()
        {
            var words = new[] { "foobar", "fo", "obar", "fo", "obar" };
            var service = new WordCombinerService(new MockWordProvider(words));

            var result = service.FindCombinations(6, 2).ToList();

            Assert.Single(result);
        }

        [Fact]
        public void Ignores_Words_Shorter_Than_Target()
        {
            var words = new[] { "a", "b", "c", "foobar" };
            var service = new WordCombinerService(new MockWordProvider(words));

            var result = service.FindCombinations(6, 3).ToList();

            // Only 'foobar' matches length 6 and can be built
            Assert.All(result, r => Assert.Equal(6, r.Result.Length));
        }

        [Fact]
        public void Returns_Empty_When_No_ValidCombinations()
        {
            var words = new[] { "abc", "xyz", "foo" };
            var service = new WordCombinerService(new MockWordProvider(words));

            var result = service.FindCombinations(6, 2).ToList();

            Assert.Empty(result);
        }
        [Fact]
        public void Reads_ConfigValues_From_AppSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var section = config.GetSection("WordCombiner");
            var targetLength = section.GetValue<int>("TargetLength");

            Assert.True(targetLength > 0);
        }
    }
}