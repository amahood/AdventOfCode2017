using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day15
    {
        public static void TestDay15()
        {
            Console.WriteLine("-------------------DAY 15-------------------");

            int numberComparisons = 5000000; //eventually rpelace this with 40million

            int generatorASeed = 618; //Replace this with my puzzle input
            int generatorBSeed = 814; //Replace this with my puzzle input
            int generatorAFactor = 16807;
            int generatorBFactor = 48271;

            int moduloBase = 2147483647;

            //Generate first result
            long generatorAPreviousValue = (generatorASeed*generatorAFactor)%moduloBase;
            long generatorBPreivousValue = (generatorBSeed*generatorBFactor)%moduloBase;

            int runningComparisonSuccesses = 0;

            //Find all A values
            List<long> genANewValues = new List<long>();
            long loopCounterA = 0;
            while (genANewValues.Count<numberComparisons)
            {
                if (loopCounterA!=0)
                {
                    generatorAPreviousValue = (generatorAPreviousValue*generatorAFactor)%moduloBase;
                }
                if (generatorAPreviousValue%4==0)
                {
                    genANewValues.Add(generatorAPreviousValue);
                }        
                loopCounterA++;
            }

            //Find all B values
            List<long> genBNewValues = new List<long>();
            long loopCounterB = 0;
            while (genBNewValues.Count<numberComparisons)
            {
                if (loopCounterB!=0)
                {
                    generatorBPreivousValue = (generatorBPreivousValue*generatorBFactor)%moduloBase;
                }
                if (generatorBPreivousValue%8==0)
                {
                    genBNewValues.Add(generatorBPreivousValue);
                }
                loopCounterB++;
            }

            //Determine how many to compare (fewest in list)
            int compareLimit = Math.Min(genANewValues.Count, genBNewValues.Count);

            for (int compareRound = 0; compareRound<compareLimit; compareRound++)
            {
                
                long genA16LSB = (genANewValues[compareRound] & 0xFFFF);
                long genB16LSB = (genBNewValues[compareRound] & 0xFFFF);

                if (genA16LSB==genB16LSB)
                {
                    runningComparisonSuccesses++;
                }
            }

            Console.WriteLine("Number of comparison success = "+runningComparisonSuccesses);

        }

        
    }

}
