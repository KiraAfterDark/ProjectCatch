using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews
{
    public class MapView : MonoBehaviour
    {
        [SerializeField]
        private MapViewProperties properties;

        private readonly List<GameObject> mapNodeObjects = new List<GameObject>();
        private readonly List<LineRenderer> lines = new List<LineRenderer>();

        private void Awake()
        {
            ClearMap();
        }

        private void Start()
        {
            DrawMap(GameplayController.Instance.Map);
        }
        
        public void DrawMap(Map map)
        {
            ClearMap();
            
            List<MapNode> mapNodes = map.GetAllNodes();

            foreach (MapNode mapNode in mapNodes)
            {
                GameObject node = Instantiate(properties.MapNodePrefabs[mapNode.NodeType], transform);
                node.transform.position = new Vector3(mapNode.Position.x, mapNode.Position.y, 0) * properties.SpacingMod;
            
                mapNodeObjects.Add(node);
            
                foreach (MapConnection connection in mapNode.Connections)
                {
                    LineRenderer line = Instantiate(properties.LineRendererPrefabs[connection.Type], transform);
                    line.SetPosition(0, node.transform.position);
                    line.SetPosition(1, new Vector3(connection.To.Position.x, 
                                                    connection.To.Position.y, 0) * properties.SpacingMod);
            
                    lines.Add(line);
                }
            }
        }

        public void DrawMap(Map map, MapNode currentNode)
        {
            ClearMap();
            
            List<MapNode> mapNodes = map.GetAllNodes();

            foreach (MapNode mapNode in mapNodes)
            {
                GameObject node;
                if (mapNode == currentNode)
                {
                    node = Instantiate(properties.CurrentNodePrefab, transform);
                }
                else
                {
                    node = Instantiate(properties.MapNodePrefabs[mapNode.NodeType], transform);
                }

                node.transform.position = new Vector3(mapNode.Position.x, mapNode.Position.y, 0) * properties.SpacingMod;
            
                mapNodeObjects.Add(node);
            
                foreach (MapConnection connection in mapNode.Connections)
                {
                    LineRenderer line = Instantiate(properties.LineRendererPrefabs[connection.Type], transform);
                    line.SetPosition(0, node.transform.position);
                    line.SetPosition(1, new Vector3(connection.To.Position.x, 
                                                    connection.To.Position.y, 0) * properties.SpacingMod);
            
                    lines.Add(line);
                }
            }
        }
        
        public void ClearMap()
        {
            foreach (GameObject mapNodeObject in mapNodeObjects)
            {
                DestroyImmediate(mapNodeObject);
            }

            mapNodeObjects.Clear();

            foreach (LineRenderer line in lines)
            {
                DestroyImmediate(line.gameObject);
            }
            
            lines.Clear();
        }
    }
}
