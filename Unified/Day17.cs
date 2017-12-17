using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day17
    {
        public static void TestDay17()
        {
            Console.WriteLine("-------------------DAY 17-------------------");

            List<int> circularBuffer = new List<int>();
            circularBuffer.Add(0);

            int incrementStep = 337; 
            int numCyclesToRun = 50000000;
            int currentLocationIndex = 0;
            int nextNewValue = 1;
            int bufferSize = 1;
            int latestCycleToTouch0 = 0;

            //For part 2, I don't need to actually keep track of the list, all I need to know is eveyrtime I land on 0 as my current location after move
            for (int i = 0; i<numCyclesToRun;i++)
            {
                //Get min number of steps to move via modulo (number past full revolution)
                int moduloCycleCount = 0;
                if (bufferSize==1 || (bufferSize==incrementStep))
                {
                    moduloCycleCount=0;
                }
                else if (incrementStep>bufferSize)
                {
                    moduloCycleCount = incrementStep%bufferSize;
                }
                else if (bufferSize>incrementStep)
                {
                    moduloCycleCount = incrementStep;
                }
                 

                if (currentLocationIndex+moduloCycleCount > bufferSize-1)
                {
                    currentLocationIndex = (currentLocationIndex+moduloCycleCount)-bufferSize;
                }
                else
                {
                    currentLocationIndex = currentLocationIndex + moduloCycleCount;
                }


                if (currentLocationIndex==0)
                {
                    latestCycleToTouch0 = nextNewValue;
                }

                currentLocationIndex++;
                nextNewValue++;
                bufferSize++;
            }

            Console.WriteLine("Last cycle to touch 0, which is next number - "+latestCycleToTouch0);
        }     
    }
}
