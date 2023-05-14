using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapNode
    {
        public List<MapNode> Prev { get; }
        public List<MapNode> Next { get; }
        
        public List<MapConnection> Connections { get; }
        
        public Vector2Int Position { get; }
        
        public MapNodeType NodeType { get; }

        public MapNode(Vector2Int position)
        {
            Next = new List<MapNode>();
            Prev = new List<MapNode>();
            Connections = new List<MapConnection>();

            Position = position;

            NodeType = (MapNodeType)Random.Range(1, Enum.GetValues(typeof(MapNodeType)).Length);
        }

        public void AddNext(MapNode next, params MapConnectionType[] availableConnections)
        {
            if (!Next.Contains(next))
            {
                Next.Add(next);

                MapConnection connection = new (this, next, availableConnections);
                Connections.Add(connection);
            }
        }

        public void AddPrev(MapNode prev)
        {
            if (!Prev.Contains(prev))
            {
                Prev.Add(prev);
            }
        }

        public void RemoveNext(MapNode next)
        {
            if (Next.Contains(next))
            {
                Next.Remove(next);

                var check = new List<MapConnection>(Connections);
                foreach (MapConnection connection in check)
                {
                    if (connection.To == next)
                    {
                        Connections.Remove(connection);
                    }
                }
            }
        }

        public void RemovePrev(MapNode prev)
        {
            Prev.Remove(prev);
        }
    }
}
