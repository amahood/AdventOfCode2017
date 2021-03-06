using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day20
    {
        public static void TestDay20()
        {
            Console.WriteLine("-------------------DAY 20-------------------");
            
            List<Particle> particles = new List<Particle>();

            StreamReader sr = new StreamReader("day20Input.txt");
            string line;
            char delimiter = ',';
            while ((line = sr.ReadLine()) != null)
            {
                string positionString = line.Substring(line.IndexOf('p'),line.IndexOf('v'));
                positionString = positionString.Substring(positionString.IndexOf('<')+1, positionString.IndexOf('>')-positionString.IndexOf('<')-1);
                string[] posHolder = positionString.Split(delimiter);

                string velocityString = line.Substring(line.IndexOf('v'),line.IndexOf('a')-line.IndexOf('v'));
                velocityString = velocityString.Substring(velocityString.IndexOf('<')+1,velocityString.IndexOf('>')-velocityString.IndexOf('<')-1);
                string[] velHolder = velocityString.Split(delimiter);

                string accelString = line.Substring(line.IndexOf('a'),line.Count()-line.IndexOf('a'));
                accelString = accelString.Substring(accelString.IndexOf('<')+1,accelString.IndexOf('>')-accelString.IndexOf('<')-1);
                string[] accelHolder = accelString.Split(delimiter);

                Particle tempParticle = new Particle();
                tempParticle.xPos = Int64.Parse(posHolder[0]);
                tempParticle.yPos = Int64.Parse(posHolder[1]);
                tempParticle.zPos = Int64.Parse(posHolder[2]);
                tempParticle.xVel = Int64.Parse(velHolder[0]);
                tempParticle.yVel = Int64.Parse(velHolder[1]);
                tempParticle.zVel = Int64.Parse(velHolder[2]);
                tempParticle.xAcc = Int32.Parse(accelHolder[0]);
                tempParticle.yAcc = Int32.Parse(accelHolder[1]);
                tempParticle.zAcc = Int32.Parse(accelHolder[2]);

                particles.Add(tempParticle);
            }
            
            //Part 1 goop
            {
                List<long> accelList = new List<long>();
                foreach (Particle p in particles)
                {
                    long accelValue = Math.Abs(p.xAcc)+ Math.Abs(p.yAcc) + Math.Abs (p.zAcc);
                    accelList.Add(accelValue);                
                }
                long minAccelValue = accelList.Min();
                
                List<int> indexOfLowest = new List<int>();
                int indexTracker = 0;
                foreach (Particle p in particles)
                {
                    long accelValue = Math.Abs(p.xAcc)+ Math.Abs(p.yAcc) + Math.Abs (p.zAcc);
                    if (accelValue== minAccelValue)
                    {
                        indexOfLowest.Add(indexTracker);
                    }
                    indexTracker++;
                }

                long lowestManhattan = 0;
                indexTracker = 0;
                int indexOfClosest = 0;
                foreach (int i in indexOfLowest)
                {
                    long manhattan = Math.Abs(particles[i].xPos) + Math.Abs(particles[i].yPos) + Math.Abs(particles[i].zPos);
                    if (indexTracker==0)
                    {
                        lowestManhattan = manhattan;
                    }
                    else
                    {
                        if (manhattan<lowestManhattan)
                        {
                            lowestManhattan = manhattan;
                            indexOfClosest = i;
                        }
                    }
                    indexTracker++;
                }
                Console.WriteLine("Closest at the limit - " + indexOfClosest);
            }   

            //Check for collisions at initial location - This is error prone and isn't consistent with how I do it in main loop, but gives me answer so is fine
            bool foundOneMatchForCurrentParticle = false;
            for (int j = 0;j<particles.Count;j++)
            {
                for (int i = 0; i<particles.Count; i++)
                {
                    if ( (particles[j].xPos==particles[i].xPos && particles[j].yPos==particles[i].yPos && particles[j].zPos==particles[i].zPos) && i!=j )
                    {
                        foundOneMatchForCurrentParticle = true;
                        particles.Remove(particles[i]);
                    }
                }
                if (foundOneMatchForCurrentParticle)
                {
                    particles.Remove(particles[j]);
                }
            }

            int collisionContinue = 0;
            while (collisionContinue<1000)
            {
                //Update position
                foreach (Particle p in particles)
                {
                    p.UpdateParticle();
                }
                //Check for collisions
                foundOneMatchForCurrentParticle = false;
                for (int j = 0;j<particles.Count;j++)
                {
                    for (int i = 0; i<particles.Count; i++)
                    {
                        if ( (particles[j].xPos==particles[i].xPos && particles[j].yPos==particles[i].yPos && particles[j].zPos==particles[i].zPos) && i!=j )
                        {
                            particles[j].flagToRemove = true;//I know this is redundant and will get set everytime, but whatever
                            particles[i].flagToRemove = true;
                        }
                    }
                }

                for (int r = 0;r<particles.Count;r++)
                {
                    if (particles[r].flagToRemove)
                    {
                        particles.Remove(particles[r]);
                        Console.WriteLine("Remove particle"); 
                    }
                }

                collisionContinue++;
                Console.WriteLine("Number of particles remaining - "+particles.Count);
            }
            Console.WriteLine("No more collisions, "+particles.Count + " particles left!");

        }

    }

    public class Particle
    {
        public Particle()
        {
            xPos = 0;
            yPos = 0;
            zPos = 0;
            xAcc = 0;
            yAcc = 0;
            zAcc = 0;
            xVel = 0;
            yVel = 0;
            zVel = 0;
            manhattanDistance = 0;
            flagToRemove = false;
        }


       public void UpdateParticle()
        {
            
            this.xVel += this.xAcc;
            this.yVel +=this.yAcc;
            this.zVel += this.zAcc;

            this.xPos += this.xVel;
            this.yPos += this.yVel;
            this.zPos += this.zVel;

            this.manhattanDistance = Math.Abs(this.xPos) + Math.Abs(this.yPos) + Math.Abs(this.zPos);
        }

        public long xPos;
        public long yPos;
        public long zPos;
        public long xVel;
        public long yVel;
        public long zVel;
        public int xAcc;
        public int yAcc;
        public int zAcc;
        public long manhattanDistance;
        public bool flagToRemove;
    
    }
}




