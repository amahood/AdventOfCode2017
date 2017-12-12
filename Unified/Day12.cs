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
            List<int> canTalkToZero = new List<int>();
            canTalkToZero.Add(0);

            for (int i = 1; i<numPrograms; i++)
            {
                for (int j = i; j<numPrograms; j++)
                {
                    KeyValuePair<int, int[]> programNode = new KeyValuePair<int, int[]>(j,programNetwork[j]);
                    foreach (int connectionNode in programNode.Value)
                    {
                        if (canTalkToZero.Contains(connectionNode) && !canTalkToZero.Contains(programNode.Key))
                        {
                            canTalkToZero.Add(programNode.Key);
                        }
                    }       
                }
            }

            Console.WriteLine(canTalkToZero.Count + " programs can talk to 0!");

        }
    }

}