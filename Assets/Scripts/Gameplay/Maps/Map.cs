using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps
{
    public class Map
    {
        private MapProperties properties;
        
        public MapNode Start { get; }
        public MapNode End { get; }

        private Dictionary<Vector2Int, MapNode> Nodes;
        
        public Map(MapProperties properties)
        {
            this.properties = properties;

            Start = new MapNode(new Vector2Int(0, -1));
            End = new MapNode(new Vector2Int(0, properties.Size.y));
            Nodes = new Dictionary<Vector2Int, MapNode>();
            
            for (int i = 0; i < properties.Lines; i++)
            {
                BuildLine(Start);
            }

            // Removing empty branches
            List<MapNode> nodes = new List<MapNode>(Nodes.Values);
            foreach (MapNode node in nodes)
            {
                if (node.Next.Count == 0)
                {
                    foreach (MapNode prev in node.Prev)
                    {
                        prev.RemoveNext(node);
                    }
                    
                    Nodes.Remove(node.Position);
                }
            }
        }

        private void BuildLine(MapNode startNode)
        {
            bool building = true;
            MapNode currentNode = startNode;
            int increment = 1;
            Vector2Int pos = currentNode.Position + new Vector2Int(Random.Range(-properties.Size.x, properties.Size.x + 1), increment);
            while(building)
            {
                if (!Nodes.TryGetValue(pos, out MapNode node))
                {
                    node = new MapNode(pos);
                    Nodes.Add(pos, node);
                }
                
                currentNode.AddNext(node);
                node.AddPrev(currentNode);

                for (int i = 0; i < properties.SplitAttempts; i++)
                {
                    increment = Random.Range(1, properties.IncrementMax + 1);

                    if (node.Position.y + increment >= properties.Size.y)
                    {
                        continue;
                    }
                    
                    if (Random.Range(0, 1.0f) < properties.SplitChance)
                    {
                        Vector2Int extraPos = currentNode.Position +
                                              new Vector2Int(Random.Range(-properties.Size.x, properties.Size.x),
                                                             increment);
                        if (!Nodes.TryGetValue(extraPos, out MapNode extra))
                        {
                            extra = new MapNode(extraPos);
                            Nodes.Add(extraPos, extra);
                        }

                        currentNode.AddNext(extra);
                        extra.AddPrev(currentNode);
                    }
                }

                currentNode = node;

                increment = Random.Range(1, properties.IncrementMax + 1);
                if (node.Position.y + increment < properties.Size.y)
                {
                    pos.x = Random.Range(-properties.Size.x, properties.Size.x + 1);
                    pos.y += increment;
                }
                else
                {
                    node.AddNext(End);
                    End.AddPrev(node);
                    building = false;
                }
            }
        }

        public List<MapNode> GetAllNodes()
        {
            List<MapNode> nodes = new ();
            nodes.Add(Start);
            nodes.AddRange(Nodes.Values);
            nodes.Add(End);

            return nodes;
        }
    }
}
