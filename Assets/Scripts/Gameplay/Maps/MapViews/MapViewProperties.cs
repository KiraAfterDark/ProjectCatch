using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews
{
    [CreateAssetMenu(fileName = "New Map View Properties", menuName = "Map/Map View/Properties", order = 0)]
    public class MapViewProperties : SerializedScriptableObject
    {
        [Title("Map View")]
        [SerializeField]
        private Dictionary<MapNodeType, GameObject> mapNodePrefabs = new Dictionary<MapNodeType, GameObject>();

        public Dictionary<MapNodeType, GameObject> MapNodePrefabs => mapNodePrefabs;

        [SerializeField]
        private LineRenderer lineRendererPrefab;

        public LineRenderer LineRendererPrefab => lineRendererPrefab;

        [SerializeField]
        private Vector2 spacingMod = Vector2.one;

        public Vector2 SpacingMod => spacingMod;
    }
}
