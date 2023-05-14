using System.Collections.Generic;
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
        private Dictionary<MapNodeType, GameObject> mapNodePrefabs = 
            new Dictionary<MapNodeType, GameObject>();

        public Dictionary<MapNodeType, GameObject> MapNodePrefabs => mapNodePrefabs;
        
        [SerializeField]
        private GameObject currentNodePrefab;

        public GameObject CurrentNodePrefab => currentNodePrefab;

        [SerializeField]
        private Dictionary<MapConnectionType, LineRenderer> lineRendererPrefabs =
            new Dictionary<MapConnectionType, LineRenderer>();

        public Dictionary<MapConnectionType, LineRenderer> LineRendererPrefabs => lineRendererPrefabs;

        [SerializeField]
        private Vector2 spacingMod = Vector2.one;

        public Vector2 SpacingMod => spacingMod;
    }
}
