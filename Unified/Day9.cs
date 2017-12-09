using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day9
    {
        public static void TestDay9()
        {
            Console.WriteLine("-------------------DAY 9-------------------");
            
            StreamReader sr = new StreamReader("day9Input.txt");
            char nextChar;

            int numberFullGroupsSeen = 0;
            int currentNumberOpenGroups = 0;

            bool previousCharWasValidGarbageBang = false;
            bool inMiddleOfGarbage = false;

            int runningScore = 0;
            int numValidCharsInGarbage = 0;

            while (!sr.EndOfStream)
            {
                nextChar = (char)sr.Read();

                if (!previousCharWasValidGarbageBang)
                {
                    switch (nextChar)
                    {
                        case '!':
                            if (inMiddleOfGarbage){previousCharWasValidGarbageBang = true;}
                            break;
                        case '{':
                            if (!inMiddleOfGarbage){currentNumberOpenGroups++;}
                            if (inMiddleOfGarbage){numValidCharsInGarbage++;}
                            break;
                        case '}':
                            if (!inMiddleOfGarbage)
                            {
                                runningScore += currentNumberOpenGroups;
                                currentNumberOpenGroups--;
                                numberFullGroupsSeen++;
                            }
                            if (inMiddleOfGarbage){numValidCharsInGarbage++;}
                            break;
                        case '<':
                            if (!inMiddleOfGarbage){ inMiddleOfGarbage = true;}
                            else if (inMiddleOfGarbage){numValidCharsInGarbage++;}
                            break;
                        case '>':
                            if (inMiddleOfGarbage){ inMiddleOfGarbage = false;}
                            break;
                        default:
                            if (inMiddleOfGarbage){numValidCharsInGarbage++;}
                            break;
                    }
                }
                else if (previousCharWasValidGarbageBang){previousCharWasValidGarbageBang = false;}

            }

            Console.WriteLine("Total score found - " + runningScore);
            Console.WriteLine("Total valid chars in garbage found - " + numValidCharsInGarbage);
        }

    }

}