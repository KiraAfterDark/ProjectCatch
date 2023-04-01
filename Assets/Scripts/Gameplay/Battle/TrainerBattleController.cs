using System.Collections.Generic;
using ProjectMaster.Battle.Actions;
using ProjectMaster.Data.Trainers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster.Gameplay.Battle.TrainerBattle
{
    public class TrainerBattleController : BattleController
    {
        [Title("Enemy Trainer")]
        
        [SerializeField]
        private BattleTrainer enemyTrainerPrefab;
        
        [SerializeField]
        private Transform enemyTrainerSocket;

        [SerializeField]
        private Transform enemyPokemonSocket;

        [SerializeField]
        private TrainerData enemyTrainerData;

        private BattleTrainer enemyTrainer;

        public override BattlePokemon EnemyPokemon => enemyTrainer.CurrentPokemon;

        #region Start Phase
        
        protected override void StartStartPhase()
        {
            enemyTrainer = Instantiate(enemyTrainerPrefab, enemyTrainerSocket);
            enemyTrainer.Init(enemyTrainerData, enemyPokemonSocket);
            
            StartBattle();
        }

        private void StartBattle()
        {
            Debug.Log("Start Trainer Battle");
            battleUi.StartBattle(enemyTrainer.Name, EnemyStartBattle);
        }

        private void EnemyStartBattle()
        {
            Debug.Log("Enemy Start");
            enemyTrainer.StartBattle(PlayerStartBattle);
        }

        private void PlayerStartBattle()
        {
            Debug.Log("Player Start");
            playerTrainer.StartBattle(StartActionPhase);
        }
        
        #endregion
        
        #region Select Phase

        protected override void EnemySelectAction()
        {
            enemyTrainer.SelectAction(OnEnemyActionSelect);
        }

        private void OnEnemyActionSelect(BattleAction battleAction)
        {
            turnActions.Add(battleAction);
            StartResolvePhase();
        }
        
        #endregion

        #region Evaluate Field
        
        protected override void EvaluateField()
        {
            Debug.Log("Evaluating field");

            List<BattleAction> remainingActions = new(turnActions);
            foreach (BattleAction action in remainingActions)
            {
                if (action.Source.Health.Fainted)
                {
                    turnActions.Remove(action);
                }
            }

            if (playerTrainer.CurrentPokemon.Health.Fainted)
            {
                playerTrainer.PokemonFainted();
                battleUi.TrainerWin(enemyTrainer, null);
                Debug.Log("Enemy Wins");
            }
            else if (enemyTrainer.CurrentPokemon.Health.Fainted)
            {
                enemyTrainer.PokemonFainted();
                battleUi.TrainerWin(playerTrainer, null);
                Debug.Log("Player Wins");
            }
            else if (turnActions.Count > 0)
            {
                ResolveAction(turnActions[0]);
            }
            else
            {
                StartActionPhase();
            }
        }

        private void PlayerNextPokemon()
        {
            
        }

        private void EnemyNextPokemon()
        {
            
        }
        
        #endregion

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Vector3 positionExtends = new (1, 0.2f, 1);
            
            if (enemyTrainerSocket)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(enemyTrainerSocket.position, positionExtends);
            }
            
            if (enemyPokemonSocket)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(enemyPokemonSocket.position, positionExtends/2);
            }
        }
    }
}
