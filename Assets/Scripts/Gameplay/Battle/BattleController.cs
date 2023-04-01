using System;
using System.Collections.Generic;
using Fsi.Runtime;
using ProjectMaster.Battle.Actions;
using ProjectMaster.Battle.Ui;
using ProjectMaster.Data.Attacks;
using ProjectMaster.Data.Pokemon.Types;
using ProjectMaster.Data.Trainers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster.Gameplay.Battle
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

        protected TrainerBattleUi battleUi;
        
        [Title("Player Trainer")]
        
        [SerializeField]
        private BattleTrainer playerTrainerPrefab;

        [SerializeField]
        private Transform playerTrainerSocket;
        
        [SerializeField]
        private Transform playerPokemonSocket;

        [SerializeField]
        private TrainerData playerTrainerData;

        protected BattleTrainer playerTrainer;

        protected readonly List<BattleAction> turnActions = new List<BattleAction>();

        public BattlePokemon PlayerPokemon => playerTrainer.CurrentPokemon;
        
        public abstract BattlePokemon EnemyPokemon { get; }

        private void Start()
        {
            battleUi = TrainerBattleUi.Instance;
            
            playerTrainer = Instantiate(playerTrainerPrefab, playerTrainerSocket);
            playerTrainer.Init(playerTrainerData, playerPokemonSocket);
            
            StartStartPhase();
        }

        #region Start Phase
        
        protected abstract void StartStartPhase();
        
        #endregion
        
        #region Action Phase
        
        protected void StartActionPhase()
        {
            // Get player action
            // Get enemy action
            // sort actions

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
            
            if (action is AttackAction attack)
            {
                UseAttack(attack);
            }
        }

        protected abstract void EvaluateField();

        #region Attack Action
        
        private void UseAttack(AttackAction action)
        {
            Debug.Log("Using attack");
            StartUseAttack(action.Attack, action.Attacker, action.Target);
        }

        private void StartUseAttack(Attack attack, BattlePokemon attacker, BattlePokemon target)
        {
            battleUi.UseAttack(attacker.Name, attack.Name, () =>
            {
                Effectiveness effectiveness =
                    GameplayController.Instance.TypeChart.GetEffectiveness(attack.Type, target.Type);

                int damage = attack.Power;

                switch (effectiveness)
                {
                    case Effectiveness.NoEffect:
                        battleUi.NoEffect(EndUseAttack);
                        break;

                    case Effectiveness.NotVeryEffective:
                        target.Damage(damage / 2);
                        battleUi.NotVeryEffective(EndUseAttack);
                        break;

                    case Effectiveness.Effective:
                        target.Damage(damage);
                        EndUseAttack();
                        break;

                    case Effectiveness.SuperEffective:
                        target.Damage(damage * 2);
                        battleUi.SuperEffective(EndUseAttack);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }

        private void EndUseAttack()
        {
            Debug.Log("Finished attack");
            EvaluateField();
        }

        #endregion
        
        #endregion

        #region End Phase

        protected void StartEndPhase()
        {
            
        }
        
        #endregion
        

        protected virtual void OnDrawGizmos()
        {
            Vector3 positionExtends = new (1, 0.2f, 1);
            
            if (playerTrainerSocket)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(playerTrainerSocket.position, positionExtends);
            }
            
            if (playerPokemonSocket)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(playerPokemonSocket.position, positionExtends/2);
            }
        }
    }
}
