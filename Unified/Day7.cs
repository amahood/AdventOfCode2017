using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day7
    {
        public static void TestDay7()
        {
            Console.WriteLine("-------------------DAY 7-------------------");

            List<TreeNode> tree = new List<TreeNode>();

            StreamReader sr = new StreamReader("day7Input.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                TreeNode newNode = CreateNodeFromString(line);
                tree.Add(newNode);
            }

            Console.WriteLine("Number of tree elements - " + tree.Count);
            List<TreeNode> trimmedTree = TrimTree(tree);
            Console.WriteLine("Number of trimmed tree elements - " + trimmedTree.Count);
            int indexOfBase = FindBase(trimmedTree);
            Console.WriteLine("Tree trunk - " + trimmedTree[indexOfBase].nodeRoot);

            int indexInMainTree = FindNodeFromRootString(tree, trimmedTree[indexOfBase].nodeRoot);

            List<TreeNode> weightedTree = WeightTree(tree, tree[indexInMainTree]);
            PrintLayeredWeightedTree(weightedTree, weightedTree[FindNodeFromRootString(weightedTree,tree[indexInMainTree].nodeRoot)]);

            //TODO/NOTE ON THE BELOW< EVEN WITH FUNCTION STUBS, I DON'T HAVE A GOOD WAY TO TRACK WEIGHT OF WAHT IT NEEDS TO BE, MAYBE DO IN FUNCTION
            //FIND ELEMNT THAT IS DIFFERENT HTATN IT'S NEIGHBORS BUT ALL CHILDREN WEIGHT ARE THE SAME
            int suspectIndex = 0;
            int suspectNodeWeight = 0;
            bool suspectFound = false;
            string suspectNodeRoot = "";
            foreach (TreeNode n in weightedTree)
            {
                if (!suspectFound)
                {
                    if (FindNodeFromRootString(weightedTree,  tree[indexInMainTree].nodeRoot)==0){}
                    else if (AreNeighborsDifferentSameWeight(weightedTree, n) && AreChildrenSameWeight(weightedTree, n))
                    {
                        suspectIndex = FindNodeFromRootString(weightedTree, n.nodeRoot);
                        suspectNodeWeight = n.nodeNumber;
                        Console.WriteLine("Local Number - " + suspectNodeWeight);
                        suspectFound = true;
                        suspectNodeRoot = n.nodeRoot;
                    }
                }
            }



        }

        public static bool AreNeighborsDifferentSameWeight(List<TreeNode> weightedTree, TreeNode node)
        {                
            //Find thing that points to me
            Console.WriteLine("Analyzing node - " + node.nodeRoot);
            bool pointToMeFound = false;
            TreeNode nodePointsToMe = new TreeNode();
            foreach (TreeNode tn in weightedTree)
            {
                if (tn.nodePointers.Contains(node.nodeRoot) && !pointToMeFound)
                {
                    nodePointsToMe = tn;
                    pointToMeFound = true;
                }
            }
            
            //Find weight of all its children, need to ensure I am the differnet one
            bool amDifferentThanNeighbors = false;
            List<int> siblingWeights = new List<int>();

            //Get weight of all my siblings
            foreach (string s in nodePointsToMe.nodePointers)
            {
                int indexOfChild = FindNodeFromRootString(weightedTree, s);
                siblingWeights.Add(weightedTree[indexOfChild].localWeight);
                Console.WriteLine("Sibling Weight - " + weightedTree[indexOfChild].localWeight);
            }

            siblingWeights.Remove(node.localWeight);
            if (!siblingWeights.Contains(node.localWeight))
            {
                amDifferentThanNeighbors = true;
            }

            return amDifferentThanNeighbors;
        }

        public static bool AreChildrenSameWeight(List<TreeNode> weightedTree, TreeNode node)
        {
            bool areChildrenSameWeight = true;
            List<int> childweights = new List<int>();

            //Get weight of all my children
            foreach (string s in node.nodePointers)
            {
                int indexOfChild = FindNodeFromRootString(weightedTree, s);
                childweights.Add(weightedTree[indexOfChild].localWeight);
            }
            
            //Compare all
            int comparisonWeight = childweights.First();
            foreach (int i in childweights)
            {
                if (i != comparisonWeight)
                {
                    areChildrenSameWeight = false;
                }
            }

            return areChildrenSameWeight;
        }

        public static List<TreeNode> WeightTree(List<TreeNode> inputTree, TreeNode root)
        {
            List<TreeNode> weightedTree = new List<TreeNode>();
            int testCounter = 0;
            foreach (TreeNode n in inputTree)
            {
                n.localWeight = FindWeight(inputTree, n);
                //Console.WriteLine("Local weight - "+n.localWeight + " - " + testCounter);
                weightedTree.Add(n);
                testCounter++;
            }

            return weightedTree;
        }

        public static void PrintLayeredWeightedTree(List<TreeNode> tree, TreeNode root)
        {
            int layerTracker = 0;
            List<TreeNode> previousLayer = new List<TreeNode>();
            List<TreeNode> currentLayer = new List<TreeNode>();
            bool childrenRemain = true;
            previousLayer.Add(root);
            Console.WriteLine("Layer " + layerTracker + " - " + previousLayer.First().localWeight);
            layerTracker++;

            while (childrenRemain)
            {
                bool atleastOneWithChild = false;
                foreach (TreeNode n in previousLayer)
                {
                    Console.WriteLine("New node");
                    if (n.nodePointers.Count>0)
                    {
                        atleastOneWithChild = true;
                        foreach (string s in n.nodePointers)
                        {
                            TreeNode nextNode = tree[FindNodeFromRootString(tree, s)];
                            currentLayer.Add(nextNode);
                            Console.WriteLine("Layer " + layerTracker + " - " + nextNode.localWeight + " - " + nextNode.nodeRoot+ " - " + nextNode.nodeNumber);
            


                        }
                    }
                }
                previousLayer.Clear();
                foreach (TreeNode tn in currentLayer)
                {
                    TreeNode nodeCopy = tn;
                    previousLayer.Add(nodeCopy);
                }

                //POTENTIALLY TRY TO DO IS SUSPECT IN CURRENT LAYER - NEED ELEMENT THAT IS DIFFERENT FROM NEIGHBORS BUT ALL CHILDREN ARE EQUAL
                
                {

                }

                currentLayer.Clear();
                childrenRemain = atleastOneWithChild;
                layerTracker++;
            }
                
        }

        

        public static int FindNodeFromRootString(List<TreeNode> tree, string nodeRoot)
        {
            int indexTracker = 0;

            foreach (TreeNode n in tree)
            {
                if (n.nodeRoot==nodeRoot)
                {
                    indexTracker = tree.IndexOf(n);
                }
            }
            return indexTracker;
        }

        public static int FindWeight(List<TreeNode> tree, TreeNode currentNode)
        {
            int runningWeightSum = currentNode.nodeNumber;
            if (currentNode.nodePointers.Count==0)
            {
                return currentNode.nodeNumber;
            }
            else
            {
                foreach (string s in currentNode.nodePointers)
                {
                    int indexOfNode = FindNodeFromRootString(tree, s);
                    int currentWeight = FindWeight(tree, tree[indexOfNode]);
                    runningWeightSum += currentWeight;
                }
                return runningWeightSum;
            }
        }

        public static bool IsBalanced(List<TreeNode> tree, TreeNode currentNode, TreeNode rootNode)
        {
            bool balancedTracker = true;
            int myWeight = currentNode.nodeNumber;
            // At node, for each node pointer, is the weight equal
            int comparisonWeight = 0;
            int nodePoitnerTracker = 1;
            foreach (string s in currentNode.nodePointers)
            {
                int indexOfNode = FindNodeFromRootString(tree, s);
                int currentWeight = FindWeight(tree, tree[indexOfNode]);
                if (nodePoitnerTracker==1){comparisonWeight=currentWeight;nodePoitnerTracker++;}
                else 
                {
                    if (currentWeight+myWeight!=comparisonWeight){balancedTracker=false;}
                    nodePoitnerTracker++;
                }
            }
            return balancedTracker;
        }

        public static List<TreeNode> TrimTree(List<TreeNode> tree)
        {
            List<TreeNode> trimmedTree = new List<TreeNode>();
            foreach (TreeNode t in tree)
            {
                if (t.nodePointers.Count>0){trimmedTree.Add(t);}
            }
            return trimmedTree;
        }
        public static int FindBase(List<TreeNode> tree)
        {
            bool baseFound = false;
            int treeTracker = 0;
            while (!baseFound && treeTracker<tree.Count)
            {
                bool sawBase = false;
                string currentRoot = tree[treeTracker].nodeRoot;
                //Console.WriteLine("Checking root - " + currentRoot + " index - " + treeTracker.ToString());
                for (int i = 0; i<tree.Count;i++)
                {
                    if (i==treeTracker)
                    {
                    }
                    else
                    {
                        foreach (string s in tree[i].nodePointers)
                        {
                            if (currentRoot==s){sawBase = true;}
                        }
                    }
                }
                if (!sawBase){baseFound=true;}
                treeTracker++;
            }

            return (treeTracker-1);
        }

        public static TreeNode CreateNodeFromString(string s)
        {
            TreeNode node = new TreeNode();
            string[] chunks = s.Split(new string[] {" "}, StringSplitOptions.None);
            node.nodeRoot = chunks[0];
            node.nodeNumber = Int32.Parse(chunks[1].Split('(',')')[1]);

            if (chunks.Count()>2)
            {
                int chunksIndex = 3;
                while (chunksIndex<chunks.Count())
                {
                    if (chunksIndex == chunks.Count()-1)
                    {
                        node.nodePointers.Add(chunks[chunksIndex]);
                    }
                    else
                    {
                        node.nodePointers.Add(chunks[chunksIndex].Split(',')[0]);
                    }
                    chunksIndex++;
                }
            }

            return node;
        }
    }


    public class TreeNode
    {
        public TreeNode()
        {
            nodeRoot="";
            nodeNumber=0;
            nodePointers = new List<string>();
            localWeight = 0;
        }

        public string nodeRoot;
        public int nodeNumber;
        public List<string> nodePointers;
        public int localWeight;
    }
}