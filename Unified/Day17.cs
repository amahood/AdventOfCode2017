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

            int incrementStep = 337; //Ultiamtely this gets changed to my puzzle input
            int numCyclesToRun = 2017; //Ultimately this changes to 2017, just using 9 to work through test case
            int currentLocationIndex = 0;
            int nextNewValue = 1;

            for (int i = 0; i<numCyclesToRun;i++)
            {
                //Get min number of steps to move via modulo (number past full revolution)
                int moduloCycleCount = 0;
                if (circularBuffer.Count==1 || (circularBuffer.Count==incrementStep))
                {
                    moduloCycleCount=0;
                }
                else if (incrementStep>circularBuffer.Count)
                {
                    moduloCycleCount = incrementStep%circularBuffer.Count;
                }
                else if (circularBuffer.Count>incrementStep)
                {
                    moduloCycleCount = incrementStep;
                }
                 

                if (currentLocationIndex+moduloCycleCount > circularBuffer.Count-1)
                {
                    currentLocationIndex = (currentLocationIndex+moduloCycleCount)-circularBuffer.Count;
                }
                else
                {
                    currentLocationIndex = currentLocationIndex + moduloCycleCount;
                }

                //Insert next value in next index position
                List<int> buildingBuffer = new List<int>();
                for (int j = 0;j<(currentLocationIndex+1);j++)
                {
                    buildingBuffer.Add(circularBuffer[j]);
                }
                buildingBuffer.Add(nextNewValue);
                for (int k = currentLocationIndex+1;k<circularBuffer.Count;k++)
                {
                    buildingBuffer.Add(circularBuffer[k]);
                }
                

                circularBuffer.Clear();
                foreach (int newNum in buildingBuffer)
                {
                    circularBuffer.Add(newNum);
                }

                //Make current location index the next spot and copy List over
                currentLocationIndex++;
                nextNewValue++;
            }

            int nextManUp = circularBuffer[currentLocationIndex+1];
            Console.WriteLine("Value after 2017 = " + nextManUp);

        }     
    }
}
