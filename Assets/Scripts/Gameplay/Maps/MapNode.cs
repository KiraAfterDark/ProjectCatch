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

        public Vector3 FlatPosition => new Vector3(Position.x, 0, Position.y);
        public Vector2 VerticalPosition => new Vector2(Position.x, Position.y);
        
        public MapNodeType NodeType { get; }

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
                    Debug.LogWarning($"Node ({Position}) already has connection to Node ({next.Position}).");
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
        
        public override string ToString()
        {
            string s = $"{NodeType} - {Position} - Connections: {Connections.Count}";
            return s;
        }
    }
}
