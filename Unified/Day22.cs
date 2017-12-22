using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day22
    {
        public static void TestDay22()
        {
            Console.WriteLine("-------------------DAY 22-------------------");

            StreamReader sr = new StreamReader("day22Input.txt");
            string line;
            List<string> inputInfectionMap = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
               inputInfectionMap.Add(line);
            }

            //Create all reuqired tracking variables            
            List<Tuple<int,int>> infectedLocations = new List<Tuple<int, int>>();

            //Process initial map to create grid
            int initialGridWidth = inputInfectionMap.First().Length;
            int intialGridHeight = inputInfectionMap.Count;
            for (int h = 0; h<intialGridHeight;h++)
            {
                for (int w = 0; w<initialGridWidth; w++)
                {
                    if (inputInfectionMap[h][w]=='#')
                    {
                        Tuple<int,int> infectedLocation = new Tuple<int,int>(h,w);
                        infectedLocations.Add(infectedLocation);
                    }
                }
            }

            //Assume odd input
            int currentCol = initialGridWidth/2;
            int currentRow = intialGridHeight/2;
            string currentDirection = "Up";
            int numSquaresCarrierHasInfected = 0;

            for (int burstNum = 0; burstNum<10000; burstNum++)
            {
                //Check node for dirty/clean state
                bool currentBurstNodeDirty = false;
                foreach (Tuple<int,int> t in infectedLocations)
                {
                    if (t.Item1==currentRow && t.Item2==currentCol)
                    {
                        currentBurstNodeDirty = true;
                    }
                }

                
                //Change direction
                if (currentBurstNodeDirty)
                {
                    //Turn right 
                    switch (currentDirection)
                    {
                        case "Up":
                            currentDirection="Right";
                            break;
                        case "Left":
                            currentDirection="Up";
                            break;
                        case "Down":
                            currentDirection="Left";
                            break;
                        case "Right":
                            currentDirection="Down";
                            break;
                    }

                    //Clean node
                    infectedLocations.Remove(new Tuple<int, int>(currentRow,currentCol));
                }
                else if (!currentBurstNodeDirty)
                {
                    switch (currentDirection)
                    {
                        case "Up":
                            currentDirection="Left";
                            break;
                        case "Left":
                            currentDirection="Down";
                            break;
                        case "Down":
                            currentDirection="Right";
                            break;
                        case "Right":
                            currentDirection="Up";
                            break;
                    }

                    //Infect Node
                    infectedLocations.Add(new Tuple<int,int>(currentRow,currentCol));
                    numSquaresCarrierHasInfected++;
                }

                //Move forward
                switch (currentDirection)
                {
                    case "Up":
                        currentRow--;
                        break;
                    case "Down":
                        currentRow++;
                        break;
                    case "Left":
                        currentCol--;
                        break;
                    case "Right":
                        currentCol++;
                        break;
                }
                    
            }

            Console.WriteLine("Number of bursts causing infection = "+numSquaresCarrierHasInfected);

        }
    }
}




