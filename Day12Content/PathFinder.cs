using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day12Content
{
    class PathNode
    {
        public string name;
        public bool isStart;
        public bool isEnd;
        public bool isBig;

        public List<PathNode> connectedNodes;

        public PathNode(string input)
        {
            name = input;
            isStart = (name.ToLower() == "start");
            isEnd = (name.ToLower() == "end");
            isBig = (name.ToUpper() == name);
            connectedNodes = new List<PathNode>();
        }

        public void AddConnection(PathNode node)
        {
            if (!connectedNodes.Contains(node))
            {
                connectedNodes.Add(node);
                node.AddConnection(this);
            }
        }

        public List<PathNode> GetConnections()
        {
            return connectedNodes;
        }

        public void PrintNode()
        {
            string connections = "";
            for(int i = 0; i < connectedNodes.Count; i++)
            {
                connections += connectedNodes[i].name;
                if (i < connectedNodes.Count - 1)
                    connections += "|";
            }
            Console.WriteLine(name + " -> (" + connections+")");
        }
    }

    class Path
    {
        public List<PathNode> nodes;
        public PathNode currenNode;
        public bool isComplete;
        public bool isBroken;
        public bool containsSmallNodeTwice;

        public Path(PathNode startNode)
        {
            nodes = new List<PathNode>();
            SetNewPathNode(startNode);
        }

        public Path(List<PathNode> previousPath)
        {
            nodes = new List<PathNode>(previousPath);
            currenNode = nodes[nodes.Count - 1];
        }

        public void SetNewPathNode(PathNode node)
        {
            nodes.Add(node);
            currenNode = node;
            if (currenNode.isEnd)
                isComplete = true;

            if (!containsSmallNodeTwice)
            {
                List<PathNode> smallNodes = new List<PathNode>();
                foreach (PathNode n in nodes)
                {
                    if (!n.isBig && !n.isStart && !n.isEnd)
                    {
                        smallNodes.Add(n);
                    }
                }
                for (int i = 0; i < smallNodes.Count; i++)
                {
                    for (int n = i + 1; n < smallNodes.Count; n++)
                    {
                        if (smallNodes[i].name == smallNodes[n].name)
                            containsSmallNodeTwice = true;
                    }
                }
            }
        }

        public List<PathNode> GetNextPossibleNodes()
        {
            List<PathNode> connections = currenNode.GetConnections();
            List<PathNode> possibleNodes = new List<PathNode>();

            foreach(PathNode p in connections)
            {
                if(p.isBig || p.isEnd)
                {
                    possibleNodes.Add(p);
                }
                else if(!p.isBig && !p.isStart)
                {
                    if (!nodes.Contains(p) || !containsSmallNodeTwice)
                        possibleNodes.Add(p);
                }
            }
            return possibleNodes;
        }

        public string GetPathString()
        {
            string result = "";
            foreach(PathNode node in nodes)
            {
                result += node.name + ",";
            }
            return result;
        }
    }


    class PathFinder
    {
        List<PathNode> nodes;
        PathNode startNode;
        PathNode endNode;

        public PathFinder()
        {
            nodes = new List<PathNode>();
        }

        public void PrintNodes()
        {
            Console.WriteLine("");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Printing all nodes");
            foreach (PathNode node in nodes)
            {
                node.PrintNode();
            }
        }

        public void AddConnectionFromInput(string input)
        {
            string nodeAString = input.Split('-')[0];
            string nodeBString = input.Split('-')[1];
            PathNode nodeA = AddOrGetNode(nodeAString);
            PathNode nodeB = AddOrGetNode(nodeBString);

            nodeA.AddConnection(nodeB);
        }

        public PathNode AddOrGetNode(string nodeString)
        {
            PathNode node = nodes.Find(n => n.name == nodeString);
            if(node==null)
            {
                node = new PathNode(nodeString);
                nodes.Add(node);
                if (node.isStart)
                    startNode = node;
                else if (node.isEnd)
                    endNode = node;
            }
            return node;
        }

        public int FindAllPaths()
        {
            List<Path> paths = new List<Path>();
            List<Path> newPaths = new List<Path>();
            paths.Add(new Path(startNode));

            bool processing = true;
            bool allComplete = false;
            while(processing)
            {
                allComplete = true;
                foreach(Path path in paths)
                {
                    Console.WriteLine("Checking Path " + path.GetPathString());
                    if (!path.isComplete && !path.isBroken)
                    {
                        allComplete = false;
                        List<PathNode> possibleNextNodes = path.GetNextPossibleNodes();
                        Console.WriteLine("Not complete. Found " + possibleNextNodes.Count + " new possible nodes");
                        if (possibleNextNodes != null && possibleNextNodes.Count>0)
                        {
                            Console.WriteLine("Set new PathNode to " + possibleNextNodes[0].name);
                            if (possibleNextNodes.Count == 1)
                            {
                                path.SetNewPathNode(possibleNextNodes[0]);
                            }
                            else if (possibleNextNodes.Count > 1)
                            {
                                for (int i = 1; i < possibleNextNodes.Count; i++)
                                {
                                    Path newPath = new Path(path.nodes);
                                    newPath.SetNewPathNode(possibleNextNodes[i]);
                                    newPaths.Add(newPath);
                                    Console.WriteLine("Added new Path " + newPath.GetPathString());
                                }
                                path.SetNewPathNode(possibleNextNodes[0]);
                            }
                        }
                        else
                        {
                            path.isBroken = true;
                        }
                    }
                }
                //Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine("");
                paths.AddRange(newPaths);
                newPaths.Clear();
                if(allComplete)
                {
                    processing = false;
                }
            }

            int result = 0;
            for(int i = 0; i < paths.Count; i++)
            {
                if (paths[i].isComplete)
                    result++;
            }
            return result;
        }
    }

}
