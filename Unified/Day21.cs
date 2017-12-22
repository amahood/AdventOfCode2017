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
            char[,] finalIterationSquare = new char[,]{};
            for (int enhances = 0;enhances<2;enhances++)
            {
                int numSubsquares = 0; 
                List<char[,]> subsquares = new List<char[,]>();
                if (currentSize%2==0)
                {
                    numSubsquares = Convert.ToInt32(Math.Pow(Convert.ToDouble(2),Convert.ToDouble(currentSize/2)));
                    int numSubsMade = 0;
                    while (numSubsMade<numSubsquares)
                    {
                        for (int subs = 0; subs<currentSize/2;subs++)
                        {
                            char[,] tempSquare = new char[2,2];
                            int localRowTracker = 0;
                            for (int row = numSubsMade/2*2;row<(numSubsMade/2*2+2);row++)
                            {
                                int localColTracker = 0;
                                for (int col = subs*2;col<(subs*2+2);col++)
                                {
                                    tempSquare[localRowTracker,localColTracker] = finalIterationSquare[row,col];
                                    localColTracker++;
                                }
                                localRowTracker++;  
                            }
                            subsquares.Add((char[,])tempSquare.Clone());   
                            numSubsMade++; 
                        }   
                        
                    }     
                }
                else if (currentSize%3==0)
                {
                    if (currentSize!=3)
                    {
                        numSubsquares = Convert.ToInt32(Math.Pow(Convert.ToDouble(2),Convert.ToDouble(currentSize/3)));
                    }
                    else numSubsquares = 1;

                    int numSubsMade = 0;
                    while (numSubsMade<numSubsquares)
                    {
                        for (int subs = 0; subs<currentSize/3;subs++)
                        {
                            char[,] tempSquare = new char[3,3];
                            int localRowTracker = 0;
                            for (int row = numSubsMade/3*3;row<(numSubsMade/3*3+3);row++)
                            {
                                int localColTracker = 0;
                                for (int col = subs*3;col<(subs*3+3);col++)
                                {
                                    if (currentSize==3)
                                    {
                                        tempSquare[localRowTracker,localColTracker] = startingImage[row,col];
                                    }
                                    else tempSquare[localRowTracker,localColTracker] = finalIterationSquare[row,col];
                                    localColTracker++;
                                }
                                localRowTracker++;  
                            }
                            subsquares.Add((char[,])tempSquare.Clone());   
                            numSubsMade++; 
                        }   
                        
                    }               
                }

                
                List<int> transformsToAdd = new List<int>();
                foreach (char[,] s in subsquares)
                {
                    transformsToAdd.Add(MatchPattern(s, transformInputs));
                }
                
                //Combine into larger item
                int sizeOfSubsquareside = transformOutputs[transformsToAdd.First()].GetLength(0);
                int newSize = currentSize+currentSize/subsquares.First().GetLength(0);
                int numSquaresPerSide = newSize/sizeOfSubsquareside;
                int numSquaresRecombined = 0;
                finalIterationSquare = new char[newSize,newSize];
                while (numSquaresRecombined<numSubsquares)
                {
                    //Add row of subsquares
                    for (int sub=0;sub<numSquaresPerSide;sub++)
                    {
                        for (int row = 0;row<sizeOfSubsquareside;row++)
                        {
                            for (int col = 0;col<sizeOfSubsquareside;col++)
                            {
                                finalIterationSquare[numSquaresRecombined/numSquaresPerSide*sizeOfSubsquareside+row, sub*sizeOfSubsquareside+col] = transformOutputs[transformsToAdd[numSquaresRecombined]][row,col];
                            }
                            
                        }
                        numSquaresRecombined++;
                    }
                }
                currentSize = finalIterationSquare.GetLength(0);
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