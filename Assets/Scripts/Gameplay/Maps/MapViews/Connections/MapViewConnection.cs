using ProjectCatch.Gameplay.Maps.MapViews.Nodes;
using Shapes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps.MapViews.Connections
{
    public class MapViewConnection : MonoBehaviour
    {
        private MapConnection connection;
        private MapViewNode from;
        private MapViewNode to;
        
        [Title("Map View Connection")]

        [SerializeField]
        public Line line;

        private bool highlight = true;

        [SerializeField]
        private float speed = 1f;

        public void Initialize(MapViewNode from, MapViewNode to, MapConnection connection)
        {
            this.connection = connection;

            line.Start = Vector3.zero;
            line.End = connection.To.WorldPosition - connection.From.WorldPosition;

            this.from = from;
            this.to = to;
        }

        public void SetCurrent(bool set)
        {
            line.Dashed = set;
            line.DashOffset = 0;
            highlight = set;

            if (set)
            {
                to.StartNextBounce();
            }
            else
            {
                to.StopNextBounce();
            }
        }

        private void Update()
        {
            if (highlight)
            {
                line.DashOffset += speed * Time.deltaTime;
            }
        }
    }
}
