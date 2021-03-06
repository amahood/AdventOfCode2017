using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day24
    {
        public static void TestDay24()
        {
            Console.WriteLine("-------------------DAY 24-------------------");

            //Parse input and build up collection of all possible pipes
            StreamReader sr = new StreamReader("day24Input.txt");
            string line;
            char delimiter = '/';
            List<Tuple<int,int>> availablePorts = new List<Tuple<int, int>>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] substrings = line.Split(delimiter);
                availablePorts.Add(new Tuple<int, int>(Int32.Parse(substrings[0]),Int32.Parse(substrings[1])));
            }

            //Search list and find set of pipes that we can start with
            List<Tuple<int,int>> starting0Ports= new List<Tuple<int, int>>();
            var start = availablePorts.Where(t => (t.Item1==0 || t.Item2==0));
            starting0Ports = start.ToList<Tuple<int,int>>();

            //Get starting ports in right order, with 0 on left (likely not necessary, just doing for good hygiene)
            for (int i=0;i<starting0Ports.Count;i++)
            {
                if ( (starting0Ports[i].Item1==0 && starting0Ports[i].Item2==0) || starting0Ports[i].Item1==0)
                {
                    //do nothing
                }
                else if (starting0Ports[i].Item2==0)
                {
                    starting0Ports[i] = SwapTuple(starting0Ports[i]);
                }
            }

            List<List<Tuple<int,int>>> possibleBridges = new List<List<Tuple<int, int>>>();

            foreach (Tuple<int,int> t in starting0Ports)
            {
                List<Tuple<int,int>> tempBridgeOnlyStarting = new List<Tuple<int, int>>();
                tempBridgeOnlyStarting.Add(t);
                foreach (List<Tuple<int,int>> br in BuildPossibleBridges(tempBridgeOnlyStarting,availablePorts))
                {
                    possibleBridges.Add(CopyBridge(br));
                }
            }

            Console.WriteLine("Number of bridges found - "+possibleBridges.Count);

            //Score bridges
            int bestBridge = 0;
            foreach (List<Tuple<int,int>> bridge in possibleBridges)
            {
                int bridgeScore = ScoreBridge(bridge);
                if (bridgeScore>bestBridge)
                {
                    bestBridge = bridgeScore;
                }
            }

            Console.WriteLine("Strongest bridge score of possible bridges = "+bestBridge);

            //Part 2 - Find lingest bridge
            int longestBridge = 0;
            List<List<Tuple<int,int>>> longestPossibleBridges = new List<List<Tuple<int, int>>>();
            foreach (List<Tuple<int,int>> bridge in possibleBridges)
            {
                int bridgeLength = bridge.Count;
                if (bridgeLength>=longestBridge)
                {
                    longestBridge = bridgeLength;
                    
                }
            }
            Console.WriteLine("Length of longest bridge = "+longestBridge);

//COnvert to linq for fasterness
            foreach (List<Tuple<int,int>> bridge in possibleBridges)
            {
                if (bridge.Count==longestBridge)
                {
                    longestPossibleBridges.Add(CopyBridge(bridge));
                }
            }

            int scoreOfLongestBridge = 0;
            if (longestPossibleBridges.Count>1)
            {
                int bestBridgeLong = 0;
                foreach (List<Tuple<int,int>> bridge in longestPossibleBridges)
                {
                    int bridgeScore = ScoreBridge(bridge);
                    if (bridgeScore>bestBridgeLong)
                    {
                        bestBridgeLong = bridgeScore;
                    }
                }
                scoreOfLongestBridge = bestBridgeLong;
            }
            else
            {
                scoreOfLongestBridge = ScoreBridge(longestPossibleBridges.First());
            }

            Console.WriteLine("Score of longest bridge = " + scoreOfLongestBridge);

        }

        public static List<List<Tuple<int,int>>> BuildPossibleBridges(List<Tuple<int,int>> bridge, List<Tuple<int,int>> availableSegments)
        {
            //Assume they are ordered in linkage order, will need to make this happen (build a swap function)
            List<List<Tuple<int,int>>> possibleBridges = new List<List<Tuple<int, int>>>();
            
            //Always add hte first one that comes in, because it is a valid option (i.e. just the starting pairs)
            possibleBridges.Add(CopyBridge(bridge)); 

            //Remove last used segment from pool of available segmenets
            Tuple<int,int> segmentToRemove = bridge.Last();
            if (availableSegments.Where(t => (t.Item1==segmentToRemove.Item1 && t.Item2==segmentToRemove.Item2) || (t.Item1==segmentToRemove.Item2 && t.Item2==segmentToRemove.Item1)).Count()!=0)
            {
                availableSegments.Remove(availableSegments.Where(t => (t.Item1==segmentToRemove.Item1 && t.Item2==segmentToRemove.Item2) || (t.Item1==segmentToRemove.Item2 && t.Item2==segmentToRemove.Item1)).First());
            }
            

            //FInd pairs that are next candidates to add to end and order properly
            int numbertoMatch = bridge.Last().Item2;
            var matches = availableSegments.Where(t => (t.Item1==numbertoMatch || t.Item2==numbertoMatch));

            if (matches.Count()!=0)
            {
                List<Tuple<int,int>> matchesList = matches.ToList<Tuple<int,int>>();
                for (int i=0;i<matchesList.Count;i++)
                {
                    if ( (matchesList[i].Item1==numbertoMatch && matchesList[i].Item2==numbertoMatch) || matchesList[i].Item1==numbertoMatch)
                    {
                        //do nothing
                    }
                    else if (matchesList[i].Item2==numbertoMatch)
                    {
                        matchesList[i] = SwapTuple(matchesList[i]);
                    }
                }

                foreach (Tuple<int,int> t in matchesList)
                {
                    List<Tuple<int,int>> copy = CopyBridge(bridge);
                    copy.Add(t);
                    List<Tuple<int,int>> avialableSegmentCopy = CopyBridge(availableSegments);
                    List<List<Tuple<int,int>>> newBridges = BuildPossibleBridges(copy, avialableSegmentCopy);
                    foreach (List<Tuple<int,int>> br in newBridges)
                    {
                        possibleBridges.Add(br);
                    }
                }
            }
    
            //Should only get to this base case if there are no more possible matches - at this point we've already added the incoming starting, so we shoul dbe good
            return possibleBridges;
        }
        
        public static List<Tuple<int,int>> CopyBridge(List<Tuple<int,int>> inBridge)
        {
            List<Tuple<int,int>> bridgeCopy = new List<Tuple<int, int>>();
            foreach (Tuple<int,int> t in inBridge)
            {
                bridgeCopy.Add(t);
            }
            return bridgeCopy;
        }

        public static int ScoreBridge(List<Tuple<int,int>> bridge)
        {
            int bridgeScore = 0;
            foreach (Tuple<int,int> t in bridge)
            {
                bridgeScore = bridgeScore + t.Item1 + t.Item2;
            }

            return bridgeScore;
        }

        public static Tuple<int,int> SwapTuple(Tuple<int,int> b)
        {
            return new Tuple<int,int>(b.Item2,b.Item1);
        }

    }


}




