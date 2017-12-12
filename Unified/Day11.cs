using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day11
    {
        public static void TestDay11()
        {
            Console.WriteLine("-------------------DAY 11-------------------");
            
            //Parse input, assume well-formed and comma separated
            StreamReader sr = new StreamReader("day11Input.txt");
            char delimiter = ',';
            string line;
            List<string> childPath = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(delimiter);
                foreach (string s in substrings)
                {
                    childPath.Add(s);
                }
            }

            //High-level approach is to calculate row and column of where he is - effectively treat like rectangular coordinates, even though there are angles
            //Treating due north and south as +/-2
            //First, create x/y tracker variables
            int currentX = 0;
            int currentY = 0;
            int maxDistance = 0;
            int currentDistance = 0;

            foreach (string step in childPath)
            {
                switch (step)
                {
                    case "n":
                        currentY+=2;
                        break;
                    case "s":
                        currentY-=2;
                        break;
                    case "ne":
                        currentX++;
                        currentY++;
                        break;
                    case "se":
                        currentX++;
                        currentY--;
                        break;
                    case "nw":
                        currentX--;
                        currentY++;
                        break;
                    case "sw":
                        currentX--;
                        currentY--;
                        break;
                }
                currentDistance = FindDistanceFromHome(currentX, currentY);
                if (currentDistance>maxDistance)
                {
                    maxDistance = currentDistance;
                }
            }
    
            Console.WriteLine("Max distance lost child got from home - " + maxDistance);
            Console.WriteLine("Number of steps to lost child at end - " + currentDistance);
        }   

        public static int FindDistanceFromHome(int currentX, int currentY)
        {
            int numDiagonals = 0;
            int numVertical = 0;
            int numHoriz = 0;
            if (Math.Abs(currentX)==Math.Abs(currentY))
            {
                numDiagonals = Math.Abs(currentX);
            }
            else if (currentX==0)
            {
                numVertical = Math.Abs(currentY)/2;
            }
            else if (currentY==0)
            {
                numHoriz = Math.Abs(currentX);
            }
            //Handle north wedge
            else if ( (currentY > currentX) && (currentY > (-1*currentX)) && (currentY>0))
            {
                numDiagonals = Math.Abs(currentX);
                numVertical = (Math.Abs(currentY)-Math.Abs(currentX))/2;
            }
            //Handle south wedge
            else if ( ((-1*currentY)>(-1*currentX)) && ((-1*currentY)>(currentX)) && (currentY<0))
            {
                numDiagonals = Math.Abs(currentX);
                numVertical = (Math.Abs(currentY)-Math.Abs(currentX))/2;
            }
            //Handle east wedge
            else if ( (currentX > currentY) && (currentX> (-1*currentY)) && (currentX>0))
            {
                numDiagonals = Math.Abs(currentY);
                numVertical = (Math.Abs(currentX)-Math.Abs(currentY));
            }
            //Handle west wedge
            else if ( ((-1*currentX)>(currentX)) && ((-1*currentX)>(-1*currentY)) && (currentX<0))
            {
                numDiagonals = Math.Abs(currentY);
                numVertical = (Math.Abs(currentX)-Math.Abs(currentY));
            }
            int numTotalSteps = numDiagonals + numVertical + numHoriz;

            return numTotalSteps;
        }
    }

}