using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day18
    {
        public static void TestDay18()
        {
            Console.WriteLine("-------------------DAY 18-------------------");

            List<Instruction> instructionSet = new List<Instruction>();

            StreamReader sr = new StreamReader("day18Input.txt");
            string line;
            char delimiter = ' ';
            while ((line = sr.ReadLine()) != null)
            {
                //Create new instruction and add
                string[] substrings = line.Split(delimiter);
                switch (substrings.Count())
                { 
                    case 1:
                        Instruction newInst1 = new Instruction(substrings[0],' ',"");
                        instructionSet.Add(newInst1);
                        break;
                    case 2:
                        Instruction newInst2 = new Instruction(substrings[0],substrings[1][0],"");
                        instructionSet.Add(newInst2);
                        break;
                    case 3:
                        Instruction newInst3 = new Instruction(substrings[0],substrings[1][0],substrings[2]);
                        instructionSet.Add(newInst3);
                        break;
                    default:
                        break;
                }
            }

            //Create new register set
            Dictionary<char,long> registerSet = new Dictionary<char, long>();

            //Process all instructions until next instruction is either <0 or ># instructions
            int nextInstructionIndex = 0;
            long lastFrequencyPlayed = 0;
            int numInstructionsProcessed = 0;
            while (nextInstructionIndex>=0 && nextInstructionIndex<instructionSet.Count)
            {
                Instruction currentInstruction = instructionSet[nextInstructionIndex];

                //If we haven't seen this register yet, initialize it and put it in the register set
                if (!registerSet.ContainsKey(currentInstruction.operand1))
                {
                    registerSet.Add(currentInstruction.operand1,0);
                }
                //TODO - May need to check to see if operand2 existis, also, unless they have checked we will never try somethign we haven't seen

                //Process instruction
                int parsedNum;
                switch (currentInstruction.instructionType)                
                {
                    case "snd":
                        lastFrequencyPlayed = registerSet[currentInstruction.operand1];
                        nextInstructionIndex++;
                        break;
                    case "set":
                        if (Int32.TryParse(currentInstruction.operand2, out parsedNum))
                        {
                            registerSet[currentInstruction.operand1] = (long)parsedNum;
                        }
                        else
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand2[0]];
                        }
                        nextInstructionIndex++;
                        break;
                    case "add":
                        if (Int32.TryParse(currentInstruction.operand2, out parsedNum))
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand1]+(long)parsedNum;
                        }
                        else
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand1] + registerSet[currentInstruction.operand2[0]];
                        }
                        nextInstructionIndex++;
                        break;
                    case "mul":
                        if (Int32.TryParse(currentInstruction.operand2, out parsedNum))
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand1]*(long)parsedNum;
                        }
                        else
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand1] * registerSet[currentInstruction.operand2[0]];
                        }
                        nextInstructionIndex++;
                        break;
                    case "mod":
                        if (Int32.TryParse(currentInstruction.operand2, out parsedNum))
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand1]%(long)parsedNum;
                        }
                        else
                        {
                            registerSet[currentInstruction.operand1] = registerSet[currentInstruction.operand1] % registerSet[currentInstruction.operand2[0]];
                        }
                        nextInstructionIndex++;
                        break;
                    case "rcv":
                        if (registerSet[currentInstruction.operand1]!=0)
                        {
                            Console.WriteLine("Recovering last frequency - "+lastFrequencyPlayed);
                        }
                        nextInstructionIndex++;
                        break;
                    case "jgz":
                        if (registerSet[currentInstruction.operand1]>0)
                        {
                            if (Int32.TryParse(currentInstruction.operand2, out parsedNum))
                            {
                                nextInstructionIndex+=parsedNum;
                            }
                            else
                            {
                                nextInstructionIndex+=(int)registerSet[currentInstruction.operand2[0]];
                            }
                        }
                        else {nextInstructionIndex++;}
                        break;
                    default:
                        break;
                }
            numInstructionsProcessed++;
            }
        }     

        public class Instruction
        {
            public Instruction(string inst, char o1, string o2)
            {
                instructionType = inst;
                operand1 = o1;
                operand2 = o2;
            }

            public string instructionType;
            public char operand1;
            public string operand2;
        }
    }

    
}
