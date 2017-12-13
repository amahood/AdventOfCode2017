using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day12
    {
        public static void TestDay12()
        {
            Console.WriteLine("-------------------DAY 12-------------------");
            
            StreamReader sr = new StreamReader("day12Input.txt");
            string separator = "<->";
            string line;
            Dictionary<int, int[]> programNetwork = new Dictionary<int, int[]>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] s1 = line.Split(separator);
                char delimiter = ',';
                string[] s2 = s1[1].Split(delimiter);
                int[] s2n = new int[s2.Count()];
                int s2nIndex = 0;
                foreach (string s in s2)
                {
                    s2n[s2nIndex] = Int32.Parse(s);
                    s2nIndex++;
                }
                programNetwork.Add(Int32.Parse(s1[0]),s2n);
            }

            int numPrograms = programNetwork.Count;

            //Need to make a copy of the network, otherwise, finding members of the group won't work
            Dictionary<int, int[]> networkCloneForGroupTracking = new Dictionary<int, int[]>();
            foreach (KeyValuePair<int, int[]> kvp in programNetwork)
            {
                networkCloneForGroupTracking.Add(kvp.Key, kvp.Value);
            }

            int numberOfGroups = 0;
            while (networkCloneForGroupTracking.Count > 0)
            {
                List<int> canTalkToSelectedProgram = CanTalkToProgram(networkCloneForGroupTracking);
                foreach (int groupMember in canTalkToSelectedProgram)
                {
                    networkCloneForGroupTracking.Remove(groupMember);
                }
                numberOfGroups++;
            }

            Console.WriteLine("Number of groups found - " + numberOfGroups);
        }

        public static List<int> CanTalkToProgram(Dictionary<int, int[]> network)
        {
            List<int> canTalkToPerson = new List<int>();
            int nextGroupAnchor = network.First().Key;
            canTalkToPerson.Add(nextGroupAnchor);

            int numPrograms = network.Count;

            for (int i = 1; i<numPrograms; i++)
            {
                for (int j = i; j<numPrograms; j++)
                {
                    foreach (int connectionNode in network.ElementAt(j).Value)
                    {
                        if (canTalkToPerson.Contains(connectionNode) && !(canTalkToPerson.Contains(j)))
                        {
                            canTalkToPerson.Add(j);
                        }
                    }       
                }
            }

            return canTalkToPerson;
        }
    }

}