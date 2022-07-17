using System.Text.RegularExpressions;

namespace PuzzleSeven
{
    class Program
    {
        /// <summary>
        /// Main function to calculate answers to part 1 and part 2.
        /// </summary>
        static void Main()
        {
            string[] instructions = ReadInstructions();
            Screen screen = CreateScreen();
            screen = ApplyInstructions(instructions, screen);
            int totalLitPixels = screen.TotalLitPixels();
            string screenPattern = screen.PrintPattern();
            Console.WriteLine($"Part 1\nTotal number of lit pixels - {totalLitPixels}\n");
            Console.WriteLine($"Part 2\n{screenPattern}");

        }

        /// <summary>
        /// Read in instructions.
        /// </summary>
        /// <returns>Array of instructions.</returns>
        static string[] ReadInstructions()
        {
            return File.ReadAllLines("../puzzle_input/puzzle_8.txt");
        }

        /// <summary>
        /// Create a Screen object which contains information about pixel
        /// locations and their on/off status.
        /// </summary>
        /// <returns>A Screen object.</returns>
        static Screen CreateScreen()
        {
            Dictionary<(int, int), int> pixels = new Dictionary<(int, int), int>();
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 50; col++)
                {
                    pixels.Add((row, col), 0);
                }
            }
            return new Screen(pixels);
        }

        /// <summary>
        /// Apply the instructions to the Screen objects.
        /// </summary>
        /// <param name="instructions">Array of instructions.</param>
        /// <param name="screen">
        /// The Screen object to apply instructions to.
        /// </param>
        /// <returns>
        /// Screen object after instructions have been applied.
        /// </returns>
        /// <exception cref="Exception">
        /// If an unknown instructions is given.
        /// </exception>
        static Screen ApplyInstructions(string[] instructions, Screen screen)
        {
            foreach (string i in instructions)
            {
                List<int> numbers = Regex.Matches(i, "[0-9]+")
                    .Cast<Match>()
                    .Select(x => int.Parse(x.Value))
                    .ToList();

                if (i.Contains("rect"))
                {
                    int a = numbers[0];
                    int b = numbers[1];
                    screen.CreateRectangle(a, b);
                }
                else if (i.Contains("rotate column"))
                {
                    int column = numbers[0];
                    int n = numbers[1];
                    screen.RotateColumn(column, n);
                }
                else if (i.Contains("rotate row"))
                {
                    int row = numbers[0];
                    int n = numbers[1];
                    screen.RotateRow(row, n);
                }
                else
                {
                    throw new Exception("Invalid input");
                }
            }
            return screen;
        }

        /// <summary>
        /// Class to represent a screen with pixels.
        /// </summary>
        class Screen
        {
            private Dictionary<(int, int), int> pixels = new Dictionary<(int, int), int>();

            public Dictionary<(int, int), int> Pixels
            {
                get { return pixels; }
                set { pixels = value; }
            }

            /// <summary>
            /// Turn on all pixels in the row 1 to a and column 1 to b.
            /// </summary>
            /// <param name="a">Row number.</param>
            /// <param name="b">Column number.</param>
            public void CreateRectangle(int a, int b)
            {
                for (int row = 0; row < b; row++)
                {
                    for (int col = 0; col < a; col++)
                    {
                        Pixels[(row, col)] = 1;
                    }
                }
            }

            /// <summary>
            /// Rotate a column by a given number of pixels.
            /// </summary>
            /// <param name="column">Column number to rotate.</param>
            /// <param name="n">Number of pixels to rotate by.</param>
            public void RotateColumn(int column, int n)
            {
                List<int> values = new List<int>();
                for (int row = 0; row < 6; row++)
                {
                    values.Add(
                        Pixels[(row, column)]
                    );
                }

                List<int> newIndexes = Enumerable.Range(n, 6 - n).ToList();
                newIndexes.AddRange(Enumerable.Range(0, n).ToList());
                var z = values.Zip(newIndexes, (v, i) => new { Value = v, Index = i });
                foreach (var aaa in z)
                {
                    Pixels[(aaa.Index, column)] = aaa.Value;
                }
            }

            /// <summary>
            /// Rotate a row by a given number of pixels.
            /// </summary>
            /// <param name="row">Row number to rotate.</param>
            /// <param name="n">Number of pixels to rotate by.</param>
            public void RotateRow(int row, int n)
            {
                List<int> values = new List<int>();
                for (int col = 0; col < 50; col++)
                {
                    values.Add(
                        Pixels[(row, col)]
                    );
                }

                List<int> newIndexes = Enumerable.Range(n, 50 - n).ToList();
                newIndexes.AddRange(Enumerable.Range(0, n).ToList());
                var z = values.Zip(newIndexes, (v, i) => new { Value = v, Index = i });
                foreach (var aaa in z)
                {
                    Pixels[(row, aaa.Index)] = aaa.Value;
                }
            }

            /// <summary>
            /// Calculate the total number of lit pixels.
            /// </summary>
            /// <returns>Number of lit pixels.</returns>
            public int TotalLitPixels()
            {
                return Pixels.Values.Sum();
            }

            /// <summary>
            /// Create a string representation of the pixels so that
            /// the contents of the screen can be seen when the string
            /// is printed.
            /// </summary>
            /// <returns>String to be printed.</returns>
            public string PrintPattern()
            {
                string pattern = "";
                for (int row = 0; row < 6; row++)
                {
                    for (int col = 0; col < 50; col++)
                    {
                        pattern += Pixels[(row, col)] == 1 ? "#" : " ";
                    }
                    pattern += "\n";
                }
                return pattern;
            }

            public Screen(Dictionary<(int, int), int> pixels)
            {
                Pixels = pixels;
            }
        }
    }
}