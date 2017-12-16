using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day14
    {
        public static void TestDay14()
        {
            Console.WriteLine("-------------------DAY 14-------------------");
            
            //128x128 grid, each row by a knot hash
            //Each row represntation of knot hash of <string>-<rownumber>


 
            string inputString = "wenycdww";
            StringBuilder trackingString = new StringBuilder();
            //Loop of 128
            for (int rowTracker = 0; rowTracker<128; rowTracker++)
            {
                //Compute string
                string rowString = inputString+"-"+rowTracker.ToString();
                List<int> instructionSet = new List<int>();
                foreach (char c in rowString)
                {
                    instructionSet.Add((Int32)c);
                }
                //Add well-known characters
                instructionSet.Add(17);
                instructionSet.Add(31);
                instructionSet.Add(73);
                instructionSet.Add(47);
                instructionSet.Add(23);
                
                //Calculate knot hash
                string rowHash = CalculateKnotHash(instructionSet);

                //Calculate binary value
                StringBuilder sb = new StringBuilder();
                foreach (char c in rowHash)
                {
                    int hexNum = Convert.ToByte(c.ToString(), 16 );
                    string bitString = Convert.ToString(hexNum, 2).PadLeft(4, '0');
                    sb.Append(bitString);
                }

                //Store
                trackingString.Append(sb.ToString());
            }

            //Count ones
            int numberOfOnes = 0;
            foreach (char c in trackingString.ToString())
            {
                if (c == '1')
                {
                    numberOfOnes++;
                }
            }

            Console.WriteLine(numberOfOnes);
            
        }

        public static string CalculateKnotHash(List<int> inputInstructions)
        {
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
                foreach (int instruction in inputInstructions)
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

            return sb.ToString();
        }

    }

}