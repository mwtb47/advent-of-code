namespace PuzzleTwo
{
    class Program
    {
        static Dictionary<(int, int), char> numberPadOne = new Dictionary<(int, int), char>() {
            { (1, 3), '1' },
            { (2, 3), '2' },
            { (3, 3), '3' },
            { (1, 2), '4' },
            { (2, 2), '5' },
            { (3, 2), '6' },
            { (1, 1), '7' },
            { (2, 1), '8' },
            { (3, 1), '9' }
        };

        static Dictionary<(int, int), char> numberPadTwo = new Dictionary<(int, int), char>() {
            { (3, 5), '1' },
            { (2, 4), '2' },
            { (3, 4), '3' },
            { (4, 4), '4' },
            { (1, 3), '5' },
            { (2, 3), '6' },
            { (3, 3), '7' },
            { (4, 3), '8' },
            { (5, 3), '9' },
            { (2, 2), 'A' },
            { (3, 2), 'B' },
            { (4, 2), 'C' },
            { (3, 1), 'D' }
        };

        static Dictionary<char, (int, int)> movements = new Dictionary<char, (int, int)>() {
            { 'U', (0, 1) },
            { 'D', (0, -1) },
            { 'L', (-1, 0) },
            { 'R', (1, 0) }
        };

        /// <summary>
        /// Main function to calculate answers to part 1 and part 2.
        /// </summary>
        static void Main()
        {
            List<string> instructions = ReadInput();

            Pad padOne = new Pad(numberPadOne);
            Pad padTwo = new Pad(numberPadTwo);

            var answerOne = Solve(instructions, padOne, (2, 2));
            var answerTwo = Solve(instructions, padTwo, (1, 3));
            Console.WriteLine($"Part 1 - {answerOne}");
            Console.WriteLine($"Part 1 - {answerTwo}");
        }

        /// <summary>
        /// Read the input from text file.
        /// </summary>
        /// <returns>List of strings containing instructions.</returns>
        static List<string> ReadInput()
        {
            string fileString = File.ReadAllText("../puzzle_input/puzzle_2.txt");
            List<string> instructions = fileString.Split("\n").ToList();
            return instructions;
        }

        /// <summary>
        /// Move according to the instructions and find the code.
        /// </summary>
        /// <param name="instructions">
        /// List of instructions.
        /// </param>
        /// <param name="numberPad">
        /// A Pad object representing the number pad.
        /// </param>
        /// <param name="startingPosition">
        /// The starting position.
        /// </param>
        /// <returns>The code as a string.</returns>
        static string Solve(List<string> instructions, Pad numberPad, (int, int) startingPosition)
        {
            (int, int) movement;
            (int, int) nextPosition;
            (int, int) currentPosition = startingPosition;
            List<char> code = new List<char>();

            foreach (string line in instructions)
            {
                foreach (char character in line)
                {
                    movement = movements[character];
                    nextPosition = (currentPosition.Item1 + movement.Item1, currentPosition.Item2 + movement.Item2);
                    if (numberPad.Positions.Contains(nextPosition))
                    {
                        currentPosition = nextPosition;
                    }
                }
                code.Add(numberPad.GetButton(currentPosition));
            }
            return String.Join("", code.ToArray());
        }

        /// <summary>
        /// Class to represent a number pad.
        /// </summary>
        class Pad
        {
            private Dictionary<(int, int), char> mappings = new Dictionary<(int, int), char>();
            public Dictionary<(int, int), char> Mappings
            {
                get { return mappings; } set { mappings = value; }
            }

            public List<(int, int)> Positions
            {
                get { return Mappings.Keys.ToList(); }
            }
            
            /// <summary>
            /// Return the button character for a given position.
            /// </summary>
            /// <param name="key">The location on the number pad.</param>
            /// <returns>The character.</returns>
            public char GetButton((int, int) key)
            {
                return Mappings[key];
            }

            public Pad(Dictionary<(int, int), char> m)
            {
                Mappings = m;
            }
        }
    }
}