using System.Text.RegularExpressions;

namespace PuzzleThree
{
    class Program 
    {
        /// <summary>
        /// Main function to calculate answers to part 1 and part 2.
        /// </summary>
        static void Main()
        {
            string[] lines = ReadLines();
            List<List<int>> parsedLines = ParseLines(lines);
            List<Triangle> rowTriangles = ParseRowTriangles(parsedLines);
            List<Triangle> columnTriangles = ParseColumnTriangles(parsedLines);

            int nValidRows = CountValid(rowTriangles);
            int nValidColumns = CountValid(columnTriangles);

            Console.WriteLine($"Part 1\nNumber of valid triangle - {nValidRows}");
            Console.WriteLine($"Part 2\nNumber of valid triangle - {nValidColumns}");
        }

        /// <summary>
        /// Read the text file into a string array.
        /// </summary>
        /// <returns>String array containing each line of the text file.</returns>
        static string[] ReadLines()
        {
            string[] lines = File.ReadAllLines("../puzzle_input/puzzle_3.txt");
            return lines;
        }

        /// <summary>
        /// Convert the string for each line into a list containing three integers.
        /// </summary>
        /// <param name="lines">String array of lines.</param>
        /// <returns>List containing a list of three integers for each line.</returns>
        static List<List<int>> ParseLines(string[] lines)
        {
            List<List<int>> parsedLines = new List<List<int>>();
            foreach (string line in lines)
            {
                MatchCollection sides = Regex.Matches(line, @"\d+");
                parsedLines.Add(
                    new List<int>{
                        Int32.Parse(sides[0].ToString()),
                        Int32.Parse(sides[1].ToString()),
                        Int32.Parse(sides[2].ToString())
                    }
                );
            }
            return parsedLines;
        }

        /// <summary>
        /// Convert each list of three integers into a Triangle object.
        /// </summary>
        /// <param name="parsedLines">List containing lists of three integers.</param>
        /// <returns>List of Triangle objects.</returns>
        static List<Triangle> ParseRowTriangles(List<List<int>> parsedLines)
        {
            List<Triangle> triangles = new List<Triangle>();
            foreach (List<int> line in parsedLines)
            {
                triangles.Add(
                    new Triangle(line[0], line[1], line[2])
                );
            }
            return triangles;
        }

        /// <summary>
        /// Form triangles by taking the first integer from the first three lists, then
        /// the first three from the next three lists, and so on. This is repeated for
        /// the second and third integers in each list.
        /// </summary>
        /// <param name="parsedLines">List containing lists of three integers.</param>
        /// <returns>List of Triangle objects.</returns>
        static List<Triangle> ParseColumnTriangles(List<List<int>> parsedLines)
        {
            List<Triangle> triangles = new List<Triangle>();
            int nLines = parsedLines.Count();
            for (int col = 0; col < 3; col++)
            {
                for (int row = 0; row < nLines; row += 3)
                {
                    triangles.Add(
                        new Triangle(
                            parsedLines[row][col],
                            parsedLines[row + 1][col],
                            parsedLines[row + 2][col]
                        )
                    );
                }
            }
            return triangles;
        }

        /// <summary>
        /// Count the number of valid triangles in a list.
        /// </summary>
        /// <param name="triangles">List of Triangle objects.</param>
        /// <returns>Number of valid traingles.</returns>
        static int CountValid(List<Triangle> triangles)
        {
            int nValid = 0;
            foreach (Triangle triangle in triangles)
            {
                if (triangle.IsValid()) nValid ++;
            }
            return nValid;
        }

        /// <summary>
        /// Class to represent a triangle.
        /// </summary>
        public class Triangle
        {
            private int sideOne;
            private int sideTwo;
            private int sideThree;

            public int SideOne { get { return sideOne; } set { sideOne = value; }}
            public int SideTwo { get { return sideTwo; } set { sideTwo = value; }}
            public int SideThree { get { return sideThree; } set { sideThree = value; }}

            /// <summary>
            /// Check if a valid traingle can be formed with the length of sides provided.
            /// </summary>
            /// <returns>True if a valid triangle can be formed, otherwise False.</returns>
            public Boolean IsValid()
            {
                List<int> sides = new List<int> { SideOne, SideTwo, SideThree };
                return sides.Sum() - sides.Max() > sides.Max();
            }

            public Triangle(int s1, int s2, int s3)
            {
                SideOne = s1;
                SideTwo = s2;
                SideThree = s3;
            }
        }
    }
}