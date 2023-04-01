using System.Collections.Generic;
using ProjectMaster.Data.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster.Data.Trainers
{
    [CreateAssetMenu(menuName = "Trainer", fileName = "New Trainer")]
    public class TrainerData : ScriptableObject
    {
        [Title("Information")]

        [SerializeField]
        private new string name = "Trainer";

        public string Name => name;

        [Title("Visuals")]

        [SerializeField]
        private GameObject model;

        public GameObject Model => model;

        [Title("Party")]

        [SerializeField]
        private List<PokemonData> partyData;

        public List<PokemonData> PartyData => partyData;

        [SerializeField]
        private int pokemonLevel = 10;

        public int PokemonLevel => pokemonLevel;
    }
}
