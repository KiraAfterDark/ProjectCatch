using System.Collections.Generic;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Gameplay.Battle.Initializer;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Battle.TrainerBattle
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
        
        private TrainerInstance enemyTrainerInstance;
        private BattleTrainer enemyTrainer;

        public override BattlePokemon EnemyPokemon => enemyTrainer.CurrentPokemon;

        #region Start Phase
        protected override void InitEnemy(BattleInitializer initializer)
        {
            if (initializer is TrainerBattleInitializer trainerInitializer)
            {
                enemyTrainerInstance = trainerInitializer.EnemyTrainerInstance;
            }
        }

        protected override void StartStartPhase()
        {
            base.StartStartPhase();
            
            enemyTrainer = Instantiate(enemyTrainerPrefab, enemyTrainerSocket);
            enemyTrainer.Init(enemyTrainerInstance, enemyPokemonSocket);

            battleUi.StartBattle(enemyTrainer.Name, () =>
            {
                enemyTrainer.StartBattle(() =>
                {
                    playerTrainer.StartBattle(StartActionPhase);
                });
            });
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
                playerTrainer.PokemonFainted(PlayerPokemonFainted);
            }
            else if (enemyTrainer.CurrentPokemon.Health.Fainted)
            {
                enemyTrainer.PokemonFainted(EnemyPokemonFainted);
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

        private void EnemyPokemonFainted()
        {
            if (enemyTrainer.HasRemainingPokemon)
            {
                Debug.Log("Enemy selecting pokmeon");
                enemyTrainer.SelectPokemon(OnEnemySelectPokemon, null);
            }
            else
            {
                PlayerWin();
            }
        }

        private void OnEnemySelectPokemon(PokemonInstance instance)
        {
            enemyTrainer.UsePokemon(instance, EnemyUsePokemon);
        }

        private void EnemyUsePokemon()
        {
            Debug.Log($"Enemy used: {enemyTrainer.CurrentPokemon.Name} - Level {enemyTrainer.CurrentPokemon.Level}");
            EvaluateField();
        }
        
        #endregion

        protected override void PlayerLose()
        {
            Debug.Log("Player Lose");
            battleUi.TrainerWin(enemyTrainer, () => { });
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Vector3 positionExtends = new (1, 0.2f, 1);
            
            if (enemyTrainerSocket)
            {
                Vector3 position = enemyTrainerSocket.position;
                Vector3 forward = enemyTrainerSocket.forward;
                
                Gizmos.color = Color.red;
                Gizmos.DrawCube(position, positionExtends);
                Gizmos.DrawRay(position, forward * 1);
                Gizmos.DrawSphere(position + forward * 1, 0.2f);
            }
            
            if (enemyPokemonSocket)
            {
                Vector3 position = enemyPokemonSocket.position;
                Vector3 forward = enemyPokemonSocket.forward;
                
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(position, positionExtends/2);
                Gizmos.DrawRay(position, forward * 1);
                Gizmos.DrawSphere(position + forward * 1, 0.1f);
            }
        }
    }
}
