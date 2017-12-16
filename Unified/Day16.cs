using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day16
    {
        public static void TestDay16()
        {
            Console.WriteLine("-------------------DAY 16-------------------");

            string initialKickLine = PopulateDanceLine(16);

            StreamReader sr = new StreamReader("day16Input.txt");
            string line;
            string stringOfInstructions="";
            while ((line = sr.ReadLine()) != null)
            {
                stringOfInstructions = stringOfInstructions+line;
            }

            char delimiter = ',';
            string[] instructions = stringOfInstructions.Split(delimiter);

            for (int i = 0; i<instructions.Count(); i++)
            {
                string currentInstruction = instructions[i];
                switch (currentInstruction[0])
                {
                    
                    case 's':
                        {
                            int numberOfDancersToSpin = Int32.Parse(currentInstruction.Substring(1, currentInstruction.Count()-1));
                            string dancersToSpin = initialKickLine.Substring(initialKickLine.Count()-numberOfDancersToSpin,numberOfDancersToSpin);
                            string newStringEnd = initialKickLine.Substring(0,initialKickLine.Count()-numberOfDancersToSpin);
                            initialKickLine=dancersToSpin+newStringEnd;
                        }
                        break;
                    case 'x':
                        {
                            int indexOfSlash = currentInstruction.IndexOf('/');
                            int indexOfFirstSwapper = Int32.Parse(currentInstruction.Substring(1,indexOfSlash-1));
                            int indexOfSecondSwapper = Int32.Parse(currentInstruction.Substring(indexOfSlash+1,currentInstruction.Length-indexOfSlash-1));
                            char firstSwapper = initialKickLine[indexOfFirstSwapper];
                            char secondSwapper = initialKickLine[indexOfSecondSwapper];
                            StringBuilder sb = new StringBuilder(initialKickLine);
                            sb[indexOfFirstSwapper] = secondSwapper;
                            sb[indexOfSecondSwapper] = firstSwapper;
                            initialKickLine = sb.ToString();
                        }
                        break;
                    case 'p':
                        {
                            char nameOfFirstSwapper = currentInstruction[1];
                            char nameOFSecondSwapper = currentInstruction[3];
                            int indexOfFirstSwapper = initialKickLine.IndexOf(nameOfFirstSwapper);
                            int indexOfSecondSwapper = initialKickLine.IndexOf(nameOFSecondSwapper);
                            StringBuilder sb= new StringBuilder(initialKickLine);
                            sb[indexOfFirstSwapper] = nameOFSecondSwapper;
                            sb[indexOfSecondSwapper] = nameOfFirstSwapper;
                            initialKickLine = sb.ToString();
                        }
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Dance line after all dance moves! = " + initialKickLine);
        }

        public static string PopulateDanceLine(int numberOfDancers)
        {
            string initialDanceLine ="";

            for (int i = 0;i<numberOfDancers; i++)
            {
                char nextDancer = (char)('a'+i);
                initialDanceLine = initialDanceLine+nextDancer;
            }

            return initialDanceLine;
        }
      
    }

    

}
