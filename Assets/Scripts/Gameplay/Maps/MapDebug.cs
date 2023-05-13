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

        [Button("Generate Map")]
        public void GenerateMap()
        {
            map = new Map(properties);
            mapView.DrawMap(map);
        }
    }
}
