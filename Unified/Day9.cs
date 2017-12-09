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

            while (!sr.EndOfStream)
            {
                nextChar = (char)sr.Read();
                Console.WriteLine(nextChar);

                if (!previousCharWasValidGarbageBang)
                {
                    switch (nextChar)
                    {
                        //Likely need to take in midle of garbage into account in all of these
                        case '!':
                            if (inMiddleOfGarbage){previousCharWasValidGarbageBang = true;Console.WriteLine("Found Valid Garbage Bang!");}
                            break;
                        case '{':
                            if (!inMiddleOfGarbage){currentNumberOpenGroups++;Console.WriteLine("Opening Group! " + currentNumberOpenGroups);}
                            break;
                        case '}':
                            if (!inMiddleOfGarbage)
                            {
                                runningScore += currentNumberOpenGroups;
                                Console.WriteLine("Closed group! " + currentNumberOpenGroups);
                                currentNumberOpenGroups--;
                                numberFullGroupsSeen++;
                            }
                            break;
                        case '<':
                            if (!inMiddleOfGarbage){ inMiddleOfGarbage = true;Console.WriteLine("Found garbage!");}
                            break;
                        case '>':
                            if (inMiddleOfGarbage){ inMiddleOfGarbage = false;Console.WriteLine("Closed garbage!");}
                            break;
                        default:
                            break;
                    }
                }
                else if (previousCharWasValidGarbageBang){previousCharWasValidGarbageBang = false;}

            }

            Console.WriteLine("Total score found - " + runningScore);
        }

    }

}