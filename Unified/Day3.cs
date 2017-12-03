using System;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Day3
    {
        public static void TestDay3(int targetStoppingLocation)
        {
            Console.WriteLine("-------------------DAY 3-------------------");

            SpiralGrid grid = new SpiralGrid();
            GridPoint finalLocation = grid.traverseGrid(targetStoppingLocation);

            Console.WriteLine("Final X Location = " + finalLocation.xLocation);
            Console.WriteLine("Final Y Location = " + finalLocation.yLocation);

            int numTotalSteps = Math.Abs(finalLocation.xLocation) + Math.Abs(finalLocation.yLocation);
            Console.WriteLine("Total Steps To Memory Extraction = " + numTotalSteps);
        }

    }

    public class SpiralGrid
    {
        public SpiralGrid()
        {
            currentX = 0;
            currentY = 0;
            currentStep = 1;
        }

        public int targetEnd;

        public int currentStep;

        public int currentX;

        public int currentY;

        public GridPoint traverseGrid(int targetStop)
        {
            targetEnd = targetStop;
            GridPoint finalLocation = new GridPoint(currentX, currentY);
            string nextMove = "";

            int maxXPos = 0;
            int maxYPos = 0;
            int maxXNeg = 0;
            int maxYNeg = 0;
            

            if (targetEnd == 1) return finalLocation;
            nextMove = "Right";
            
            while (currentStep < targetEnd)
            {
                currentStep++;
                if (nextMove=="Right")
                {
                    currentX++;
                    if (currentX>maxXPos)
                    {
                        maxXPos = currentX;
                        nextMove = "Up";
                    }
                    else
                    {
                        nextMove = "Right";
                    }
                }
                else if (nextMove=="Up")
                {
                    currentY++;
                    if (currentY>maxYPos)
                    {
                        maxYPos = currentY;
                        nextMove = "Left";
                    }
                    else
                    {
                        nextMove = "Up";
                    }
                }
                else if (nextMove=="Left")
                {
                    currentX--;
                    if (currentX<maxXNeg)
                    {
                        maxXNeg = currentX;
                        nextMove = "Down";
                    }
                    else
                    {
                        nextMove = "Left";
                    }
                }
                else if (nextMove=="Down")
                {
                    currentY--;
                    if (currentY<maxYNeg)
                    {
                        maxYNeg = currentY;
                        nextMove = "Right";
                    }
                    else
                    {
                        nextMove = "Down";
                    }
                }
            }
                
            finalLocation.xLocation = currentX;
            finalLocation.yLocation = currentY;

            return finalLocation;
        }

    }

    public struct GridPoint
    {
        public int xLocation;
        public int yLocation;

        public GridPoint(int x, int y)
        {
            xLocation = x;
            yLocation = y;
        }
    } 
}
