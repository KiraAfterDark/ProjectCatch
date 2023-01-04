using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.NPCs.Trainers
{
    [CreateAssetMenu(fileName = "New Trainer Base", menuName = "Project Catch/Trainers/Trainer")]
    public class TrainerBase : ScriptableObject
    {
        [Title("Data")]

        [SerializeField]
        private new string name;

        public string Name => name;

        [SerializeField]
        private TrainerObject trainerObject;
    }
}
