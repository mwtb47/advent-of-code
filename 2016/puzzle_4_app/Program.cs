using System.Text.RegularExpressions;

namespace PuzzleFour
{
    class Program
    {
        /// <summary>
        /// Main function to calculate answers to part 1 and part 2.
        /// </summary>
        static void Main()
        {
            string[] codes = ReadCodes();
            IEnumerable<ParsedCode> parsedCodes = ParseCodes(codes);
            int answerOne = SolvePartOne(parsedCodes);
            string answerTwo = SolvePartTwo(parsedCodes);
            Console.WriteLine($"Part 1\nSum of number in valid codes - {answerOne}");
            Console.WriteLine($"Part 2\n{answerTwo}");
        }

        /// <summary>
        /// Read input codes.
        /// </summary>
        /// <returns>An array of string codes.</returns>
        static string[] ReadCodes()
        {
            return File.ReadAllLines("../puzzle_input/puzzle_4.txt");
        }

        /// <summary>
        /// Create a ParsedCode object for each code in the input.
        /// </summary>
        /// <param name="codes">Array of string codes.</param>
        /// <returns>An IEnumerable of ParsedCode objects.</returns>
        static IEnumerable<ParsedCode> ParseCodes(string[] codes)
        {
            return codes.Select(code =>
                {
                    List<string> letters =
                        Regex.Matches(code.Substring(0, code.IndexOf("[")), "[a-z]")
                        .Cast<Match>()
                        .Select(x => x.Value)
                        .ToList();

                    string inputTwo = code.Substring(0, code.LastIndexOf("-"));
                    int number = int.Parse(Regex.Match(code, @"\d+").Value);
                    string check = Regex.Match(code, @"\[(\w+)\]").Groups[1].Value;

                    return new ParsedCode(letters, inputTwo, number, check);
                }
            );
        }

        /// <summary>
        /// Solve part 1 of the puzzle.
        /// </summary>
        /// <param name="parsedCodes">IEnumerable of ParsedCode objects.</param>
        /// <returns>The sum of numbers from valid codes.</returns>
        static int SolvePartOne(IEnumerable<ParsedCode> parsedCodes)
        {
            int total = 0;
            foreach (ParsedCode code in parsedCodes)
            {
                string answer = code.FiveMostFrequent();
                if (answer == code.BracketString) total += code.Number;
            }
            return total;
        }

        /// <summary>
        /// Find the sector ID and number of the code containing information
        /// about the North Pole.
        /// </summary>
        /// <param name="parsedCodes">IEnumerable of ParsedCode objects.</param>
        /// <returns>The sector ID and number of the target code.</returns>
        /// <exception cref="Exception">
        /// Throw exception if none of the codes contain the answer.
        /// </exception>
        static string SolvePartTwo(IEnumerable<ParsedCode> parsedCodes)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz";
            foreach (ParsedCode code in parsedCodes)
            {
                string output = "";
                int shift = code.Number % 26;
                foreach (char c in code.InputTwo)
                {
                    if (c != '-')
                    {
                        output += alphabet[alphabet.IndexOf(c) + shift];
                    }
                    else
                    {
                        output += " ";
                    }
                }
                if (output.Contains("northpole"))
                {
                    return $"Sector ID for {output} - {code.Number}";
                }
            }
            throw new Exception("Answer not found for part 2.");
        }

        /// <summary>
        /// Class representing a code and the information containing in it.
        /// </summary>
        class ParsedCode
        {
            private List<string> letters = new List<string>();
            private string inputTwo = "";
            private int number;
            private string bracketString = "";

            public List<string> Letters
            {
                get { return letters; }
                set { letters = value; }
            }
            public string InputTwo
            {
                get { return inputTwo; }
                set { inputTwo = value; }
            }
            public int Number
            {
                get { return number; }
                set { number = value; }
            }
            public string BracketString
            {
                get { return bracketString; }
                set { bracketString = value; }
            }

            /// <summary>
            /// Count the number of times each letter appears.
            /// </summary>
            /// <returns>Dictionary containing letter frequencies.</returns>
            private Dictionary<string, int> CountLetters()
            {
                return Letters
                    .GroupBy(x => x)
                    .ToDictionary(g => g.Key, g => g.Count());
            }

            /// <summary>
            /// Get the five most frequently occuring letters. Any ties are broken by
            /// further sorting by alphabetical order.
            /// </summary>
            /// <returns>String with the five letters.</returns>
            public string FiveMostFrequent()
            {
                Dictionary<string, int> letterCounts = CountLetters();
                IEnumerable<(string, int)> letterCountsList = letterCounts.Select(x => (x.Key, x.Value));
                IEnumerable<(string, int)> orderedCounts = letterCountsList
                    .OrderByDescending(x => x.Item2)
                    .ThenBy(x => x.Item1)
                    .Take(5);
                string code = String.Join("", orderedCounts.Select(x => x.Item1));
                return code;
            }
            public ParsedCode(List<string> letters, string inputTwo, int number, string bracketString)
            {
                Letters = letters;
                InputTwo = inputTwo;
                Number = number;
                BracketString = bracketString;
            }
        }
    }
}