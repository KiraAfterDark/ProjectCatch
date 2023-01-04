using ProjectCatch.Mons.Data;
using UnityEngine;

namespace ProjectCatch.Mons.Attacks
{
    [CreateAssetMenu(fileName = "New Attack", menuName = "Project Catch/Attacks/Attack")]
    public class AttackReference : ScriptableObject
    {
        [SerializeField]
        private MonType type;

        public MonType Type => type;
    }
}
