using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public static class Day4
    {
        public static void TestDay4()
        {
            Console.WriteLine("-------------------DAY 4-------------------");
            //Part 1 - Find number of valid passphrases in puzzle input - valid are no duplicate words
            //ASSUMPTIONS
                //1 - Clean line spearations for passphrase and each word separated by space

            Console.WriteLine("Solving Part 1 - Finding number of valid passphrases!");
            StreamReader sr = new StreamReader("day4Input.txt");
            string possiblePassphrase;
            int numValidPassphrases = 0;
            int numPossiblePassphrases = 0;

            while ((possiblePassphrase= sr.ReadLine()) != null)
            {
                numPossiblePassphrases++;
                bool passphrasevalid = true;
                string[] substrings = possiblePassphrase.Split(' ');
                foreach (string s in substrings)
                {
                    var occurrences = (from sub in substrings where sub == s select sub);
                    if ((int)occurrences.Count()>1){passphrasevalid = false;}
                }
                if (passphrasevalid){numValidPassphrases++;}
            }

            Console.WriteLine("Number of possible passphrases = "+numPossiblePassphrases);
            Console.WriteLine("Number of valid passphrases = "+numValidPassphrases);
        }
    }
}
