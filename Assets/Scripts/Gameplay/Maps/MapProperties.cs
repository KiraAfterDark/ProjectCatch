using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Maps
{
    [CreateAssetMenu(fileName = "New Map Properties", menuName = "Map/Properties", order = 0)]
    public class MapProperties : ScriptableObject
    {
        [Title("Map Properties")]

        [SerializeField]
        private Vector2Int size;

        public Vector2Int Size => size;

        [SerializeField]
        private int lines = 2;

        public int Lines => lines;

        [Range(0, 1)]
        [SerializeField]
        private float splitChance = 0;

        public float SplitChance => splitChance;

        [Min(0)]
        [SerializeField]
        private int splitAttempts = 1;

        public int SplitAttempts => splitAttempts;

        [Min(0)]
        [SerializeField]
        private int incrementMax = 2;

        public int IncrementMax => incrementMax;

        [Title("Map Visuals")]
        [SerializeField]
        private GameObject nodePrefab;

        public GameObject NodePrefab => nodePrefab;

        [SerializeField]
        private LineRenderer lineRendererPrefab;

        public LineRenderer LineRendererPrefab => lineRendererPrefab;

        [SerializeField]
        private Vector2 spacingMod = Vector2.one;

        public Vector2 SpacingMod => spacingMod;
    }
}
