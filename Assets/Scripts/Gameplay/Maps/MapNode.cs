using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapNode
    {
        public List<MapNode> Prev { get; }
        public List<MapNode> Next { get; }
        
        public Vector2Int Position { get; }

        public MapNode(Vector2Int position)
        {
            Next = new List<MapNode>();
            Prev = new List<MapNode>();

            Position = position;
        }

        public void AddNext(MapNode next)
        {
            Next.Add(next);
        }

        public void AddPrev(MapNode prev)
        {
            Prev.Add(prev);
        }
    }
}
