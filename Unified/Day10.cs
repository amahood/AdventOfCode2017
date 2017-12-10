using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day10
    {
        public static void TestDay10()
        {
            Console.WriteLine("-------------------DAY 10-------------------");

            //Read in list of instructions we need to parse
            StreamReader sr = new StreamReader("day10Input.txt");
            string line;
            char delimiter = ',';
            List<int> instructionSet = new List<int>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(delimiter);
                foreach (string s in substrings)
                {
                    instructionSet.Add(Int32.Parse(s));
                }
            }

            //Initialize working list based on list size (recompile required to change list size)
            int listSize = 256;
            List<int> hashList = new List<int>();
            for (int i = 0; i<listSize; i++)
            {
                hashList.Add(i);
            }

            //Process each instruction
            int currentIndexTracker = 0;
            int currentSkip = 0;
            foreach (int instruction in instructionSet)
            {
                //Reverse required elements
                //Get all elements into substring
                List<int> sublist = new List<int>();
                int sublistIndexTracker = currentIndexTracker;
                while (sublist.Count < instruction)
                {
                    if (sublistIndexTracker >= listSize){ sublistIndexTracker = sublistIndexTracker-listSize;}
                    sublist.Add(hashList[sublistIndexTracker]);
                    sublistIndexTracker++;
                }
                //Reverse substring
                sublist.Reverse();
                //Insert back into list
                int sublistInsertionTracker = currentIndexTracker;
                foreach (int s in sublist)
                {
                    if (sublistInsertionTracker >= listSize){ sublistInsertionTracker = sublistInsertionTracker-listSize;}
                    hashList[sublistInsertionTracker] = s;
                    sublistInsertionTracker++;
                }

                //Increment current index tracker, handle wrap around
                currentIndexTracker = currentIndexTracker + instruction + currentSkip;
                if (currentIndexTracker >= listSize)
                {
                    currentIndexTracker = currentIndexTracker-listSize;
                }

                //Increment skip size
                currentSkip++;
            }

            //Compute product of first element times second element
            int hashChecksum = hashList[0]*hashList[1];
            Console.WriteLine("Hash checksum = "+hashChecksum);

        }

    }

}