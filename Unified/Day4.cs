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
            //ASSUMPTIONS
                //1 - Clean line spearations for passphrase and each word separated by space

            Console.WriteLine("Finding number of valid passphrases! Eliminating equal and anagram substrings!");
            StreamReader sr = new StreamReader("day4Input.txt");
            string possiblePassphrase;
            int numValidPassphrases = 0;
            int numPossiblePassphrases = 0;

            while ((possiblePassphrase= sr.ReadLine()) != null)
            {
                numPossiblePassphrases++;
                bool validAtSubstringLevel = CheckValidPassphraseSubstringUniqueness(possiblePassphrase);
                bool validAtSibstringAnagramLevel = CheckValidPassphraseSubstringAnagramUniqueness(possiblePassphrase);
                if (validAtSubstringLevel && validAtSibstringAnagramLevel){numValidPassphrases++;}
            }

            Console.WriteLine("Number of possible passphrases = "+numPossiblePassphrases);
            Console.WriteLine("Number of valid passphrases = "+numValidPassphrases);
        }

        private static bool CheckValidPassphraseSubstringUniqueness(string pass)
        {
            //Part 1 - Find number of valid passphrases in puzzle input - valid are no duplicate words
            bool passphrasevalid = true;
            string[] substrings = pass.Split(' ');
            foreach (string s in substrings)
            {
                var occurrences = (from sub in substrings where sub == s select sub);
                if ((int)occurrences.Count()>1){passphrasevalid = false;}
            }
            return passphrasevalid;
        }

        private static bool CheckValidPassphraseSubstringAnagramUniqueness(string pass)
        {
            //Part 2 - Make sure there are no anagrams in substrings
            string[] substrings = pass.Split(' ');
            for (int i = 0; i<substrings.Length-1; i++)
            {
                for (int j = i+1; j<substrings.Length; j++)
                {
                    if (IsSubstringAnagramPair(substrings[i], substrings[j])){return false;}
                }
            }
            return true;
        }

        private static bool IsSubstringAnagramPair(string s1, string s2)
        {
            if (s1.Length != s2.Length){return false;}
            s1 = String.Concat(s1.OrderBy(c => c));
            s2 = String.Concat(s2.OrderBy(c => c));
            return String.Equals(s1, s2);

        }
    }
}
