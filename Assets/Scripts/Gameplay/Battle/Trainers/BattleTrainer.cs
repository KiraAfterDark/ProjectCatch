using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;
using ProjectCatch.Data.Pokemon;
using ProjectCatch.Data.Trainers;
using ProjectCatch.Gameplay.Battle;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch
{
    public abstract class BattleTrainer : MonoBehaviour
    {
        protected TrainerData data;

        public string Name => data.Name;
        
        protected BattlePokemon currentPokemon;

        public BattlePokemon CurrentPokemon => currentPokemon;

        private GameObject model;

        protected BattleUi battleUi;
        protected BattleController battleController;

        protected List<PokemonInstance> party;

        public bool HasRemainingPokemon => party.Count > 0;

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

            battleUi = BattleUi.Instance;
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

        public void SelectPokemon(Action<PokemonInstance> callback)
        {
            battleUi.SelectPokemon(party, callback);
        }
    }
}
