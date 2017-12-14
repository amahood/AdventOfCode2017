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

            //Next easiest thing to do here is sort
            Dictionary<int,int> firewallInputSorted = firewallInput.OrderBy(x => x.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

            //Create List of FirewallLayers that is new class structure firewall
            List<FirewallLayer> builtFirewall = new List<FirewallLayer>();
            foreach (KeyValuePair<int,int> kvp in firewallInputSorted)
            {
                FirewallLayer nextLayer = new FirewallLayer(kvp.Key, kvp.Value);
                builtFirewall.Add(nextLayer);
            }

            int currentFroggerLane = 0;
            int runningSeverity = 0;
            bool isFirstStep = true;

            //Add severity of row 0, as we know we will hit this
            runningSeverity += builtFirewall[currentFroggerLane].depthLayer* builtFirewall[currentFroggerLane].range;
            builtFirewall[currentFroggerLane].isFroggerInLane = true;

            while (currentFroggerLane<(builtFirewall.Count-1))
            {
                //Increment frogger lane
                if (!isFirstStep)
                {
                    currentFroggerLane++;
                    
                    //Check to see if we are fucked by scanner
                    if (builtFirewall[currentFroggerLane].currentScannerPosition==0)
                    {
                        builtFirewall[currentFroggerLane].isFroggerInLane = true;
                        runningSeverity += builtFirewall[currentFroggerLane].depthLayer* builtFirewall[currentFroggerLane].range;
                    }
                }

                //cycle scanner
                foreach (FirewallLayer fl in builtFirewall)
                {
                    fl.CycleScanner();
                }
                if (isFirstStep)
                {
                    isFirstStep = false;
                }
            }
            Console.WriteLine("Total severity of frogger firewall crossing - " + runningSeverity);
        }
    }

    public class FirewallLayer
    {
        public FirewallLayer(int depth, int incomingRange)
        {
            depthLayer = depth;
            range = incomingRange;
            currentScannerPosition = 0;
            isFroggerInLane = false;
            isScannerAscending = false;
        }

        public int depthLayer;
        public int range;
        public int currentScannerPosition;
        public bool isFroggerInLane;
        public bool isScannerAscending;

        //Add function to compute next position, taking into account rollover
        public void CycleScanner()
        {
            if (range>2)
            {
                if (currentScannerPosition==(range-1))
                {
                    currentScannerPosition = range-2;
                    isScannerAscending = true;
                }
                else if (currentScannerPosition==0)
                {
                    currentScannerPosition = 1;
                    isScannerAscending = false;
                }
                else if (isScannerAscending)
                {
                    currentScannerPosition-=1;
                }
                else 
                {
                    currentScannerPosition+=1;
                }
            }
            else if (range==2)
            {
                if (currentScannerPosition==0)
                {
                    currentScannerPosition=1;
                }
                else if (currentScannerPosition==1)
                {
                    currentScannerPosition=0;
                }
            }
        }
    }

}