using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {

            //NOTE - Initial solve of this puzzle takes the following dependnecies
                //1 - The fact that the copied version of my puzzle input has tab delimiters. Doesn't work for the sample case.
                //2 - There is in fact one pair of evenly divisible numbers in each row

            //POTENTIAL SIMPLIFICATIONS/OPTIMIZATIONS
                // Could almost certainly combine the part 1 and 2 processing into 1 loo
                // Could probably find a way to break out of the line processing loop in part 2 once we have already found an evenly divisible set

            //Part 1 - Find checksum of sum of biggest - smallest in each line
            StreamReader sr = new StreamReader("puzzleInput.txt");
            string line;
            
            int checksum = 0;
            char delimiter = '\t';

            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(delimiter);
                int lineSmallest = Int32.Parse(substrings[0]);
                int lineLargest = Int32.Parse(substrings[0]);

                foreach (string num in substrings)
                {
                    int currentItem = Int32.Parse(num);
                    if (currentItem < lineSmallest) { lineSmallest = currentItem;}
                    if (currentItem > lineLargest) { lineLargest = currentItem;}
                }
                
                int lineDelta =  lineLargest-lineSmallest;
                checksum += lineDelta;
            }
            
            Console.WriteLine("Puzzle Line Delta Checksum - " + checksum);
            sr.Close();

            //Part 2 - Find checksum of sum of only two numbers divisible in each line
            StreamReader sr2 = new StreamReader("puzzleInput.txt");
            string line2;
            
            int checksum2 = 0;

            while ((line2 = sr2.ReadLine()) != null)
            {
                string[] substrings2 = line2.Split(delimiter);
                int i = 0;
                foreach (string num in substrings2)
                {
                    int currentNum = Int32.Parse(num);
                    int loopMax = substrings2.Length;

                    for (int j = i +1; j<loopMax;j++)
                    {
                        int currentCompareNum = Int32.Parse(substrings2[j]);
                        if ( (currentNum%currentCompareNum==0) || (currentCompareNum%currentNum==0) )
                        {
                            //Console.WriteLine(currentNum + " and " + currentCompareNum + " are divisbile!");
                            int lineLargest2 = Math.Max(currentNum, currentCompareNum);
                            int lineSmallest2 = Math.Min(currentNum, currentCompareNum);
                            
                            int lineDelta2 = lineLargest2 / lineSmallest2;
                            checksum2 += lineDelta2;
                        }
                    }
                    i++;
                }              
            }

            sr2.Close();
            Console.WriteLine("Puzzle Modulo Checksum - " + checksum2);

        }
    }
}
