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

            //New alternative mechanism here - prefetch and eliminate - find first number that isn't a multiple of any + it's depth
            int newDelayTracker = 0;
            bool foundCandidate = false;

            while (!foundCandidate)
            {
                //Console.WriteLine("Testing delay - " + newDelayTracker);
                bool foundIntermediateProblem = false;
                foreach (FirewallLayer f in builtFirewall)
                {
                    //Bail if we already  found a problem and on delay 0, first pass
                    if (newDelayTracker==0){foundIntermediateProblem = true;}
                    if (!foundIntermediateProblem)
                    {
                        if ( (newDelayTracker+f.depthLayer) >= (2*(f.range-1)) && f.range!=0 )
                        {
                            int layerModuloResult = (newDelayTracker+f.depthLayer) % (2*(f.range-1));
                            if (layerModuloResult==0)
                            {
                                foundIntermediateProblem = true;
                            }
                        }
                    }
                }
                if (foundIntermediateProblem==false)
                {
                    foundCandidate = true;
                }

                newDelayTracker++;
            }


            

            Console.WriteLine("Delay yielding free crossing - " + (newDelayTracker-1));
        }

        public static int TravelFroggerFirewall(List<FirewallLayer> inputFirewall)
        {
            int currentFroggerLane = 0;
            int runningSeverity = 0;
            bool isFirstStep = true;
            bool hitOnFirstStep = false;

            while (currentFroggerLane<(inputFirewall.Count-1))
            {
                if (isFirstStep)
                {
                    if (inputFirewall[0].currentScannerPosition==0)
                    {
                        runningSeverity += inputFirewall[currentFroggerLane].depthLayer*inputFirewall[currentFroggerLane].range;
                        inputFirewall[currentFroggerLane].isFroggerInLane = true;
                        hitOnFirstStep = true;
                    }
                }
                
                //Increment frogger lane
                if (!isFirstStep)
                {
                    currentFroggerLane++;
                    
                    //Check to see if we are fucked by scanner
                    if (inputFirewall[currentFroggerLane].currentScannerPosition==0)
                    {
                        inputFirewall[currentFroggerLane].isFroggerInLane = true;
                        runningSeverity += inputFirewall[currentFroggerLane].depthLayer* inputFirewall[currentFroggerLane].range;
                    }
                }

                //Cycle scanner
                foreach (FirewallLayer fl in inputFirewall)
                {
                    fl.CycleScanner();
                }
                if (isFirstStep)
                {
                    isFirstStep = false;
                }
            }

            //Artifically add one if we get hit on stepping in, as, that will register as 0
            if (hitOnFirstStep && runningSeverity==0)
            {
                runningSeverity++;
            }
            return runningSeverity;
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