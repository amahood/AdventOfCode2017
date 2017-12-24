using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day23
    {
        public static void TestDay23()
        {
            Console.WriteLine("-------------------DAY 23-------------------");

            //TESTING LOOPS
            int d=0;
            int b = 108400;
            int f = 0;
            int h = 0;
            int e =2;
            for (int o = 0;o<1000;o++)
            {
                f = 1;
                d = 2;
                for (int m = 0;m<108398;m++)
                {
                    e = 2;
                    for (int i = 0;i<108398;i++)
                    {
                        if (d*e==b)
                        {
                            f = 0;
                        }
                        e++;
                    }
                    d++;
                }

                if (f==0)
                {
                    h++;
                    Console.WriteLine("Loop - "+o+", h now "+h);
                }
            }


            List<Instruction23> instructionList = new List<Instruction23>();

            StreamReader sr = new StreamReader("day23Input.txt");
            string line;
            char delimiter = ' ';
            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(delimiter);
                Instruction23 newInst3 = new Instruction23(substrings[0],substrings[1],substrings[2]);
                instructionList.Add(newInst3);
            }

            int numberOfInstructionsInProg = instructionList.Count;
            int numberOfMulInstructions = 0;

            //Initialize register set
            Dictionary<char,long> registerSet = new Dictionary<char, long>();
            registerSet.Add('a',1);
            registerSet.Add('b',0);
            registerSet.Add('c',0);
            registerSet.Add('d',0);
            registerSet.Add('e',0);
            registerSet.Add('f',0);
            registerSet.Add('g',0);
            registerSet.Add('h',0);

            int programCounter = 0;

            while (programCounter>=0 && programCounter<numberOfInstructionsInProg)
            {
                Instruction23 nextInstruction = instructionList[programCounter];
                int parsedNum = 0;

                switch (nextInstruction.instructionType)
                {
                    case "set":
                        if (Int32.TryParse(nextInstruction.operand2, out parsedNum))
                        {
                            registerSet[nextInstruction.operand1[0]] = (long)parsedNum;
                        }
                        else
                        {
                            registerSet[nextInstruction.operand1[0]] = registerSet[nextInstruction.operand2[0]];
                        }
                        programCounter++;
                        break;
                    case "sub":
                        if (Int32.TryParse(nextInstruction.operand2, out parsedNum))
                        {
                            registerSet[nextInstruction.operand1[0]] = registerSet[nextInstruction.operand1[0]]-(long)parsedNum;
                        }
                        else
                        {
                            registerSet[nextInstruction.operand1[0]] = registerSet[nextInstruction.operand1[0]] - registerSet[nextInstruction.operand2[0]];
                        }
                        programCounter++;
                        break;
                    case "mul":
                        if (Int32.TryParse(nextInstruction.operand2, out parsedNum))
                        {
                            registerSet[nextInstruction.operand1[0]] = registerSet[nextInstruction.operand1[0]]*(long)parsedNum;
                        }
                        else
                        {
                            registerSet[nextInstruction.operand1[0]] = registerSet[nextInstruction.operand1[0]] * registerSet[nextInstruction.operand2[0]];
                        }
                        numberOfMulInstructions++;
                        programCounter++;
                        break;
                    case "jnz":
                        if (!Int32.TryParse(nextInstruction.operand1.ToString(), out parsedNum))
                            {
                                if (registerSet[nextInstruction.operand1[0]]!=0)
                                {
                                    if (Int32.TryParse(nextInstruction.operand2, out parsedNum))
                                    {
                                        programCounter+=parsedNum;
                                    }
                                    else
                                    {
                                        programCounter+=(int)registerSet[nextInstruction.operand2[0]];
                                    }
                                }
                                else {programCounter++;}
                            }
                            else if (Int32.TryParse(nextInstruction.operand1.ToString(), out parsedNum))
                            {
                                if (parsedNum!=0)
                                {
                                    if (Int32.TryParse(nextInstruction.operand2, out parsedNum))
                                    {
                                        programCounter+=parsedNum;
                                    }
                                    else
                                    {
                                        programCounter+=(int)registerSet[nextInstruction.operand2[0]];
                                    }
                                }
                                else {programCounter++;}
                            }
                        break;
                }
                //Console.WriteLine("Program Counter - "+programCounter);
                //Console.WriteLine("Value of h at the end of the instruction - " + registerSet['h']);
            }

            Console.WriteLine("Number of mul instructions processed - " + numberOfMulInstructions);
            Console.WriteLine("Value of h at the end of the program - " + registerSet['h']);
        }


    }

    public class Instruction23
    {
        public Instruction23(string inst, string o1, string o2)
        {
            instructionType = inst;
            operand1 = o1;
            operand2 = o2;
        }

        public string instructionType;
        public string operand1;
        public string operand2;
    }

}




