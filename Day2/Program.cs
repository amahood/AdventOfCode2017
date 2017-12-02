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

            //NOTE - Initial solve of this puzzle takes a dependency on the fact that the copied version of my puzzle input has tab delimiters. Doesn't work for the sample case.

            //Console.WriteLine("Printing out puzzle input!");
            StreamReader sr = new StreamReader("puzzleInput.txt");
            string line;
            
            int checksum = 0;
            char delimiter = '\t';

            while ((line = sr.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                string[] substrings = line.Split(delimiter);
                int lineSmallest = Int32.Parse(substrings[0]);
                int lineLargest = Int32.Parse(substrings[0]);

                foreach (string num in substrings)
                {
                    //Console.WriteLine(num);
                    int currentItem = Int32.Parse(num);
                    if (currentItem < lineSmallest) { lineSmallest = currentItem;}
                    if (currentItem > lineLargest) { lineLargest = currentItem;}
                }
                
                //Console.WriteLine("Line largest - " + lineLargest);
                //Console.WriteLine("Line smallest - " + lineSmallest);
                int lineDelta =  lineLargest-lineSmallest;
                //Console.WriteLine("Adding "+  lineDelta + " to checksum!");
                checksum += lineDelta;
                //Console.WriteLine("Current checksum value - " + checksum);
            }
            
            Console.WriteLine("Puzzle Checksum - " + checksum);
        }
    }
}
