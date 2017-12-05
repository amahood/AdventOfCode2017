using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day5
    {
        public static void TestDay5()
        {
            Console.WriteLine("-------------------DAY 5-------------------");

            StreamReader sr = new StreamReader("day5Input.txt");
            string line;
            List<int> instructionList = new List<int>();

            while ((line = sr.ReadLine()) != null)
            {
                 instructionList.Add(Int32.Parse(line));
            }

            int instructionPointer = 0;
            int currentInstruction = 0;
            int instructionCounter = 0;

            while (instructionPointer<instructionList.Count)
            {   
                currentInstruction = instructionList[instructionPointer];

                if (currentInstruction==0)
                {
                    instructionList[instructionPointer]++;
                }
                else if (currentInstruction>=3)
                {
                    instructionList[instructionPointer]--;
                    instructionPointer+=currentInstruction;
                }
                else
                {
                    instructionList[instructionPointer]++;
                    instructionPointer+=currentInstruction;
                }

                instructionCounter++;
            }
            Console.WriteLine("It took "+instructionCounter+" instructions to service the IRQ!");
        }
    }
}