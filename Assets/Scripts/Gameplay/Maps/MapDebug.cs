using System.Collections.Generic;
using ProjectCatch.Gameplay.Maps.MapViews;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapDebug : MonoBehaviour
    {
        private Map map;

        [SerializeField]
        private MapView mapView;

        [SerializeField]
        private MapProperties properties;

        [SerializeField]
        private bool randomSelected = false;

        [Button("Generate Map")]
        public void GenerateMap()
        {
            map = new Map(properties);

            if (randomSelected)
            {
                List<MapNode> nodes = map.GetAllNodes();
                mapView.DrawMap(map, nodes[Random.Range(0, nodes.Count)]);
            }
            else
            {
                mapView.DrawMap(map);
            }
        }
    }
}
