namespace PuzzleOne
{
    class Program
    {
        /// <summary>
        /// Main function to calculate answers to part 1 and part 2.
        /// </summary>
        static void Main()
        {
            List<string> instructions = ReadInstructions();
            Tuple<Position, List<Position>> results = Move(instructions);

            int finalPositionBlocks = results.Item1.Blocks();
            Console.WriteLine($"Part 1\nBlocks away, final positon - {finalPositionBlocks}\n");

            Position firstRetrace = FirstRetrace(results.Item2);
            int firstRetractBlocks = firstRetrace.Blocks();
            Console.WriteLine($"Part 2\nBlocks away, first retraced position - {firstRetractBlocks}");
        }

        /// <summary>
        /// Read input files and split into a list of instructions.
        /// </summary>
        /// <returns>List of instructions.</returns>
        static List<string> ReadInstructions()
        {

            string fileString = File.ReadAllText("../puzzle_input/puzzle_1.txt");
            List<string> instructions = fileString.Split(", ").ToList();
            return instructions;
        }

        /// <summary>
        /// Move position according to the instructions to find the final
        /// position and a list of all positions passed on the way there.
        /// </summary>
        /// <param name="instructions">List of instructions.</param>
        /// <returns>Tuple where the first item is the final position and
        /// the second item is a list of all positions passed.</returns>
        static Tuple<Position, List<Position>> Move(List<string> instructions)
        {
            Direction movement = new Direction(0, 1);
            Position position = new Position(0, 0);
            List<Position> waypoints = new List<Position>();

            foreach (string i in instructions)
            {
                movement.Turn(i.Substring(0, 1));
                int magnitude = int.Parse(i.Substring(1));
                for (int m = 0; m < magnitude; m++)
                {
                    position.Move(movement);
                    waypoints.Add(new Position(position.X, position.Y));
                }
            }
            return Tuple.Create(position, waypoints);
        }

        /// <summary>
        /// Find the first retraced position.
        /// </summary>
        /// <param name="waypoints">List of all positions passed.</param>
        /// <returns>The first retraced position.</returns>
        /// <exception cref="Exception">
        /// Throw exception if no position is retraced.
        /// </exception>
        static Position FirstRetrace(List<Position> waypoints)
        {
            HashSet<Tuple<int, int>> alreadyVisited = new HashSet<Tuple<int, int>>();
            foreach (Position position in waypoints)
            {
                Tuple<int, int> locationTuple = Tuple.Create(position.X, position.Y);
                if (alreadyVisited.Contains(locationTuple))
                {
                    return position;
                }
                alreadyVisited.Add(locationTuple);
            }
            throw new Exception("No retraced positions found.");
        }

        /// <summary>
        /// Class to represent a directional vector with magnitude 1.
        /// </summary>
        class Direction
        {
            private int x;
            private int y;

            public int X { get { return x; } set { x = value; } }
            public int Y { get { return y; } set { y = value; } }

            /// <summary>
            /// Rotate the directional vector 90 degrees clockwise or anti-clockwise.
            /// </summary>
            /// <param name="direction">
            /// Either R to turn right or L to turn left.
            /// </param>
            public void Turn(string direction)
            {
                if (direction == "R")
                {
                    int currentX = x;
                    int currentY = y;
                    X = currentY; 
                    Y = -currentX;
                }
                else {
                    int currentX = x;
                    int currentY = y;
                    X = -currentY; 
                    Y = currentX;
                }
            }

            public Direction(int x, int y)
            {
                X = x; Y = y;
            }
        }

        /// <summary>
        /// Class to represent a positional vector.
        /// </summary>
        class Position
        {
            private int x;
            private int y;

            public int X { get { return x; } set { x = value; } }
            public int Y { get { return y; } set { y = value; } }

            /// <summary>
            /// Move to a new position.
            /// </summary>
            /// <param name="direction">
            /// Direction object containing which direction to move in.
            /// </param>
            public void Move(Direction direction)
            {
                X += direction.X;
                Y += direction.Y;
            }

            /// <summary>
            /// Calculate the number of blocks away from (0, 0) the position is.
            /// </summary>
            /// <returns>
            /// The number of blocks.
            /// </returns>
            public int Blocks()
            {
                return Math.Abs(X) + Math.Abs(Y);
            }

            public Position(int x, int y)
            {
                X = x; Y = y;
            }
        }
    }
}