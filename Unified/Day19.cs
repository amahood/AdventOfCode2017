using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day19
    {
        public static void TestDay19()
        {
            Console.WriteLine("-------------------DAY 19-------------------");

            //Goal is I want to end up filling up a 2D char array
            //Read in a bunch of lines, go through and put in 2D array
            List<string> initialMap = new List<string>();

            StreamReader sr = new StreamReader("day19Input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                initialMap.Add(line);
                Console.WriteLine(line);
            }

            //Put into 2d char array
            int numLines = initialMap.Count;
            int maxLineSize = 0;
            foreach (string s in initialMap)
            {
                if (s.Length>maxLineSize)
                {
                    maxLineSize = s.Length;
                }
            }
            char[,] initialCharMap = new char[numLines,maxLineSize];
            for (int row = 0; row<numLines; row++)
            {
                for (int column = 0;column<maxLineSize;column++)
                {
                    initialCharMap[row,column] = initialMap[row][column];
                }
            }

            //First, find first position of entry
            int startingColumn = 0;
            for (int start = 0; start<maxLineSize;start++)
            {
                if (initialCharMap[0,start]=='|')
                {
                    startingColumn = start;
                    Console.WriteLine("Starting at 0,"+startingColumn);
                }
            }

            //Now follow the map!!!
            List<char> charsEncountered = new List<char>();
            bool foundEnd = false;
            int currentRow = 0;
            int currentColumn = startingColumn;
            string currentDirection = "Down";
            char nextChar;
            int numSteps = 1;
            while (!foundEnd)
            {
                switch (currentDirection)
                {
                    case "Down":
                        currentRow++;
                        break;
                    case "Up":
                        currentRow--;
                        break;
                    case "Left":
                        currentColumn--;
                        break;
                    case "Right":
                        currentColumn++;
                        break;
                }

                nextChar = initialCharMap[currentRow,currentColumn];

                switch (nextChar)
                {
                    case '|':
                        break;
                    case '-':
                        break;
                    case '+':
                        currentDirection = NextDirectionAtFork(currentRow,currentColumn, initialCharMap, currentDirection);
                        break;
                    case ' ':
                        Console.WriteLine("Found the end???");
                        foundEnd = true;
                        break;
                    default:
                        //Check if we have an alphabetic character
                        if (nextChar>=65 && nextChar<91)
                        {
                            Console.WriteLine("Found letter - "+nextChar);
                            charsEncountered.Add(nextChar);
                        }
                        else
                        {
                            Console.WriteLine("Error condition, unsure where we're at???");
                        }
                        break;
                }
                numSteps++;
            }
            Console.WriteLine("Number of steps traveled - " + (numSteps-1));
        }    


        public static string NextDirectionAtFork(int x, int y, char[,] map, string currentDir)
        {
            Dictionary<string, char> neighbors = new Dictionary<string, char>();
            switch (currentDir)
            {
                case "Down":
                    if (x<map.GetLength(0)-1){neighbors.Add("Down",map[x+1,y]);}
                    if (y>0){neighbors.Add("Left",map[x,y-1]);}
                    if (y<map.GetLength(1)-1){neighbors.Add("Right",map[x,y+1]);}
                    break;
                case "Up":
                    if (x>0){neighbors.Add("Up",map[x-1,y]);}
                    if (y>0){neighbors.Add("Left",map[x,y-1]);}
                    if (y<map.GetLength(1)-1){neighbors.Add("Right",map[x,y+1]);}
                    break;
                case "Left":
                    if (x<map.GetLength(0)-1){neighbors.Add("Down",map[x+1,y]);}
                    if (y>0){neighbors.Add("Left",map[x,y-1]);}
                    if (x>0){neighbors.Add("Up",map[x-1,y]);}
                    break;
                case "Right":
                    if (x<map.GetLength(0)-1){neighbors.Add("Down",map[x+1,y]);}
                    if (y<map.GetLength(1)-1){neighbors.Add("Right",map[x,y+1]);}
                    if (x>0){neighbors.Add("Up",map[x-1,y]);}
                    break;
            }
            bool foundNextDir = false;
            //Could also consider adding a linq query here to find the non-0 one and fail if there are more than one
            foreach (KeyValuePair<string,char> kvp in neighbors)
            {
                if (kvp.Value != ' ' && !foundNextDir)
                {
                    foundNextDir = true;
                    return kvp.Key;
                }
            }
            
            Console.WriteLine("Likely error condition detected, we should alwaysw have a place to go at +");
            return "Fail";
            
        }

    }
}

