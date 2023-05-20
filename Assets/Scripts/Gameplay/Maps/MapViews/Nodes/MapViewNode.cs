using System;
using System.Collections.Generic;
using DG.Tweening;
using ProjectCatch.Gameplay.Maps.MapViews.Connections;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews.Nodes
{
    public class MapViewNode : MonoBehaviour
    {
        public MapNode MapNode { get; private set; }

        private List<MapViewConnection> connections = new List<MapViewConnection>();

        [Title("Current Highlight")]

        [SerializeField]
        private float highlightMin = 0.25f;

        [SerializeField]
        private float highlightMax = 0.50f;

        [SerializeField]
        private float highlightTime = 0.5f;

        [SerializeField]
        private Ease highlightEase = Ease.Linear;

        private float highlightAmount;

        [Title("Next Bounce")]

        [SerializeField]
        private float bounceModMin = 0.8f;

        [SerializeField]
        private float bounceModMax = 1.2f;

        [SerializeField]
        private float bounceTime = 0.5f;

        [SerializeField]
        private Ease bounceEase = Ease.Linear;

        private float bounceAmount;

        [Title("References")]
        
        [SerializeField]
        private Sphere sphere;

        private float sphereRadius; 

        [SerializeField]
        private Sphere highlight;

        private Tween tween;

        public void Initialize(MapNode mapNode)
        {
            MapNode = mapNode;
            transform.position = mapNode.WorldPosition;

            sphereRadius = sphere.Radius;
            
            StopCurrentHighlight();
            StopNextBounce();
        }

        public void StartCurrentHighlight()
        {
            tween?.Kill();

            highlightAmount = highlightMin;
            tween = DOTween.To(() => highlightAmount, x => highlightAmount = x, highlightMax, highlightTime)
                           .OnUpdate(() =>
                           {
                               Color c = highlight.Color;
                               c.a = highlightAmount;
                               highlight.Color = c;
                           })
                           .SetLoops(-1, LoopType.Yoyo)
                           .SetEase(highlightEase);

            foreach (MapViewConnection connection in connections)
            {
                connection.SetCurrent(true);
            }
        }

        public void StopCurrentHighlight()
        {
            tween?.Kill();
            Color c = highlight.Color;
            c.a = 0;
            highlight.Color = c;
            
            foreach (MapViewConnection connection in connections)
            {
                connection.SetCurrent(false);
            }
        }

        public void StartNextBounce()
        {
            tween?.Kill();

            bounceAmount = bounceModMin;
            tween = DOTween.To(() => bounceAmount, x => bounceAmount = x, bounceModMax, bounceTime)
                           .OnUpdate(() =>
                           {
                               sphere.Radius = sphereRadius * bounceAmount;
                           })
                           .SetLoops(-1, LoopType.Yoyo)
                           .SetEase(bounceEase);
        }

        public void StopNextBounce()
        {
            tween?.Kill();
            sphere.Radius = sphereRadius;
        }

        public void AddConnection(MapViewNode to, MapViewConnection prefab, MapConnection connection)
        {
            MapViewConnection line = Instantiate(prefab, transform);
            line.Initialize(this, to, connection);

            connections.Add(line);
        }
    }
}
