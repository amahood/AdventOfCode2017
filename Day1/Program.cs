using System;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code Day 1 Challenge");

            string puzzleInput = args[0];
            Console.WriteLine("Your puzzle input is " + puzzleInput);
        
            //Validate input is all digits
            if ( (puzzleInput.Length == 0) || (puzzleInput.Length==1) || (puzzleInput.All(char.IsDigit) == false) )
            {
                Console.WriteLine("Bad puzzle input :(, must be n>1 numbers...bailing");
                return;
            }

            //Part 1 - Compute sum of like adjacent digits
            long outputSum = 0;
            int i = 0;
            while (i < (puzzleInput.Length-1))
            {
                if (puzzleInput[i] == puzzleInput[i+1])
                {
                    outputSum = outputSum + (puzzleInput[i]-'0');
                }   
                i++;             
            }

            if (puzzleInput[0] == puzzleInput.Last()) { outputSum = outputSum + puzzleInput.Last()-'0';}

            Console.WriteLine("Puzzle instance Part 1 answer = " + outputSum.ToString());
            Console.WriteLine();

            //Part 2 - Compute sum of like digits that are halfway around the list - Assumption is it is even number
            int j = 0;
            int half = puzzleInput.Length/2;
            int outputSum2 = 0;
            while (j<puzzleInput.Length)
            {
                if ((j + half)<puzzleInput.Length)
                {
                    if (puzzleInput[j] == puzzleInput[j+half])
                    {
                        outputSum2 = outputSum2 + (puzzleInput[j]-'0');
                    }
                }
                else
                {
                    if (puzzleInput[j] == puzzleInput[j-half])
                    {
                        outputSum2 = outputSum2 + (puzzleInput[j]-'0');
                    }
                }
                j++;
            }
    
            Console.WriteLine("Puzzle instance Part 2 answer = " + outputSum2.ToString());
            Console.WriteLine();

        }
    }
}
