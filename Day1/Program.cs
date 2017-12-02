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

            Console.WriteLine("Puzzle instance answer = " + outputSum.ToString());
        }
    }
}
