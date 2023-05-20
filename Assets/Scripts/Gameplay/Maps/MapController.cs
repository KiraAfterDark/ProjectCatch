using DG.Tweening;
using Fsi.Runtime;
using ProjectCatch.Gameplay.Maps.Characters;
using ProjectCatch.Gameplay.Maps.MapViews;
using ProjectCatch.Gameplay.Maps.MapViews.Nodes;
using ProjectCatch.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCatch.Gameplay.Maps
{
    public class MapController : MbSingleton<MapController>
    {
        private Map Map => GameplayController.Instance.Map;

        private MapNode currentNode;
        
        [Title("Map Controller")]

        [SerializeField]
        private MapView mapView;

        [Range(0, 1)]
        [SerializeField]
        private float encounterRate = 0.2f;

        [FormerlySerializedAs("mapCharacterPrefab")]
        [Title("Map Character")]

        [SerializeField]
        private MapTrainer mapTrainerPrefab;

        private MapTrainer mapTrainer;

        // Input
        private PlayerInput playerInput;

        private void Awake()
        {
            InitializeInput();
        }
        
        private void Start()
        {
            mapView.DrawMap(Map);

            currentNode = Map.Start;
            
            mapTrainer = Instantiate(mapTrainerPrefab, transform);
            mapTrainer.transform.position = Map.Start.WorldPosition;

            mapView.SetCurrentNode(Map.Start);
        }
        
        private void OnEnable()
        {
            playerInput.Map.Enable();
        }

        private void OnDisable()
        {
            playerInput.Map.Disable();
        }
        
        private void InitializeInput()
        {
            playerInput = new PlayerInput();

            playerInput.Map.Select.performed += ctx => Select();
        }
        
        private void Select()
        {
            var pos = playerInput.Map.Position.ReadValue<Vector2>();

            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if(hit.collider.TryGetComponent(out MapViewNode mapViewNode))
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 500, Color.green, 2);
                        MapNode node = mapViewNode.MapNode;

                        if (currentNode.ConnectedTo(node) && mapTrainer.CanMove)
                        {
                            mapTrainer.Move(node, FinishMove);
                            mapView.StopCurrentNode();
                        }
                    }
                }
            }
        }

        private void MoveTo(MapViewNode selectedNode)
        {
            MapNode node = selectedNode.MapNode;

            if (currentNode.ConnectedTo(node) && mapTrainer.CanMove)
            {
                mapTrainer.Move(node, FinishMove);
                selectedNode.StopCurrentHighlight();
            }
        }

        private void FinishMove(MapNode node)
        {
            if (node == Map.End)
            {
                mapView.StopCurrentNode();
                GameplayController.Instance.GenerateMap();
                mapView.DrawMap(Map);
                
                mapTrainer.transform.position = Map.Start.WorldPosition;
                node = Map.Start;
            }

            currentNode = node;

            mapView.SetCurrentNode(node);
        }
    }
}
