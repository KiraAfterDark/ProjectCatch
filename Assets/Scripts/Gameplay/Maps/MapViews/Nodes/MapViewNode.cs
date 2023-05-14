using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews.Nodes
{
    public class MapViewNode : MonoBehaviour
    {
        public MapNode MapNode { get; private set; }

        public void Initialize(MapNode mapNode)
        {
            MapNode = mapNode;
        }
    }
}
