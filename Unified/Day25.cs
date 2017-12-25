using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day25
    {
        public static void TestDay25()
        {
            Console.WriteLine("-------------------DAY 25-------------------");

            char currentState = 'A';
            char nextState = 'A';
            int currentTapeVal = 0;
            int currentTapePosition = 0; //Position is relative to starting position, right is +, left is -

            Dictionary<int,int> turingTape = new Dictionary<int, int>(); //Will use the format of Position,Value for entries

            for (int i = 0; i<12173597; i++)
            {
                currentState = nextState;
                int tapeValueIfPresent = 0;
                bool tapeContainsCurrentLocation = turingTape.TryGetValue(currentTapePosition, out tapeValueIfPresent);
                if (tapeContainsCurrentLocation)
                {
                    currentTapeVal = tapeValueIfPresent;
                }
                else
                {
                    currentTapeVal = 0;
                    turingTape.Add(currentTapePosition,currentTapeVal);
                }

                switch (currentState)
                {
                    case 'A':
                        if (currentTapeVal==0)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition++;
                            nextState = 'B';
                        }
                        else if (currentTapeVal==1)
                        {
                            turingTape[currentTapePosition] = 0;
                            currentTapePosition--;
                            nextState = 'C';
                        }
                        break;
                    case 'B':
                        if (currentTapeVal==0)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition--;
                            nextState = 'A';
                        }
                        else if (currentTapeVal==1)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition++;
                            nextState = 'D';
                        }
                        break;
                    case 'C':
                        if (currentTapeVal==0)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition++;
                            nextState = 'A';
                        }
                        else if (currentTapeVal==1)
                        {
                            turingTape[currentTapePosition] = 0;
                            currentTapePosition--;
                            nextState = 'E';
                        }
                        break;
                    case 'D':
                        if (currentTapeVal==0)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition++;
                            nextState = 'A';
                        }
                        else if (currentTapeVal==1)
                        {
                            turingTape[currentTapePosition] = 0;
                            currentTapePosition++;
                            nextState = 'B';
                        }
                        break;
                    case 'E':
                        if (currentTapeVal==0)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition--;
                            nextState = 'F';
                        }
                        else if (currentTapeVal==1)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition--;
                            nextState = 'C';
                        }
                        break;
                    case 'F':
                        if (currentTapeVal==0)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition++;
                            nextState = 'D';
                        }
                        else if (currentTapeVal==1)
                        {
                            turingTape[currentTapePosition] = 1;
                            currentTapePosition++;
                            nextState = 'A';
                        }
                        break;
                }
            }
    
            int diagnosticChecksum = 0;
            foreach (KeyValuePair<int,int> kvp in turingTape)
            {
                if (kvp.Value==1)
                {
                    diagnosticChecksum++;
                }
            }

            Console.WriteLine("Diagnostic checksum is - "+diagnosticChecksum);

        }

    }


}




