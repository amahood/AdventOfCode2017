using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day6
    {
        public static void TestDay6()
        {
            Console.WriteLine("-------------------DAY 6-------------------");

            List<int> memoryArray = new List<int>();

            StreamReader sr = new StreamReader("day6Input.txt");
            string line;
            char delimiter = '\t';
            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(delimiter);
                foreach (string s in substrings)
                {
                    memoryArray.Add(Int32.Parse(s));
                }
            }

            List<List<int>> patternsSeen = new List<List<int>>();
            bool patternSeenAgain = false;
            int loopTracker = 0;
            patternsSeen = AddToPatternsSeen(patternsSeen, memoryArray);

            while (!patternSeenAgain)
            {
                int mostFilledIndex = FindMostFilledBank(memoryArray);
                memoryArray = Defrag(memoryArray, mostFilledIndex);
                patternsSeen = AddToPatternsSeen(patternsSeen, memoryArray);
                patternSeenAgain = HaveSeenPattern(patternsSeen);
                loopTracker++;
            }

            Console.WriteLine("It took " + loopTracker + " redistribution cycles before detecting infinite memory rebalance loop!");

        }

        public static List<List<int>> AddToPatternsSeen(List<List<int>> patternList, List<int> currentArray)
        {
            List<int> memCopy = new List<int>();
            foreach (int i in currentArray)
            {
                memCopy.Add(i);
            }
            patternList.Add(memCopy);
            return patternList;
        }

        public static int FindMostFilledBank(List<int> mem)
        {
            int indexTracker = 0;
            int largestIndex = 0;
            int largestValue = 0;

            foreach (int size in mem)
            {
                if (size > largestValue)
                {
                    largestIndex = indexTracker;
                    largestValue = size;
                }
                indexTracker++;
            }

            return largestIndex;
        }

        public static List<int> Defrag(List<int> fragMem, int largestStart)
        {
            int redistributionAmount = fragMem[largestStart];
            fragMem[largestStart] = 0;
            int redistIndexTracker = 0;
            if (largestStart ==fragMem.Count-1)
            {
                redistIndexTracker = 0;
            }
            else
            {
                redistIndexTracker = (largestStart + 1);
            }

            while (redistributionAmount>0)
            {
                fragMem[redistIndexTracker]++;
                redistributionAmount--;
                if (redistIndexTracker ==fragMem.Count-1)
                {
                    redistIndexTracker = 0;
                }
                else
                {
                    redistIndexTracker++;
                }
            }

            return fragMem;
        }

        public static bool HaveSeenPattern(List<List<int>> patternDict)
        {
            for (int i = 0; i < (patternDict.Count-1); i++)
            {
                if (patternDict[i].SequenceEqual(patternDict.Last())){return true;}
            }
            return false;
        }
    }
}