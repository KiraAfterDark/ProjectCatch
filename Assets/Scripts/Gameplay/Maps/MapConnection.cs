using System;
using Random = UnityEngine.Random;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapConnection
    {
        public MapNode From { get; }
        public MapNode To { get; }
        
        public MapConnectionType Type { get; }

        public MapConnection(MapNode from, MapNode to, params MapConnectionType[] availableTypes)
        {
            From = from;
            To = to;

            if (availableTypes == null || availableTypes.Length == 0)
            {
                Type = (MapConnectionType)Random.Range(1, Enum.GetValues(typeof(MapConnectionType)).Length);
            }
            else
            {
                Type = availableTypes[Random.Range(0, availableTypes.Length)];
            }
        }
    }
    
    public enum MapConnectionType
    {
        None = 0,
        
        Grass = 1,
        Cave = 2,
        Water = 3,
        Waterfall = 4,
    }
}
