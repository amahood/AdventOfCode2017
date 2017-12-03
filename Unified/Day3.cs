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
            Console.WriteLine("Solving Part 1 - Finding Manhattan Distance of number!");
            SpiralGrid grid = new SpiralGrid();
            GridPoint finalLocation = grid.findManhattanDistance(targetStoppingLocation);
            Console.WriteLine("Final X Location = " + finalLocation.xLocation);
            Console.WriteLine("Final Y Location = " + finalLocation.yLocation);
            int numTotalSteps = Math.Abs(finalLocation.xLocation) + Math.Abs(finalLocation.yLocation);
            Console.WriteLine("Total Steps To Memory Extraction = " + numTotalSteps);
            Console.WriteLine();

            Console.WriteLine("Solving Part 2 - Finding sum of all adjacent squares!");
            GridPoint finalLocation2 = grid.findSumSquare(targetStoppingLocation);
            Console.WriteLine("Final X Location = " + finalLocation2.xLocation);
            Console.WriteLine("Final Y Location = " + finalLocation2.yLocation);
            Console.WriteLine("Final Sum = " + finalLocation2.part2Value);
        }

    }

    public class SpiralGrid
    {
        public SpiralGrid()
        {
            currentX = 0;
            currentY = 0;
            currentStep = 1;
            currentSumSquare = 1;
            grid = new List<GridPoint>();
        }

        private List<GridPoint> grid;

        public int targetEnd;

        public int currentStep;

        public int currentX;

        public int currentY;

        public int currentSumSquare;

        public GridPoint findManhattanDistance(int targetStop)
        {
            currentX = 0;
            currentY = 0;
            currentStep = 1;
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

        private int findAdjacentSumValue(GridPoint currentPoint)
        {
            int runningSum = 0;
            foreach (GridPoint gp in grid)
            {
                if (gp.xLocation == currentPoint.xLocation+1 && gp.yLocation == currentPoint.yLocation){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation+1 && gp.yLocation == currentPoint.yLocation+1){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation && gp.yLocation == currentPoint.yLocation+1){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation-1 && gp.yLocation == currentPoint.yLocation+1){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation-1 && gp.yLocation == currentPoint.yLocation){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation-1 && gp.yLocation == currentPoint.yLocation-1){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation && gp.yLocation == currentPoint.yLocation-1){runningSum+=gp.part2Value;}
                if (gp.xLocation == currentPoint.xLocation+1 && gp.yLocation == currentPoint.yLocation-1){runningSum+=gp.part2Value;}
            }
            return runningSum;
        }

        public GridPoint findSumSquare(int targetStop)
        {
            currentX = 0;
            currentY = 0;
            currentStep = 1;
            targetEnd = targetStop;
            GridPoint finalLocation = new GridPoint(currentX, currentY, currentStep);
            string nextMove = "";

            int maxXPos = 0;
            int maxYPos = 0;
            int maxXNeg = 0;
            int maxYNeg = 0;
            

            if (targetEnd == 1) 
            return finalLocation;

            GridPoint origin = new GridPoint(0,0,1);
            grid.Add(origin);
            nextMove = "Right";
            
            while (currentSumSquare <= targetEnd)
            {
                currentStep++;
                
                if (nextMove=="Right")
                {
                    currentX++;
                    GridPoint newRight = new GridPoint(currentX, currentY);
                    newRight.part2Value = findAdjacentSumValue(newRight);
                    currentSumSquare = newRight.part2Value;
                    grid.Add(newRight);

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
                    GridPoint newUp = new GridPoint(currentX, currentY);
                    newUp.part2Value = findAdjacentSumValue(newUp);
                    currentSumSquare = newUp.part2Value;
                    grid.Add(newUp);
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
                    GridPoint newLeft = new GridPoint(currentX, currentY);
                    newLeft.part2Value = findAdjacentSumValue(newLeft);
                    currentSumSquare = newLeft.part2Value;
                    grid.Add(newLeft);
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
                    GridPoint newDown = new GridPoint(currentX, currentY);
                    newDown.part2Value = findAdjacentSumValue(newDown);
                    currentSumSquare = newDown.part2Value;
                    grid.Add(newDown);
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
            finalLocation.part2Value = currentSumSquare;

            return finalLocation;
        }
    }

    public struct GridPoint
    {
        public int xLocation;
        public int yLocation;
        public int part2Value;

        public GridPoint(int x, int y)
        {
            xLocation = x;
            yLocation = y;
            part2Value = 0;
        }

        public GridPoint(int x, int y, int sumValue)
        {
            xLocation = x;
            yLocation = y;
            part2Value = sumValue;
        }
    } 
}
