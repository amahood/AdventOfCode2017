using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day21
    {
        public static void TestDay21()
        {
            Console.WriteLine("-------------------DAY 21-------------------");
            
            List<char[,]> transformInputs = new List<char[,]>();
            List<char[,]> transformOutputs = new List<char[,]>();
            char[,] startingImage = new char[3,3]{{'.','#','.'},{'.','.','#'},{'#','#','#'}};

            StreamReader sr = new StreamReader("day21Input.txt");
            string line;
            char delimiter = '/';
            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(" => ");
                string[] inputSubstrings = substrings[0].Split(delimiter);
                string[] outputSubstrings = substrings[1].Split(delimiter);

                //Process input transforms
                char[,] inputItem = new char[inputSubstrings.Count(),inputSubstrings[0].Count()];
                int rowTracker = 0;
                foreach (string s in inputSubstrings)
                {
                    for (int i = 0;i<s.Count();i++)
                    {
                        inputItem[rowTracker,i] = s[i];
                    }
                    rowTracker++;
                }
                transformInputs.Add(inputItem);

                //Process output transforms
                char[,] outputItem = new char[outputSubstrings.Count(),outputSubstrings[0].Count()];
                rowTracker = 0;
                foreach (string s in outputSubstrings)
                {
                    for (int i = 0;i<s.Count();i++)
                    {
                        outputItem[rowTracker,i] = s[i];
                    }
                    rowTracker++;
                }
                transformOutputs.Add(outputItem);
            }
            
            int currentSize = 3;
            for (int enhances = 0;enhances<5;enhances++)
            {
                int numSubsquares;
                List<char[,]> subsquares = new List<char[,]>();
                if (currentSize%2==0)
                {
                    numSubsquares = currentSize/2;
                }
                else if (currentSize%3==0)
                {
                    numSubsquares = currentSize/3;
                    char[,] finalIterationSquare = new char[currentSize+numSubsquares,currentSize+numSubsquares];
                    
                    //Make subgrids
                    int numSubsMade = 0;
                    while (numSubsMade<numSubsquares)
                    {
                        char[,] tempSquare = new char[3,3];

                        int localRowTracker = 0;
                        for (int row = numSubsMade*3;row<(numSubsMade*3+3);row++)
                        {
                            
                            int localColTracker = 0;
                            for (int col = numSubsMade*3;col<(numSubsMade*3+3);col++)
                            {
                                tempSquare[localRowTracker,localColTracker] = startingImage[row,col];
                                localColTracker++;
                            }
                            localRowTracker++;
                        }
                        subsquares.Add(tempSquare);
                    
                    
                    //TODO 
                        //Match and translate
                        //REcombine into larger item
                    
                }
            }
            
        }

        public static int MatchPattern(char[,] inputPattern)
        {
            int inputPatternIndex = 0;
            return inputPatternIndex;
        }

    }
}