using ProjectCatch.Data.Pokemon.Types;
using UnityEngine;

namespace ProjectCatch.Data.Attacks
{
    [CreateAssetMenu(menuName = "Attack", fileName = "New Attack")]
    public class Attack : ScriptableObject
    {
        [SerializeField]
        private new string name;

        public string Name => name;

        [SerializeField]
        private string description;

        public string Description => description;

        [Min(0)]
        [SerializeField]
        private int power = 50;

        public int Power => power;
        
        [SerializeField]
        private Type type;

        public Type Type => type;

        [SerializeField]
        private AttackCategory category;

        public AttackCategory Category => category;

    }

    public enum AttackCategory
    {
        Physical = 0,
        Special = 1,
    }
}
