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
            List<string> stringGrid = new List<string>();
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
                stringGrid.Add(sb.ToString());
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

            Console.WriteLine("Number of Ones = " + numberOfOnes);
            
            //Create grid to track answers
            int currentRegionTracker = 1;
            int[,] regionGrid = new int[128,128];
            //Fill grid with 0s
            for (int row =0; row<128; row++)
            {
                for (int column = 0; column<128; column++)
                {
                    regionGrid[row, column] = 0;
                }
            }

            //With current implementation, I'm getting close, like 1600 some, but sample case I don't think I handle is if there is a giant U, I will start at both ends with different numbers
            for (int row =0; row<128; row++)
            {
                for (int column = 0; column<128; column++)
                {
                    if (stringGrid[row][column]=='1')
                    {
                        //Try chase mechansim
                        //Retrieve square from answer grid and check to see if grid is already populated with number
                        int answerFromAnswerGrid = regionGrid[row,column];

                        if (answerFromAnswerGrid==0)
                        {
                            int squaresVisitedInThisGroup = 0;

                            List<KeyValuePair<int,int>> squaresToVisit = new List<KeyValuePair<int, int>>();
                            KeyValuePair<int,int> currentSquare = new KeyValuePair<int, int>(row,column);
                            squaresToVisit.Add(currentSquare);

                            List<KeyValuePair<int,int>> haveVisitedBeforeInThisGroup = new List<KeyValuePair<int, int>>();

                            while (squaresToVisit.Count!=0)
                            {
                                currentSquare = squaresToVisit.First();
                                int currentRow = currentSquare.Key;
                                int currentColumn = currentSquare.Value;
                                squaresToVisit.Remove(currentSquare);
                                haveVisitedBeforeInThisGroup.Add(new KeyValuePair<int,int>(currentRow,currentColumn));

                                regionGrid[currentRow,currentColumn] = currentRegionTracker;
                                
                                //Check right
                                if (currentColumn<127)
                                {
                                    if (stringGrid[currentRow][currentColumn+1]=='1' && regionGrid[currentRow,currentColumn+1]==0 && !haveVisitedBeforeInThisGroup.Contains(new KeyValuePair<int, int>(currentRow, currentColumn+1)) && !squaresToVisit.Contains(new KeyValuePair<int, int>(currentRow, currentColumn+1)))
                                    {
                                        squaresToVisit.Add(new KeyValuePair<int, int>(currentRow, currentColumn+1));
                                    }
                                }
                                //Check left
                                if (currentColumn>0)
                                {
                                    if (stringGrid[currentRow][currentColumn-1]=='1' && regionGrid[currentRow, currentColumn-1]==0 && !haveVisitedBeforeInThisGroup.Contains(new KeyValuePair<int, int>(currentRow, currentColumn-1)) &&  !squaresToVisit.Contains(new KeyValuePair<int, int>(currentRow, currentColumn-1)))
                                    {
                                        squaresToVisit.Add(new KeyValuePair<int, int>(currentRow, currentColumn-1));
                                    }
                                }
                                //Check up
                                if (currentRow>0)
                                {
                                    if (stringGrid[currentRow-1][currentColumn]=='1' && regionGrid[currentRow-1, currentColumn]==0 && !haveVisitedBeforeInThisGroup.Contains(new KeyValuePair<int, int>(currentRow-1, currentColumn)) && !squaresToVisit.Contains(new KeyValuePair<int, int>(currentRow-1, currentColumn)))
                                    {
                                        squaresToVisit.Add(new KeyValuePair<int, int>(currentRow-1, currentColumn));
                                    }
                                }
                                //Check down
                                if (currentRow<127)
                                {
                                    if (stringGrid[currentRow+1][currentColumn]=='1' && regionGrid[currentRow+1, currentColumn]==0 && !squaresToVisit.Contains(new KeyValuePair<int, int>(currentRow+1, currentColumn)) && !haveVisitedBeforeInThisGroup.Contains(new KeyValuePair<int, int>(currentRow+1, currentColumn)))
                                    {
                                        squaresToVisit.Add(new KeyValuePair<int, int>(currentRow+1, currentColumn));
                                    }
                                }

                                squaresVisitedInThisGroup++;
                            }
                            //Console.WriteLine("Squares visited in Group "+ currentRegionTracker + " = " + squaresVisitedInThisGroup);
                            
                            currentRegionTracker++;                        
                        }
                        else if (answerFromAnswerGrid!=0)
                        {
                            //Do nothing, just go to next square, as we've already hit this as part of the chase
                        }
                    }
                }
            }

            Console.WriteLine("Number of groups - " + (currentRegionTracker-1));
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
