using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.TrainerBuilders
{
    public class TrainerBuilder : MonoBehaviour
    {
        [Title("Trainer Visuals")]

        [SerializeField]
        private List<TrainerBuilderAsset> trainerAssets = new List<TrainerBuilderAsset>();

        private void Start()
        {
            
        }
    }
}
