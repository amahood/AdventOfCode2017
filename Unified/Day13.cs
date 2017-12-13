using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day13
    {
        public static void TestDay13()
        {
            Console.WriteLine("-------------------DAY 13-------------------");
            
            StreamReader sr = new StreamReader("day13Input.txt");
            string separator = ": ";
            string line;
            Dictionary<int,int> firewallInput = new Dictionary<int,int>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] s1 = line.Split(separator);
                firewallInput.Add(Int32.Parse(s1[0]),Int32.Parse(s1[1]));
            }

            //Fill up new firewall including 0 spaces just for future ease
            int maxKey = firewallInput.Keys.Max();
            for (int i = 0; i<maxKey; i++)
            {
                bool keyExists = firewallInput.ContainsKey(i);
                if (!keyExists)
                {
                    KeyValuePair<int, int> kvp = new KeyValuePair<int,int>(i,0);
                    firewallInput.Add(kvp.Key, kvp.Value);
                }
            }

            //NExt easiest thing to do here is sort

        }

        
    }

}