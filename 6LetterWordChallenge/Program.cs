using _6LetterWordChallenge.Services;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var section = config.GetSection("WordCombiner");
int targetLength = section.GetValue<int>("TargetLength");
int maxWords = section.GetValue<int>("MaxWords");
string inputFile = section.GetValue<string>("InputFile") ?? "input.txt";


var inputPath = Path.Combine(AppContext.BaseDirectory, inputFile);

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Error: {inputFile} not found in application directory.");
    return;
}

IWordProvider wordProvider = new FileWordProvider(inputPath);
var service = new WordCombinerService(wordProvider);

var results = service.FindCombinations(targetLength, maxWords);

foreach (var combo in results)
{
    Console.WriteLine($"{string.Join("+", combo.Parts)}={combo.Result}");
}

var outputPath = Path.Combine(AppContext.BaseDirectory, "output.txt");
File.WriteAllLines(outputPath, results.Select(c => $"{string.Join("+", c.Parts)}={c.Result}"));
Console.WriteLine($"\nResults written to: {outputPath}");
Console.ReadLine();
