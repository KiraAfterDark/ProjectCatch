using ProjectCatch.Gameplay.Maps.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.TrainerBuilders
{
    public class TrainerBuilderAsset : MonoBehaviour
    {
        [Title("Map Assets")]

        [Required]
        [SerializeField]
        private MapTrainer mapTrainer;

        public MapTrainer MapTrainer => mapTrainer;

        [Title("Battle Assets")]

        [Required]
        [SerializeField]
        private GameObject battleMesh;

        public GameObject BattleMesh => battleMesh;
    }
}
