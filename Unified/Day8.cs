using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day8
    {
        public static void TestDay8()
        {
            Console.WriteLine("-------------------DAY 8-------------------");
            
            StreamReader sr = new StreamReader("day8Input.txt");
            string line;

            List<Instruction> program = new List<Instruction>();
            while ((line = sr.ReadLine()) != null)
            {
                Instruction inst = CreateInstructionFromString(line);           
                program.Add(inst) ;
            }
            
            int highestValueEverSeen = 0;
            Dictionary<string, int> registerSet = new Dictionary<string, int>();
            foreach (Instruction i in program)
            {
                //Add increment operand and condition operand register to register set if we haven't seen them yet
                if (!registerSet.ContainsKey(i.operandRegister))
                {
                    registerSet.Add(i.operandRegister, 0);
                }
                if (!registerSet.ContainsKey(i.conditionOperand))
                {
                    registerSet.Add(i.conditionOperand, 0);
                }

                //Evaluate condition and decide if increment should happen
                bool shouldCarryOutAction = false;
                int conditionOperandCurrentValue = registerSet[i.conditionOperand];
                switch (i.conditionOperator)
                {
                    case ">":
                        shouldCarryOutAction = (conditionOperandCurrentValue>i.conditionValue);
                        break;
                    case "<":
                        shouldCarryOutAction = (conditionOperandCurrentValue<i.conditionValue);
                        break;
                    case ">=":
                        shouldCarryOutAction = (conditionOperandCurrentValue>=i.conditionValue);
                        break;
                    case "<=":
                        shouldCarryOutAction = (conditionOperandCurrentValue<=i.conditionValue);
                        break;
                    case "!=":
                        shouldCarryOutAction = (conditionOperandCurrentValue!=i.conditionValue);
                        break;
                    case "==":
                        shouldCarryOutAction = (conditionOperandCurrentValue==i.conditionValue);
                        break;
                }

                //Carry out action pending result of above evaluation
                if (shouldCarryOutAction)
                {
                    switch (i.operation)
                    {
                        case "inc":
                            registerSet[i.operandRegister] = registerSet[i.operandRegister] + i.incrementSize;
                            break;
                        case "dec":
                            registerSet[i.operandRegister] = registerSet[i.operandRegister] - i.incrementSize;
                            break;
                    }
                }

                //Find max Value in all registers, compare to tracker
                foreach (KeyValuePair<string,int> kvp in registerSet)
                {
                    if (registerSet[kvp.Key]>highestValueEverSeen){highestValueEverSeen = registerSet[kvp.Key];}
                }
            }

            //Find max value in register - Probably a much more efficient way to do this with Linq
            int maxValueSoFar = 0;
            foreach (KeyValuePair<string,int> kvp in registerSet)
            {
                if (registerSet[kvp.Key]>maxValueSoFar){maxValueSoFar = registerSet[kvp.Key];}
            }
            Console.WriteLine("Max value in any register after program is " + maxValueSoFar);
            Console.WriteLine("Max value ever seen " + highestValueEverSeen);
        }

        public static Instruction CreateInstructionFromString(string instText)
        {
            Instruction formedInstruction = new Instruction();
            char delimiter = ' ';
            string[] substrings = instText.Split(delimiter);

            formedInstruction.operandRegister = substrings[0];
            formedInstruction.operation = substrings[1];
            formedInstruction.incrementSize = Int32.Parse(substrings[2]);
            formedInstruction.conditionOperand = substrings[4];
            formedInstruction.conditionOperator = substrings[5];
            formedInstruction.conditionValue = Int32.Parse(substrings[6]);

            return formedInstruction;
        }

    }

    public class Instruction
    {
        public Instruction()
        {
            operandRegister = "";
            operation = "";
            incrementSize = 0;
            conditionOperand = "";
            conditionOperator = "";
            conditionValue = 0;
        }

        public string operandRegister;
        public string operation;
        public int incrementSize;
        public string conditionOperand;
        public string conditionOperator;
        public int conditionValue;
    }

}