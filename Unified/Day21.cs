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
            
            char[,] finalIterationSquare = new char[,]{};
            finalIterationSquare = (char[,])startingImage.Clone();
            for (int enhances = 0;enhances<18;enhances++)
            {
                //Split into subsquares
                List<char[,]> subsquares = new List<char[,]>();
                subsquares = SplitToSubsquares(finalIterationSquare);
                
                //Match subsquares to rules
                List<int> transformsToAdd = new List<int>();
                foreach (char[,] s in subsquares)
                {
                    transformsToAdd.Add(MatchPattern(s, transformInputs));
                }
                
                char[,] combinedGrid = RecombineSubsquares(transformsToAdd, transformOutputs);
                finalIterationSquare = new char[combinedGrid.GetLength(0),combinedGrid.GetLength(0)];
                finalIterationSquare = (char[,])combinedGrid.Clone();
            }

            Console.WriteLine("Printing final grid:");
            int numPixelsOn = 0;
            for (int row = 0;row<finalIterationSquare.GetLength(0);row++)
            {
                StringBuilder sb = new StringBuilder();
                for (int col = 0;col<finalIterationSquare.GetLength(1);col++)
                {
                    char nextChar = finalIterationSquare[row,col];
                    if (nextChar=='#')
                    {
                        numPixelsOn++;
                    }
                    sb.Append(nextChar);
                }
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("Number of pixels still on = "+numPixelsOn);
            
        }

        public static List<char[,]> SplitToSubsquares(char[,] gridToSplit)
        {
            List<char[,]> listOfSubsquares = new List<char[,]>();
            char[,] copyOfGrid = (char[,])gridToSplit.Clone();
            
            int incomingGridDimension = gridToSplit.GetLength(0);
            int divisibleBy=0;

            if (incomingGridDimension%2==0){divisibleBy=2;}
            else if (incomingGridDimension%3==0){divisibleBy=3;}
            else {Console.WriteLine("Should never get here, means error parsing grid dimension!");}

            for (int r = 0; r<incomingGridDimension; r += divisibleBy)
            {
                for (int c = 0; c<incomingGridDimension; c += divisibleBy)
                {
                    char[,] subsquare = new char[divisibleBy,divisibleBy];

                    subsquare[0,0] = copyOfGrid[r,c];
                    subsquare[0,1] = copyOfGrid[r,c+1];
                    subsquare[1,0] = copyOfGrid[r+1,c];
                    subsquare[1,1] = copyOfGrid[r+1,c+1];
                    if (divisibleBy==3)
                    {
                        subsquare[0,2] = copyOfGrid[r,c+2];
                        subsquare[1,2] = copyOfGrid[r+1,c+2];
                        subsquare[2,0] = copyOfGrid[r+2,c];
                        subsquare[2,1] = copyOfGrid[r+2,c+1];
                        subsquare[2,2] = copyOfGrid[r+2,c+2];
                    }

                    listOfSubsquares.Add(subsquare);
                }
            }

            return listOfSubsquares;
        }

        public static int MatchPattern(char[,] inputPattern, List<char[,]> transformInputs)
        {

            int inputPatternIndex = 0;
            bool foundMatch = false;

            while (!foundMatch && inputPatternIndex<transformInputs.Count)
            {
                if (inputPattern.GetLength(0)==transformInputs[inputPatternIndex].GetLength(0))
                {
                    char temp;
                    //Check current orientation
                    char[,] inputCopy = inputPattern;
                    if (inputCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }
                    
                    //Chek 90
                    inputCopy = Rotate90(inputCopy,inputCopy.GetLength(0));
                    if (inputCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }
                    
                    //Check 180
                    inputCopy = Rotate90(inputCopy,inputCopy.GetLength(0));
                    if (inputCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }
                    
                    //Check 270
                    inputCopy = Rotate90(inputCopy,inputCopy.GetLength(0));
                    if (inputCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }
                    
                    //Check flip
                    char[,] inputFlipCopy = inputPattern;
                    for (int flips = 0;flips<inputFlipCopy.GetLength(1);flips++)
                    {
                        temp = inputFlipCopy[flips,0];
                        inputFlipCopy[flips,0] = inputFlipCopy[flips,inputFlipCopy.GetLength(0)-1];
                        inputFlipCopy[flips,inputFlipCopy.GetLength(0)-1] = temp;
                    }
                    if (inputFlipCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }
                    
                    //Check flip+90
                    inputFlipCopy = Rotate90(inputFlipCopy,inputFlipCopy.GetLength(0));
                    if (inputFlipCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }

                    //Check flip+180
                    inputFlipCopy = Rotate90(inputFlipCopy,inputFlipCopy.GetLength(0));
                    if (inputFlipCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }

                    //Check flip+270
                    inputFlipCopy = Rotate90(inputFlipCopy,inputFlipCopy.GetLength(0));
                    if (inputFlipCopy.Cast<char>().SequenceEqual(transformInputs[inputPatternIndex].Cast<char>()))
                    {
                        return inputPatternIndex;
                    }
                }
                inputPatternIndex++;
            }
            Console.WriteLine("Should never get here, means we didn't get a match!!!!!");
            return inputPatternIndex;
        }

        public static char[,] RecombineSubsquares(List<int> transformIndexes, List<char[,]> transformOutputs)
        {
            int subsquareSize = transformOutputs[transformIndexes.First()].GetLength(0);
            int newGridSize = (int)Math.Sqrt(transformIndexes.Count)*subsquareSize;
            int transformIndex = 0;

            char[,] outputGrid = new char[newGridSize,newGridSize];
            for (int r=0; r<newGridSize; r += subsquareSize)
            {
                for (int c = 0; c< newGridSize; c += subsquareSize)
                {
                    outputGrid[r,c] = transformOutputs[transformIndexes[transformIndex]][0,0];
                    outputGrid[r,c+1] = transformOutputs[transformIndexes[transformIndex]][0,1];
                    outputGrid[r,c+2] = transformOutputs[transformIndexes[transformIndex]][0,2];
                    outputGrid[r+1,c] = transformOutputs[transformIndexes[transformIndex]][1,0];
                    outputGrid[r+1,c+1] = transformOutputs[transformIndexes[transformIndex]][1,1];
                    outputGrid[r+1,c+2] = transformOutputs[transformIndexes[transformIndex]][1,2];
                    outputGrid[r+2,c] = transformOutputs[transformIndexes[transformIndex]][2,0];
                    outputGrid[r+2,c+1] = transformOutputs[transformIndexes[transformIndex]][2,1];
                    outputGrid[r+2,c+2] = transformOutputs[transformIndexes[transformIndex]][2,2];
                    if (subsquareSize==4)
                    {
                        outputGrid[r,c+3] = transformOutputs[transformIndexes[transformIndex]][0,3];
                        outputGrid[r+1,c+3] = transformOutputs[transformIndexes[transformIndex]][1,3];
                        outputGrid[r+2,c+3] = transformOutputs[transformIndexes[transformIndex]][2,3];
                        outputGrid[r+3,c] = transformOutputs[transformIndexes[transformIndex]][3,0];
                        outputGrid[r+3,c+1] = transformOutputs[transformIndexes[transformIndex]][3,1];
                        outputGrid[r+3,c+2] = transformOutputs[transformIndexes[transformIndex]][3,2];
                        outputGrid[r+3,c+3] = transformOutputs[transformIndexes[transformIndex]][3,3];
                    }

                    transformIndex++;
                }
            }

            return outputGrid;
        }

        public static char[,] Rotate90(char[,] array, int size)
        {
            char[,] ret = new char[size,size];

            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j) {
                    ret[i, j] = array[size - j - 1, i];
                }
            }

            return ret;
        }

    }
}