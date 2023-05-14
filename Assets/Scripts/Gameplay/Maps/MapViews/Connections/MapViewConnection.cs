using System;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews.Connections
{
    public class MapViewConnection : MonoBehaviour
    {
        private MapConnection connection;
        
        [Title("Map View Connection")]

        [SerializeField]
        public Line line;

        public void Initialize(MapConnection connection, Vector3 scale)
        {
            this.connection = connection;
            
            line.Start = Vector3.Scale(connection.From.FlatPosition, scale);
            line.End = Vector3.Scale(connection.To.FlatPosition, scale);
        }
    }
}
