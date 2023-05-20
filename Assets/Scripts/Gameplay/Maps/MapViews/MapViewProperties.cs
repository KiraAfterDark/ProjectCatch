using System.Collections.Generic;
using ProjectCatch.Gameplay.Maps.MapViews.Connections;
using ProjectCatch.Gameplay.Maps.MapViews.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ProjectCatch.Gameplay.Maps.MapViews
{
    [CreateAssetMenu(fileName = "New Map View Properties", menuName = "Map/Map View/Properties", order = 0)]
    public class MapViewProperties : SerializedScriptableObject
    {
        [Title("Map View")]
        [SerializeField]
        private Dictionary<MapNodeType, MapViewNode> mapNodePrefabs = 
            new Dictionary<MapNodeType, MapViewNode>();

        public Dictionary<MapNodeType, MapViewNode> MapNodePrefabs => mapNodePrefabs;
        
        [SerializeField]
        private MapViewNode currentNodePrefab;

        public MapViewNode CurrentNodePrefab => currentNodePrefab;

        [SerializeField]
        private Dictionary<MapConnectionType, MapViewConnection> connectionPrefabs =
            new Dictionary<MapConnectionType, MapViewConnection>();

        public Dictionary<MapConnectionType, MapViewConnection> ConnectionPrefabs => connectionPrefabs;
    }
}
