using System.Text.RegularExpressions;

namespace PuzzleSeven
{
    class Program
    {
        /// <summary>
        /// Main function to solve part 1 and part 2.
        /// </summary>
        static void Main()
        {
            string[] ipAddresses = ReadIpAddresses();

            int tlsCount = 0;
            int slsCount = 0;
            foreach (string ip in ipAddresses)
            {
                IpAddress i = new IpAddress(ip);
                if (i.IsValidABBA()) tlsCount++;
                if (i.IsValidBAB()) slsCount++;
            }

            Console.WriteLine($"Part 1\nNumber of IPs which support TLS - {tlsCount}");
            Console.WriteLine($"Part 2\nNumber of IPs which support SLS - {slsCount}");
        }

        /// <summary>
        /// Read all the IP addresses from the text file.
        /// </summary>
        /// <returns>Array of IP addresses.</returns>
        static string[] ReadIpAddresses()
        {
            return File.ReadAllLines("../puzzle_input/puzzle_7.txt");
        }

        /// <summary>
        /// Reverse a string.
        /// </summary>
        /// <param name="s">String to reverse.</param>
        /// <returns>Reversed string.</returns>
        static string ReverseString(string s)
        {
            string reversed = "";
            foreach (int i in Enumerable.Range(0, s.Length).Reverse())
            {
                reversed += s.Substring(i, 1);
            }
            return reversed;
        }

        /// <summary>
        /// Check if a string contains any ABBA patterns.
        /// </summary>
        /// <param name="s">String to check.</param>
        /// <returns>
        /// True if ABBA pattern is found, else false.
        /// </returns>
        static Boolean CheckABBA(string s)
        {
            foreach (int i in Enumerable.Range(0, s.Length - 3))
            {
                string sFour = s.Substring(i, 4);
                string? sFourReverse = ReverseString(sFour);
                string sFirstHalf = sFour.Substring(0, 2);
                string sSecondHalf = sFour.Substring(2, 2);
                if (sFour == sFourReverse && sFirstHalf != sSecondHalf)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find all the ABA patterns in the outer strings.
        /// </summary>
        /// <param name="outerStrings">List of outer strings.</param>
        /// <returns>List of strings following ABA pattern.</returns>
        static List<string> FindABAs(List<string> outerStrings)
        {
            List<string> abas = new List<string>();
            foreach (string s in outerStrings)
            {
                foreach (int i in Enumerable.Range(0, s.Length - 2))
                {
                    string sThree = s.Substring(i, 3);
                    string sThreeReversed = ReverseString(sThree);
                    string firstLetter = sThree.Substring(0, 1);
                    string secondLetter = sThree.Substring(1, 1);
                    if (sThree == sThreeReversed && firstLetter != secondLetter)
                    {
                        abas.Add(sThree);
                    }
                }
            }
            return abas;
        }

        /// <summary>
        /// Check if ABA pattern has corresponding BAB pattern in any of
        /// the strings inside the brackets.
        /// </summary>
        /// <param name="bracketList">
        /// List of strings from inside brackets.
        /// </param>
        /// <param name="abaList">List of ABA pattern strings.</param>
        /// <returns>
        /// True if corresponding BAB pattern is found, else false.
        /// </returns>
        static Boolean CheckBAB(List<string> bracketList, List<string> abaList)
        {
            foreach (string aba in abaList)
            {
                string firstLetter = aba.Substring(0, 1);
                string secondLetter = aba.Substring(1, 1);
                string bab = secondLetter + firstLetter + secondLetter;
                if (bracketList.Any(bracketString => bracketString.Contains(bab)))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Class to represent an IP address.
        /// </summary>
        class IpAddress
        {
            private string address = "";
            public string Address
            {
                get { return address; }
                set { address = value; }
            }

            /// <summary>
            /// Get a list of strings inside the brackets.
            /// </summary>
            private List<string> BracketStrings
            {
                get
                {
                    return Regex.Matches(address, @"\[(\w+)\]")
                        .Cast<Match>()
                        .Select(m => m.Groups[1].Value)
                        .ToList();
                }
            }

            /// <summary>
            /// Get a list of strings outside the brackets.
            /// </summary>
            private List<string> OuterStrings
            {
                get
                {
                    return Regex.Matches(address, @"\w+")
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .Except(BracketStrings)
                        .ToList();
                }
            }

            /// <summary>
            /// Check if the IP address conforms to ABBA pattern rule.
            /// </summary>
            /// <returns>True if IP address conforms, else false.</returns>
            public Boolean IsValidABBA()
            {
                return OuterStrings.Any(c => CheckABBA(c))
                    && !BracketStrings.Any(c => CheckABBA(c));
            }

            /// <summary>
            /// Check if the IP address conforms to BAB pattern rule.
            /// </summary>
            /// <returns>True if IP address conforms, else false.</returns>
            public Boolean IsValidBAB()
            {
                List<string> abas = FindABAs(OuterStrings);
                return CheckBAB(BracketStrings, abas);
            }

            public IpAddress(string address)
            {
                Address = address;
            }
        }
    }
}
