using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps
{
    public class Map
    {
        private readonly MapProperties properties;
        
        public MapNode Start { get; }
        public MapNode End { get; }

        private readonly Dictionary<Vector2Int, MapNode> nodes;
        
        public Map(MapProperties properties)
        {
            this.properties = properties;

            Start = new MapNode(new Vector2Int(0, -1));
            End = new MapNode(new Vector2Int(0, properties.Size.y));
            nodes = new Dictionary<Vector2Int, MapNode>();
            
            for (int i = 0; i < properties.Lines; i++)
            {
                BuildLine(Start);
            }

            // Removing empty branches
            List<MapNode> n = new List<MapNode>(nodes.Values);
            foreach (MapNode node in n)
            {
                if (node.Next.Count == 0)
                {
                    foreach (MapNode prev in node.Prev)
                    {
                        prev.RemoveNext(node);
                    }
                    
                    nodes.Remove(node.Position);
                }
            }
        }

        private void BuildLine(MapNode startNode)
        {
            bool building = true;
            MapNode currentNode = startNode;
            int increment = 1;
            Vector2Int pos = currentNode.Position 
                             + new Vector2Int(Random.Range(-properties.Size.x, properties.Size.x + 1)
                                                                   , increment);
            while(building)
            {
                if (!nodes.TryGetValue(pos, out MapNode node))
                {
                    node = new MapNode(pos);
                    nodes.Add(pos, node);
                }
                
                currentNode.AddNext(node, properties.WalkableConnections.ToArray());
                node.AddPrev(currentNode);

                // Try to split path
                for (int i = 0; i < properties.SplitAttempts; i++)
                {
                    increment = GetYIncrement();

                    if (node.Position.y + increment >= properties.Size.y - 1)
                    {
                        break;
                    }
                    
                    if (Random.Range(0, 1.0f) < properties.SplitChance)
                    {
                        Vector2Int extraPos = currentNode.Position 
                                              + new Vector2Int(GetXIncrement(currentNode.Position.x), increment);
                        if (!nodes.TryGetValue(extraPos, out MapNode extra))
                        {
                            extra = new MapNode(extraPos);
                            nodes.Add(extraPos, extra);
                        }
                        
                        currentNode.AddNext(extra, properties.SpecialConnections.ToArray());
                        extra.AddPrev(currentNode);
                    }
                }

                currentNode = node;

                increment = GetYIncrement();
                if (node.Position.y + increment < properties.Size.y)
                {
                    pos.x = currentNode.Position.x + GetXIncrement(currentNode.Position.x);
                    pos.y += increment;
                }
                else
                {
                    node.AddNext(End, properties.WalkableConnections.ToArray());
                    End.AddPrev(node);
                    building = false;
                }
            }
        }

        private int GetYIncrement()
        {
            return Random.Range(1, properties.IncrementMax.y + 1);
        }

        private int GetXIncrement(int xPos)
        {
            int min = -properties.IncrementMax.x;
            int max = properties.IncrementMax.x;

            if (xPos <= -properties.Size.x)
            {
                min = 0;
            }

            if (xPos >= properties.Size.x)
            {
                max = 0;
            }

            int increment = Random.Range(min, max + 1);
            return increment;
        }

        public List<MapNode> GetAllNodes()
        {
            List<MapNode> n = new ();
            n.Add(Start);
            n.AddRange(nodes.Values);
            n.Add(End);

            return n;
        }
    }
}
