using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapNode
    {
        public List<MapNode> Prev { get; }
        public List<MapConnection> Connections { get; }
        
        public Vector2Int Position { get; }

        public Vector3 WorldPosition => new Vector3(Position.x * worldScalar.x, 0, Position.y * worldScalar.z);
        
        public MapNodeType NodeType { get; }

        private readonly Vector3 worldScalar = new Vector3(2, 0, 5);

        public MapNode(Vector2Int position)
        {
            Prev = new List<MapNode>();
            Connections = new List<MapConnection>();

            Position = position;

            NodeType = (MapNodeType)Random.Range(1, Enum.GetValues(typeof(MapNodeType)).Length);
        }

        #region Add/Remove
        
        public bool AddNext(MapNode next, params MapConnectionType[] availableConnections)
        {
            foreach (MapConnection connection in Connections)
            {
                if (connection.To == next)
                {
                    return false;
                }
            }
            
            MapConnection c = new (this, next, availableConnections);
            Connections.Add(c);
            return true;
        }

        public bool AddPrev(MapNode prev)
        {
            if (!Prev.Contains(prev))
            {
                Prev.Add(prev);
                return true;
            }

            return false;
        }

        public bool RemoveNext(MapNode next)
        {
            foreach(MapConnection connection in Connections)
            {
                if (connection.To == next)
                {
                    Connections.Remove(connection);
                    return true;
                }
            }

            Debug.LogWarning($"Node ({Position}) is not connected to Node ({next.Position}).");
            return false;
        }

        public bool RemovePrev(MapNode prev)
        {
            if (Prev.Contains(prev))
            {
                Prev.Remove(prev);
                return true;
            }

            return false;
        }
        
        #endregion

        public bool ConnectedTo(MapNode node)
        {
            foreach (MapConnection connection in Connections)
            {
                if (connection.To == node)
                {
                    return true;
                }
            }

            return false;
        }
        
        public override string ToString()
        {
            string s = $"{NodeType} - {Position} - Connections: {Connections.Count}";
            return s;
        }
    }
}
