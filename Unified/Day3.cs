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
        private List<GridPoint> gridPoints = new List<GridPoint>();

        public SpiralGrid()
        {
            currentX = 0;
            currentY = 0;
            currentStep = 1;
            GridPoint origin = new GridPoint(0,0);
            gridPoints.Add(origin);
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

            if (targetEnd == 1) return finalLocation;
            nextMove = "Right";
            
            while (currentStep < targetEnd)
            {
                Console.WriteLine("Current Step - " + currentStep + " Current X - " + currentX + " Current Y - " + currentY);
                currentStep++;
                if (nextMove=="Right")
                {
                    currentX++;
                    GridPoint newRight = new GridPoint(currentX, currentY);
                    gridPoints.Add(newRight);
                    
                    //Check if up already traversed if yes move right, if no, move up
                    if (gridPoints.Contains(new GridPoint(currentX, currentY+1)))
                    {
                        nextMove = "Right";
                    }
                    else
                    {
                        nextMove = "Up";
                    }
                }
                else if (nextMove=="Up")
                {
                    currentY++;
                    GridPoint newUp = new GridPoint(currentX, currentY);
                    gridPoints.Add(newUp);

                    //Check if left already traversed, if yes move up, if no, move left
                    if (gridPoints.Contains(new GridPoint(currentX-1, currentY)))
                    {
                        nextMove = "Up";
                    }
                    else
                    {
                        nextMove = "Left";
                    }
                }
                else if (nextMove=="Left")
                {
                    currentX--;
                    GridPoint newLeft = new GridPoint(currentX, currentY);
                    gridPoints.Add(newLeft);

                    //Check if down already traversed, if yes move left, if no, move down
                    if (gridPoints.Contains(new GridPoint(currentX, currentY-1)))
                    {
                        nextMove = "Left";
                    }
                    else
                    {
                        nextMove = "Down";
                    }
                }
                else if (nextMove=="Down")
                {
                    currentY--;
                    GridPoint newDown = new GridPoint(currentX, currentY);
                    gridPoints.Add(newDown);
                    
                    //Check if right already traversed, if yes move down, if no, move right
                    if (gridPoints.Contains(new GridPoint(currentX+1, currentY)))
                    {
                        nextMove = "Down";
                    }
                    else
                    {
                        nextMove = "Right";
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
