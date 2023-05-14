using System.Collections.Generic;
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
        private Dictionary<MapConnectionType, LineRenderer> lineRendererPrefabs =
            new Dictionary<MapConnectionType, LineRenderer>();

        public Dictionary<MapConnectionType, LineRenderer> LineRendererPrefabs => lineRendererPrefabs;

        [SerializeField]
        private Vector3 spacingMod = Vector2.one;

        public Vector3 SpacingMod => spacingMod;
    }
}
