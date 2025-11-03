using _6LetterWordChallenge.Models;

namespace _6LetterWordChallenge.Services
{
    public class WordCombinerService
    {
        private readonly IWordProvider _wordProvider;

        public WordCombinerService(IWordProvider wordProvider)
        {
            _wordProvider = wordProvider;
        }

        public IEnumerable<WordCombination> FindCombinations(int targetLength, int maxWords)
        {
            var words = _wordProvider.GetWords().ToList();
            var wordSet = new HashSet<string>(words);
            var seenCombos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var combo in CombineWords(words, targetLength, maxWords))
            {
                var combined = string.Concat(combo);

                if (combined.Length == targetLength && wordSet.Contains(combined))
                {
                    var key = string.Join("+", combo);
                    if (seenCombos.Add(key))
                        yield return new WordCombination(combined, combo);
                }
            }
        }

        private IEnumerable<List<string>> CombineWords(List<string> words, int targetLength, int remaining, List<string>? prefix = null)
        {
            prefix ??= new List<string>();
            int currentLength = prefix.Sum(w => w.Length);

            // Prune if we already exceed the target length
            if (currentLength > targetLength)
                yield break;

            // Base case
            if (remaining == 0)
            {
                if (currentLength == targetLength)
                    yield return new List<string>(prefix);
                yield break;
            }

            // Recursive case
            foreach (var w in words)
            {
                int newLength = currentLength + w.Length;
                if (newLength > targetLength)
                    continue; // prune early before going deeper

                prefix.Add(w);
                foreach (var next in CombineWords(words, targetLength, remaining - 1, prefix))
                    yield return next;
                prefix.RemoveAt(prefix.Count - 1);
            }
        }
    }
}