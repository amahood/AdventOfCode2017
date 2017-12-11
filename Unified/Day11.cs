using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day10
    {
        public static void TestDay10()
        {
            Console.WriteLine("-------------------DAY 10-------------------");

            //NOT GETTING RIGHT ANSWER FOR ANY OF SAMPLE TEST CASES, NEED TO TRY TO DEBUG SOMEWHERE - EMPTY STRING IS PROBABLY BEST PLACE TO START

            //Read in list of instructions we need to parse - Changing this to read each char for Part 2
            StreamReader sr = new StreamReader("day10Input.txt");
            char nextChar;
            List<int> instructionSet = new List<int>();
            while (!sr.EndOfStream)
            {
                nextChar = (char)sr.Read();
                instructionSet.Add((Int32)nextChar);
                //Console.WriteLine((Int32)nextChar);
            }
            //Add well-known characters
            instructionSet.Add(17);
            instructionSet.Add(31);
            instructionSet.Add(73);
            instructionSet.Add(47);
            instructionSet.Add(23);

            //Initialize working list based on list size (recompile required to change list size)
            int listSize = 256;
            List<int> hashList = new List<int>();
            for (int i = 0; i<listSize; i++)
            {
                hashList.Add(i);
            }

            //Set number of rounds
            int numberOfRounds = 64;
            int roundTracker = 0;
            //Process each instruction
            int currentIndexTracker = 0;
            int currentSkip = 0;
            while (roundTracker<numberOfRounds)
            {
                foreach (int instruction in instructionSet)
                {
                    //Reverse required elements
                    //Get all elements into substring
                    List<int> sublist = new List<int>();
                    int sublistIndexTracker = currentIndexTracker;
                    while (sublist.Count < instruction)
                    {
                        if (sublistIndexTracker >= listSize)
                        { 
                            sublistIndexTracker = sublistIndexTracker-listSize;
                        }
                        sublist.Add(hashList[sublistIndexTracker]);
                        sublistIndexTracker++;
                    }
                    //Reverse substring
                    sublist.Reverse();
                    //Insert back into list
                    int sublistInsertionTracker = currentIndexTracker;
                    foreach (int s in sublist)
                    {
                        if (sublistInsertionTracker >= listSize)
                        { 
                            sublistInsertionTracker = sublistInsertionTracker-listSize;
                        }
                        hashList[sublistInsertionTracker] = s;
                        sublistInsertionTracker++;
                    }

                    //Increment current index tracker, handle wrap around
                    currentIndexTracker = currentIndexTracker + instruction + currentSkip;
                    if (currentIndexTracker >= listSize)
                    {
                        currentIndexTracker = currentIndexTracker%listSize;
                    }

                    //Increment skip size
                    currentSkip++;
                }
                roundTracker++;
            }

            //Calculate dense hash - hard code assumption that we are dealing with 256 element array so 16 blocks of 16 chars
            int numBlocks = 16;
            int numBlocksTracker = 0;
            List<int> denseHash = new List<int>();
            while (numBlocksTracker<numBlocks)
            {
                int xorTracker = 0;
                //Do rounds to XOR within one block of 16
                for (int i = 0; i<16;i++)
                {
                    //Console.WriteLine(i+(numBlocks*numBlocksTracker));
                    xorTracker = xorTracker ^ hashList[i+(numBlocks*numBlocksTracker)];
                }
                denseHash.Add(xorTracker);
                numBlocksTracker++;
            }

            //Convert to Hex chars
            StringBuilder sb = new StringBuilder();
            foreach (int h in denseHash)
            {
                sb.Append(h.ToString("x2"));
            }

            Console.WriteLine("Dense Knot Hash checksum = "+sb.ToString());

        }
        

    }

}