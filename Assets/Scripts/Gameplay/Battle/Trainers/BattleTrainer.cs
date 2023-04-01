using System;
using System.Collections.Generic;
using ProjectMaster.Battle.Actions;
using ProjectMaster.Battle.Ui;
using ProjectMaster.Data.Pokemon;
using ProjectMaster.Data.Trainers;
using ProjectMaster.Gameplay.Battle;
using ProjectMaster.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster
{
    public abstract class BattleTrainer : MonoBehaviour
    {
        protected TrainerData data;

        public string Name => data.Name;
        
        protected BattlePokemon currentPokemon;

        public BattlePokemon CurrentPokemon => currentPokemon;

        private GameObject model;

        protected TrainerBattleUi battleUi;
        protected BattleController battleController;

        protected List<PokemonInstance> party;

        [Title("Prefabs")]
        
        [SerializeField]
        protected BattlePokemon battlePokemonPrefab;

        [Title("Sockets")]

        [SerializeField]
        private Transform modelSocket;
        
        protected Transform pokemonSocket;

        public void Init(TrainerData data, Transform pokemonSocket)
        {
            this.data = data;
            this.pokemonSocket = pokemonSocket;

            model = Instantiate(data.Model, modelSocket);

            battleUi = TrainerBattleUi.Instance;
            battleController = BattleController.Instance;

            party = new List<PokemonInstance>();
            foreach (PokemonData pokemonData in data.PartyData)
            {
                if (party.Count >= 6)
                {
                    Debug.LogWarning("Cannot add more than 6 pokemon to party.", gameObject);
                    break;
                }
                
                PokemonInstance instance = new (pokemonData, 10);
                party.Add(instance);
            }
        }

        public abstract void StartBattle(Action callback);

        public abstract void SelectAction(Action<BattleAction> callback);

        public void PokemonFainted()
        {
            Destroy(currentPokemon.gameObject);
        }
    }
}
