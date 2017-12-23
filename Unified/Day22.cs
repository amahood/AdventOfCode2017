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
            List<Node> nodesOnMap = new List<Node>();

            //Process initial map to create grid
            int initialGridWidth = inputInfectionMap.First().Length;
            int intialGridHeight = inputInfectionMap.Count;
            for (int h = 0; h<intialGridHeight;h++)
            {
                for (int w = 0; w<initialGridWidth; w++)
                {
                    Node currentNode = new Node(h,w,inputInfectionMap[h][w]);
                    nodesOnMap.Add(currentNode);
                }
            }

            //Assume odd input
            int currentCol = initialGridWidth/2;
            int currentRow = intialGridHeight/2;
            string currentDirection = "Up";
            int numSquaresCarrierHasInfected = 0;

            for (int burstNum = 0; burstNum<10000000; burstNum++)
            {
                char currentHealthState=' ';
                int matchingNodeIndex = 0;
                
                var queryNodes = from node in nodesOnMap
                           where node.row == currentRow && node.col == currentCol
                           select node;
                List<Node> queyrNodesList = queryNodes.ToList<Node>();

                if (queyrNodesList.Count()==1)
                {
                    matchingNodeIndex = nodesOnMap.FindIndex(n => n.col == currentCol && n.row == currentRow);
                    currentHealthState = nodesOnMap[matchingNodeIndex].health;
                    
                }
                else if (queryNodes.Count()==0)
                {
                    nodesOnMap.Add(new Node(currentRow,currentCol,'.'));
                    currentHealthState = '.';
                    matchingNodeIndex = nodesOnMap.Count-1;
                }
                else
                {
                    Console.WriteLine("Should never get here!");
                }
               
                //Change direction and change current health state
                switch (currentHealthState)
                {
                    case ('.'):
                        nodesOnMap[matchingNodeIndex].health = 'W';
                        switch (currentDirection)
                        {
                            case "Up":
                                currentDirection = "Left";
                                break;
                            case "Down":
                                currentDirection = "Right";
                                break;
                            case "Left":
                                currentDirection = "Down";
                                break;
                            case "Right":
                                currentDirection = "Up";
                                break;
                        }
                        break;
                    case ('#'):
                        nodesOnMap[matchingNodeIndex].health = 'F';
                        switch (currentDirection)
                        {
                            case "Up":
                                currentDirection = "Right";
                                break;
                            case "Down":
                                currentDirection = "Left";
                                break;
                            case "Left":
                                currentDirection = "Up";
                                break;
                            case "Right":
                                currentDirection = "Down";
                                break;
                        }
                        break;
                    case ('W'):
                        nodesOnMap[matchingNodeIndex].health = '#';
                        numSquaresCarrierHasInfected++;
                        break;
                    case ('F'):
                        nodesOnMap[matchingNodeIndex].health = '.';
                        switch (currentDirection)
                        {
                            case "Up":
                                currentDirection = "Down";
                                break;
                            case "Down":
                                currentDirection = "Up";
                                break;
                            case "Left":
                                currentDirection = "Right";
                                break;
                            case "Right":
                                currentDirection = "Left";
                                break;
                        }
                        break;
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
                    case "Right":
                        currentCol++;
                        break;
                    case "Left":
                        currentCol--;
                        break;
                }    
               
            }

            Console.WriteLine("Number of bursts causing infection = "+numSquaresCarrierHasInfected);

        }

/*
        public static void PrintBurstGrid(List<Node> grid)
        {
            int maxX = 0;
            int maxY = 0;
            foreach (Node n in grid)
            {
                if (Math.Abs(n.row)>maxY)
                {
                    maxY = Math.Abs(n.row);
                }
                if (Math.Abs(n.col)>maxX)
                {
                    maxX = Math.Abs(n.col);
                }
            }

            grid = grid.OrderBy(n => n.row).ThenBy(n => n.col).ToList();

            int currentRow = grid[0].row;
            int currentCol = grid[0].col;
            StringBuilder sb = new StringBuilder();
            foreach (Node n in grid)
            {
                if (n.row==currentRow)
                {
                    sb.Append(n.health);
                    currentCol++;
                }
                else
                {
                    Console.WriteLine(sb.ToString());
                    sb = new StringBuilder();
                    currentCol = 0;
                    currentRow = n.row;
                    for (int c = 0;c<(Math.Abs(n.col-currentCol));c++)
                    {
                        sb.Append('.');
                    }
                    sb.Append(n.health);
                }
            }

        }
        */
    }

    public class Node
    {
        public Node(int r, int c, char h)
        {
            row = r;
            col = c;
            health = h;
        }

        public int row;
        public int col;
        public char health;
    }
}




