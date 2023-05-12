using System;
using System.Collections;
using System.Collections.Generic;
using Fsi.Runtime;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Battle.Initializer;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Gameplay.Pokemon.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Battle
{
    
    /// <summary>
    /// Battles consist of multiple phases
    /// 1. Start Phase
    /// 2. Select Phase
    /// 3. Execute Phase (Execute action and evaluate field repeating until no actions remain)
    /// 4. End Phase
    /// </summary>
    public abstract class BattleController : MbSingleton<BattleController>
    {
        [Title("BattleUi")]

        protected BattleUi battleUi;

        [Title("Player Trainer")]

        [SerializeField]
        private BattleTrainer playerTrainerPrefab;

        [SerializeField]
        private Transform playerTrainerSocket;
        
        [SerializeField]
        private Transform playerPokemonSocket;

        [SerializeField]
        private TrainerInstanceData playerTrainerData;
        
        private TrainerInstance playerTrainerInstance;
        protected BattleTrainer playerTrainer;
        
        public BattlePokemon PlayerPokemon => playerTrainer.CurrentPokemon;
        
        public abstract BattlePokemon EnemyPokemon { get; }

        // Actions
        protected readonly List<BattleAction> turnActions = new List<BattleAction>();
        
        private void Start()
        {
            battleUi = BattleUi.Instance;

            if (GameplayController.Instance.TryGetBattleInitializer(out BattleInitializer battleInitializer))
            {
                playerTrainerInstance = battleInitializer.PlayerTrainerInstance;
                InitEnemy(battleInitializer);
                StartStartPhase();
            }
        }
        
        protected abstract void InitEnemy(BattleInitializer initializer);

        #region Start Phase

        protected virtual void StartStartPhase()
        {
            playerTrainer = Instantiate(playerTrainerPrefab, playerTrainerSocket);
            playerTrainer.Init(playerTrainerInstance, playerPokemonSocket);
        }
        
        #endregion
        
        #region Action Phase
        
        protected void StartActionPhase()
        {
            // Get player action
            // Get enemy action
            // sort actions

            Debug.Log("Start Action Phase");
            playerTrainer.SelectAction(ReceivePlayerAction);
        }

        private void ReceivePlayerAction(BattleAction playerAction)
        {
            turnActions.Add(playerAction);
            EnemySelectAction();
        }

        protected abstract void EnemySelectAction();
        
        #endregion
        
        #region Resolve Phase

        protected void StartResolvePhase()
        {
            Debug.Log("Start Execute Phase");

            BattleAction action = turnActions[0];
            
            ResolveAction(action);
        }

        protected void ResolveAction(BattleAction action)
        {
            Debug.Log("Evaluating next action");

            if (!turnActions.Remove(action))
            {
                Debug.LogError("Trying to execute action that isn't in turn actions.");
                return;
            }

            turnActions.Remove(action);
            action.Resolve(EvaluateField);
        }

        protected abstract void EvaluateField();

        #region Evaluate Field
        
        protected void PlayerPokemonFainted()
        {
            if (playerTrainer.HasRemainingPokemon)
            {
                playerTrainer.SelectPokemon(OnPlayerSelectPokemon, null);
            }
            else
            {
                PlayerLose();
            }
        }

        private void OnPlayerSelectPokemon(PokemonInstance instance)
        {
            playerTrainer.UsePokemon(instance, OnPlayerUsePokemon);
        }

        private void OnPlayerUsePokemon()
        {
            Debug.Log($"Player used: {playerTrainer.CurrentPokemon.Name} - Level {playerTrainer.CurrentPokemon.Level}");
            EvaluateField();
        }

        #endregion
        
        #endregion

        #region End Phase

        protected void StartEndPhase()
        {
            
        }

        protected void PlayerWin()
        {
            Debug.Log("Player win!");
            battleUi.TrainerWin(playerTrainer, () => { });
        }

        protected abstract void PlayerLose();

        #endregion
        

        protected virtual void OnDrawGizmos()
        {
            Vector3 positionExtends = new (1, 0.2f, 1);
            
            if (playerTrainerSocket)
            {
                Vector3 position = playerTrainerSocket.position;
                Vector3 forward = playerTrainerSocket.forward;
                
                Gizmos.color = Color.green;
                Gizmos.DrawCube(position, positionExtends);
                Gizmos.DrawRay(position, forward * 1);
                Gizmos.DrawSphere(position + forward * 1, 0.2f);
            }
            
            if (playerPokemonSocket)
            {
                Vector3 position = playerPokemonSocket.position;
                Vector3 forward = playerPokemonSocket.forward;
                
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(position, positionExtends/2);
                Gizmos.DrawRay(position, forward * 1);
                Gizmos.DrawSphere(position + forward * 1, 0.1f);
            }
        }
    }
}
