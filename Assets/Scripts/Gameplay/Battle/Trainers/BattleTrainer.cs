using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;
using ProjectCatch.Gameplay;
using ProjectCatch.Gameplay.Battle;
using ProjectCatch.Gameplay.Items;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch
{
    public abstract class BattleTrainer : MonoBehaviour
    {
        protected TrainerInstance instance;

        public string Name => instance.Name;
        
        protected BattlePokemon currentPokemon;

        public BattlePokemon CurrentPokemon => currentPokemon;

        private GameObject model;

        protected BattleUi battleUi;
        protected BattleController battleController;
        
        public List<PokemonInstance> Party => instance.Party;

        public bool HasRemainingPokemon => Party.Count > 0;

        [Title("Prefabs")]
        
        [SerializeField]
        protected BattlePokemon battlePokemonPrefab;

        [Title("Sockets")]

        [SerializeField]
        private Transform modelSocket;
        
        protected Transform pokemonSocket;

        public void Init(TrainerInstance instance, Transform pokemonSocket)
        {
            // TODO Make this work with instance also make trainer instance
            
            this.instance = instance;
            this.pokemonSocket = pokemonSocket;

            model = Instantiate(this.instance.Model, modelSocket);

            battleUi = BattleUi.Instance;
            battleController = BattleController.Instance;
        }

        public void StartBattle(Action callback)
        {
            UsePokemon(Party[0], callback);
        }

        public abstract void UsePokemon(PokemonInstance pokemon, Action callback);

        public abstract void SelectAction(Action<BattleAction> actionSelectCallback);

        public abstract void SelectPokemon(Action<PokemonInstance> callback, Action cancelCallback);

        public void PokemonFainted(Action callback)
        {
            StartCoroutine(PokemonFaintedSequence(callback));
        }

        private IEnumerator PokemonFaintedSequence(Action callback)
        {
            BattlePokemon pokemon = currentPokemon;
            Party.Remove(pokemon.Instance);
            pokemon.Faint();
            BattleUi.Instance.PokemonFainted(Name);

            yield return new WaitForSeconds(2f);

            callback?.Invoke();
        }

        public void SwitchPokemon(PokemonInstance switchTo, Action callback)
        {
            StartCoroutine(SwitchPokemonSequence(switchTo, callback));
        }

        private IEnumerator SwitchPokemonSequence(PokemonInstance switchTo, Action callback)
        {
            currentPokemon.SwitchOut();
            battleUi.SwitchOut(currentPokemon.Name, Name);

            yield return new WaitForSeconds(1.5f);
            
            UsePokemon(switchTo, callback);
        }

        public Inventory GetInventory()
        {
            return instance.Inventory;
        }
    }
}
