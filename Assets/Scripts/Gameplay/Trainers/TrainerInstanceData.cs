using System.Collections.Generic;
using ProjectCatch.Gameplay.Items;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectCatch.Gameplay
{
    [CreateAssetMenu(menuName = "Trainer/Instance Data", fileName = "New Trainer Instance Data")]
    public class TrainerInstanceData : SerializedScriptableObject
    {
        [Title("Information")]

        [SerializeField]
        private new string name = "";

        public string Name => name;
        
        [Title("Visuals")]

        [SerializeField]
        private GameObject model;

        public GameObject Model => model;

        [Title("Party")]
        
        [SerializeField]
        private List<PokemonInstanceData> partyInstance = new List<PokemonInstanceData>();

        public List<PokemonInstanceData> PartyInstance => partyInstance;

        [Title("Inventory")]

        [SerializeField]
        private SerializedDictionary<Item, int> inventory = new SerializedDictionary<Item, int>();

        public SerializedDictionary<Item, int> Inventory => inventory;
    }
}
