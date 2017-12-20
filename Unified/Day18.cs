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

            //Create new register sets and message queues for each
            Dictionary<char,long> registerSetProg0 = new Dictionary<char, long>();
            registerSetProg0.Add('p',0);
            Dictionary<char,long> registerSetProg1 = new Dictionary<char, long>();
            registerSetProg1.Add('p',1);
            Queue<long> prog0IncomingQueue = new Queue<long>();
            Queue<long> prog1IncomingQueue = new Queue<long>();

            //Looks like we will need send value trackers for PArt 2, so initializing them
            int messagesSentByProg0 = 0;
            int messagesSentByProg1 = 0;

            
            int nextInstructionIndexProg0 = 0;
            int nextInstructionIndexProg1 = 0;
            bool prog0WaitingForMessages = false;
            bool prog1WaitingForMessages = false;

            //Process all instructions until next instruction is either <0 or ># instructions, or both instructions are waiting for sends
            while ( ((nextInstructionIndexProg0>=0 && nextInstructionIndexProg0<instructionSet.Count) || (nextInstructionIndexProg1>=0 && nextInstructionIndexProg1<instructionSet.Count)) && (!prog0WaitingForMessages || !prog1WaitingForMessages) )
            {
                Instruction currentInstructionProg0 = instructionSet[nextInstructionIndexProg0];
                Instruction currentInstructionProg1 = instructionSet[nextInstructionIndexProg1];

                //If we haven't seen this register yet, initialize it and put it in the register set (For both register sets)
                int parsedOperand;
                if ( !(Int32.TryParse(currentInstructionProg0.operand1.ToString(), out parsedOperand)))
                {
                    if (!registerSetProg0.ContainsKey(currentInstructionProg0.operand1))
                    {
                        registerSetProg0.Add(currentInstructionProg0.operand1,0);
                    }
                }
                if ( !(Int32.TryParse(currentInstructionProg1.operand1.ToString(), out parsedOperand)))
                {
                    if (!registerSetProg1.ContainsKey(currentInstructionProg1.operand1))
                    {
                        registerSetProg1.Add(currentInstructionProg1.operand1,0);
                    }
                }

                //Process instruction for Prog0
                int parsedNum;
                if (!prog0WaitingForMessages || (prog0IncomingQueue.Count!=0))
                {
                    switch (currentInstructionProg0.instructionType)                
                    {
                        case "snd":
                            if (Int32.TryParse(currentInstructionProg0.operand1.ToString(), out parsedNum))
                            {
                                prog1IncomingQueue.Enqueue((long)parsedNum);
                                //Console.WriteLine("Program 0 sent "+(long)parsedNum);
                            }
                            else
                            {
                                prog1IncomingQueue.Enqueue(registerSetProg0[currentInstructionProg0.operand1]);
                                //Console.WriteLine("Program 0 sent "+registerSetProg0[currentInstructionProg0.operand1]);
                            }
                            messagesSentByProg0++;
                            nextInstructionIndexProg0++;
                            break;
                        case "set":
                            if (Int32.TryParse(currentInstructionProg0.operand2, out parsedNum))
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = (long)parsedNum;
                            }
                            else
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand2[0]];
                            }
                            nextInstructionIndexProg0++;
                            break;
                        case "add":
                            if (Int32.TryParse(currentInstructionProg0.operand2, out parsedNum))
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand1]+(long)parsedNum;
                            }
                            else
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand1] + registerSetProg0[currentInstructionProg0.operand2[0]];
                            }
                            nextInstructionIndexProg0++;
                            break;
                        case "mul":
                            if (Int32.TryParse(currentInstructionProg0.operand2, out parsedNum))
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand1]*(long)parsedNum;
                            }
                            else
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand1] * registerSetProg0[currentInstructionProg0.operand2[0]];
                            }
                            nextInstructionIndexProg0++;
                            break;
                        case "mod":
                            if (Int32.TryParse(currentInstructionProg0.operand2, out parsedNum))
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand1]%(long)parsedNum;
                            }
                            else
                            {
                                registerSetProg0[currentInstructionProg0.operand1] = registerSetProg0[currentInstructionProg0.operand1] % registerSetProg0[currentInstructionProg0.operand2[0]];
                            }
                            nextInstructionIndexProg0++;
                            break;
                        case "rcv":
                            if (prog0IncomingQueue.Count!=0)
                            {
                                prog0WaitingForMessages = false;
                                long nextValueToReceive = prog0IncomingQueue.Dequeue();
                                registerSetProg0[currentInstructionProg0.operand1] = nextValueToReceive;
                                //Console.WriteLine("Program 0 received "+nextValueToReceive);
                                nextInstructionIndexProg0++;
                            }
                            else
                            {
                                prog0WaitingForMessages = true;
                                //Console.WriteLine("Program 0 waiting for messages :(");
                            }
                            
                            break;
                        case "jgz":
                            if (!Int32.TryParse(currentInstructionProg0.operand1.ToString(), out parsedNum))
                            {
                                if (registerSetProg0[currentInstructionProg0.operand1]>0)
                                {
                                    if (Int32.TryParse(currentInstructionProg0.operand2, out parsedNum))
                                    {
                                        nextInstructionIndexProg0+=parsedNum;
                                    }
                                    else
                                    {
                                        nextInstructionIndexProg0+=(int)registerSetProg0[currentInstructionProg0.operand2[0]];
                                    }
                                }
                                else {nextInstructionIndexProg0++;}
                            }
                            else if (Int32.TryParse(currentInstructionProg0.operand1.ToString(), out parsedNum))
                            {
                                if (parsedNum>0)
                                {
                                    if (Int32.TryParse(currentInstructionProg0.operand2, out parsedNum))
                                    {
                                        nextInstructionIndexProg0+=parsedNum;
                                    }
                                    else
                                    {
                                        nextInstructionIndexProg0+=(int)registerSetProg0[currentInstructionProg0.operand2[0]];
                                    }
                                }
                                else {nextInstructionIndexProg0++;}
                            }

                            break;
                        default:
                            break;
                    }
                }

                //Process instruction for Prog1
                if (!prog1WaitingForMessages || (prog1IncomingQueue.Count!=0))
                {
                    switch (currentInstructionProg1.instructionType)                
                    {
                        case "snd":
                            if (Int32.TryParse(currentInstructionProg1.operand1.ToString(), out parsedNum))
                            {
                                prog0IncomingQueue.Enqueue((long)parsedNum);
                                //Console.WriteLine("Program 1 sent "+(long)parsedNum);
                            }
                            else
                            {
                                prog0IncomingQueue.Enqueue(registerSetProg1[currentInstructionProg1.operand1]);
                                //Console.WriteLine("Program 1 sent "+registerSetProg1[currentInstructionProg1.operand1]);
                            }
                            messagesSentByProg1++;
                            nextInstructionIndexProg1++;
                            break;
                        case "set":
                            if (Int32.TryParse(currentInstructionProg1.operand2, out parsedNum))
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = (long)parsedNum;
                            }
                            else
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand2[0]];
                            }
                            nextInstructionIndexProg1++;
                            break;
                        case "add":
                            if (Int32.TryParse(currentInstructionProg1.operand2, out parsedNum))
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand1]+(long)parsedNum;
                            }
                            else
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand1] + registerSetProg1[currentInstructionProg1.operand2[0]];
                            }
                            nextInstructionIndexProg1++;
                            break;
                        case "mul":
                            if (Int32.TryParse(currentInstructionProg1.operand2, out parsedNum))
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand1]*(long)parsedNum;
                            }
                            else
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand1] * registerSetProg1[currentInstructionProg1.operand2[0]];
                            }
                            nextInstructionIndexProg1++;
                            break;
                        case "mod":
                            if (Int32.TryParse(currentInstructionProg1.operand2, out parsedNum))
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand1]%(long)parsedNum;
                            }
                            else
                            {
                                registerSetProg1[currentInstructionProg1.operand1] = registerSetProg1[currentInstructionProg1.operand1] % registerSetProg1[currentInstructionProg1.operand2[0]];
                            }
                            nextInstructionIndexProg1++;
                            break;
                        case "rcv":
                            if (prog1IncomingQueue.Count!=0)
                            {   
                                prog1WaitingForMessages = false;
                                long nextValueToReceive = prog1IncomingQueue.Dequeue();
                                registerSetProg1[currentInstructionProg1.operand1] = nextValueToReceive;
                                //Console.WriteLine("Program 1 received "+nextValueToReceive);
                                nextInstructionIndexProg1++;
                            }
                            else
                            {
                                prog1WaitingForMessages = true;
                                //Console.WriteLine("Program 1 waiting for messages :(");
                            }
                            
                            break;
                        case "jgz":
                             if (!Int32.TryParse(currentInstructionProg1.operand1.ToString(), out parsedNum))
                            {
                                if (registerSetProg1[currentInstructionProg1.operand1]>0)
                                {
                                    if (Int32.TryParse(currentInstructionProg1.operand2, out parsedNum))
                                    {
                                        nextInstructionIndexProg1+=parsedNum;
                                    }
                                    else
                                    {
                                        nextInstructionIndexProg1+=(int)registerSetProg1[currentInstructionProg1.operand2[0]];
                                    }
                                }
                                else {nextInstructionIndexProg1++;}
                            }
                            else if (Int32.TryParse(currentInstructionProg1.operand1.ToString(), out parsedNum))
                            {
                                if (parsedNum>0)
                                {
                                    if (Int32.TryParse(currentInstructionProg1.operand2, out parsedNum))
                                    {
                                        nextInstructionIndexProg1+=parsedNum;
                                    }
                                    else
                                    {
                                        nextInstructionIndexProg1+=(int)registerSetProg1[currentInstructionProg1.operand2[0]];
                                    }
                                }
                                else {nextInstructionIndexProg1++;}
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.WriteLine("Program 0 sent " + messagesSentByProg0 + " messages!");
            Console.WriteLine("Program 1 sent " + messagesSentByProg1 + " messages!");
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
