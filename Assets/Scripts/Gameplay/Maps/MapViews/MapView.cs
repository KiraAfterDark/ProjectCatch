using System;
using System.Collections.Generic;
using ProjectCatch.Gameplay.Maps.MapViews.Connections;
using ProjectCatch.Gameplay.Maps.MapViews.Nodes;
using ProjectCatch.Input;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews
{
    public class MapView : MonoBehaviour
    {
        [SerializeField]
        private MapViewProperties properties;

        private readonly List<MapViewNode> mapNodeObjects = new List<MapViewNode>();
        private readonly List<MapViewConnection> lines = new List<MapViewConnection>();

        private PlayerInput playerInput;

        private void Awake()
        {
            InitializeInput();
            
            ClearMap();
        }

        private void OnEnable()
        {
            playerInput.Map.Enable();
        }

        private void OnDisable()
        {
            playerInput.Map.Disable();
        }

        private void Start()
        {
            DrawMap(GameplayController.Instance.Map);
        }

        private void InitializeInput()
        {
            playerInput = new PlayerInput();

            playerInput.Map.Select.performed += ctx => Select();
        }
        
        public void DrawMap(Map map)
        {
            ClearMap();
            
            List<MapNode> mapNodes = map.GetAllNodes();

            foreach (MapNode mapNode in mapNodes)
            {
                MapViewNode node = Instantiate(properties.MapNodePrefabs[mapNode.NodeType], transform);
                node.Initialize(mapNode);
                Vector3 position = Vector3.Scale(mapNode.FlatPosition, properties.SpacingMod);
                node.transform.position = position;
            
                mapNodeObjects.Add(node);
            
                foreach (MapConnection connection in mapNode.Connections)
                {
                    AddLine(properties.ConnectionPrefabs[connection.Type], connection);
                }
            }
        }

        public void DrawMap(Map map, MapNode currentNode)
        {
            ClearMap();
            
            List<MapNode> mapNodes = map.GetAllNodes();

            foreach (MapNode mapNode in mapNodes)
            {
                MapViewNode node;
                if (mapNode == currentNode)
                {
                    node = Instantiate(properties.CurrentNodePrefab, transform);
                }
                else
                {
                    node = Instantiate(properties.MapNodePrefabs[mapNode.NodeType], transform);
                }

                node.Initialize(mapNode);

                node.transform.position = Vector3.Scale(mapNode.FlatPosition , properties.SpacingMod);
            
                mapNodeObjects.Add(node);
            
                foreach (MapConnection connection in mapNode.Connections)
                {
                    AddLine(properties.ConnectionPrefabs[connection.Type], connection);
                }
            }
        }

        public void ClearMap()
        {
            foreach (MapViewNode mapNodeObject in mapNodeObjects)
            {
                DestroyImmediate(mapNodeObject.gameObject);
            }

            mapNodeObjects.Clear();

            foreach (MapViewConnection line in lines)
            {
                DestroyImmediate(line.gameObject);
            }

            lines.Clear();
        }

        private void AddLine(MapViewConnection prefab, MapConnection connection)
        {
            MapViewConnection line = Instantiate(prefab, transform);
            line.Initialize(connection, properties.SpacingMod);
            
            lines.Add(line);
        }

        private void Select()
        {
            Debug.Log("Boop");
            var pos = playerInput.Map.Position.ReadValue<Vector2>();

            if (Camera.main != null)
            {
                Debug.Log("Blah");
                Ray ray = Camera.main.ScreenPointToRay(pos);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if(hit.collider.TryGetComponent(out MapViewNode mapViewNode))
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 500, Color.green, 2);
                        MapNode node = mapViewNode.MapNode;
                        Debug.Log(node.ToString());
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 500, Color.red, 2);
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * 500, Color.red, 2);
                }
            }
        }
    }
}
