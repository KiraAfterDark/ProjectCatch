using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Mons.Data
{
    [CreateAssetMenu(fileName = "New Type Chart", menuName = "Project Catch/Config/Type Chart")]
    public class TypeChart : SerializedScriptableObject
    {
        [TableMatrix(SquareCells = true, VerticalTitle = "Defender", HorizontalTitle = "Attacker")]
        [SerializeField]
        private float[,] typeChart = new float[18, 18];

        public float GetTypeMod(MonType attackType, MonType defenderType)
        {
            return typeChart[(int)attackType, (int)defenderType];
        }
    }
}
