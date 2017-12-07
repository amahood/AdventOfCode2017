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
        }

        public string nodeRoot;
        public int nodeNumber;
        public List<string> nodePointers;
    }
}