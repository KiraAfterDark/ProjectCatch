using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapDebug : MonoBehaviour
    {
        private Map map;

        [SerializeField]
        private MapProperties properties;

        private List<GameObject> mapNodeObjects = new List<GameObject>();

        private List<LineRenderer> lines = new List<LineRenderer>();

        [Button("Generate Map")]
        public void GenerateMap()
        {
            ClearMap();
            
            map = new Map(properties);
            
            DrawMap();
        }

        private void ClearMap()
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

        private void DrawMap()
        {
            List<MapNode> mapNodes = map.GetAllNodes();

            foreach (MapNode mapNode in mapNodes)
            {
                GameObject node = Instantiate(properties.NodePrefab, transform);
                node.transform.position = new Vector3(mapNode.Position.x, mapNode.Position.y, 0) * properties.SpacingMod;

                mapNodeObjects.Add(node);

                foreach (MapNode next in mapNode.Next)
                {
                    LineRenderer line = Instantiate(properties.LineRendererPrefab, transform);
                    line.SetPosition(0, node.transform.position);
                    line.SetPosition(1, new Vector3(next.Position.x, next.Position.y, 0) * properties.SpacingMod);

                    lines.Add(line);
                }
            }
        }
    }
}
