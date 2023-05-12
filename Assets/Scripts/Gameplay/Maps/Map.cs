using System.Collections.Generic;
using ProjectCatch.Utilities;
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
            Vector2Int size = properties.Size;

            for (int i = 0; i < properties.Lines; i++)
            {
                BuildLine(Start);
            }
        }

        private void BuildLine(MapNode startNode)
        {
            bool building = true;
            MapNode currentNode = startNode;
            int increment = 1;
            Vector2Int pos = currentNode.Position + new Vector2Int(Random.Range(-properties.Size.x, properties.Size.x), increment);
            while(building)
            {
                if (!Nodes.TryGetValue(pos, out MapNode node))
                {
                    node = new MapNode(pos);
                    Nodes.Add(pos, node);
                }
                
                currentNode.AddNext(node);
                node.AddPrev(currentNode);
                currentNode = node;

                increment = 1;
                if (node.Position.y + increment < properties.Size.y)
                {
                    pos.x = Random.Range(-properties.Size.x, properties.Size.x);
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
