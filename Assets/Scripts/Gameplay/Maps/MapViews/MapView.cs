using System.Collections.Generic;
using ProjectCatch.Gameplay.Maps.MapViews.Nodes;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews
{
    public class MapView : MonoBehaviour
    {
        [SerializeField]
        private MapViewProperties properties;

        private readonly Dictionary<MapNode, MapViewNode> mapViewNodes = new Dictionary<MapNode, MapViewNode>();

        private MapViewNode currentNode;

        private void Awake()
        {
            ClearMap();
        }

        public void DrawMap(Map map)
        {
            ClearMap();
            
            List<MapNode> mapNodes = map.GetAllNodes();

            foreach (MapNode mapNode in mapNodes)
            {
                MapViewNode node = GetNode(mapNode);

                foreach (MapConnection connection in mapNode.Connections)
                {
                    node.AddConnection(GetNode(connection.To), properties.ConnectionPrefabs[connection.Type], connection);
                }
            }
        }

        public MapViewNode GetNode(MapNode node)
        {
            if (mapViewNodes.TryGetValue(node, out MapViewNode viewNode))
            {
                return viewNode;
            }
            
            viewNode = Instantiate(properties.MapNodePrefabs[node.NodeType], transform);
            viewNode.Initialize(node);
            mapViewNodes.Add(node, viewNode);
            return viewNode;
        }

        private void ClearMap()
        {
            foreach (MapViewNode mapNodeObject in mapViewNodes.Values)
            {
                DestroyImmediate(mapNodeObject.gameObject);
            }

            mapViewNodes.Clear();
        }

        public void SetCurrentNode(MapNode node)
        {
            if (currentNode != null)
            {
                currentNode.StopCurrentHighlight();
            }

            currentNode = mapViewNodes[node];
            currentNode.StartCurrentHighlight();
        }

        public void StopCurrentNode()
        {
            if (currentNode != null)
            {
                currentNode.StopCurrentHighlight();
            }
        }
    }
}
