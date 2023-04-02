using System;
using System.Collections.Generic;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Battle;
using ProjectCatch.Ui;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch
{
    public class ActionSelect : MonoBehaviour
    {
        private bool hasAction = false;
        
        private Action<BattleAction> callback;

        private BattleTrainer battleTrainer;

        [Title("Groups")]

        [SerializeField]
        private GameObject actionSelectGroup;

        [SerializeField]
        private GameObject attackButtonGroup;

        [Title("Containers")]

        [SerializeField]
        private List<AttackButton> attackButtons = new List<AttackButton>();

        public void Init(BattleTrainer battleTrainer, Action<BattleAction> callback)
        {
            hasAction = false;
            this.callback = callback;

            this.battleTrainer = battleTrainer;

            for (int i = 0; i < attackButtons.Count; i++)
            {
                if (i >= battleTrainer.CurrentPokemon.Attacks.Count)
                {
                    attackButtons[i].gameObject.SetActive(false);
                }
                else
                {
                    attackButtons[i].gameObject.SetActive(true);
                    attackButtons[i].Init(battleTrainer.CurrentPokemon.Attacks[i], OnAttackSelected);
                }
            }

            attackButtonGroup.SetActive(false);
            actionSelectGroup.SetActive(true);
        }
        
        public void SelectAttack()
        {
            Debug.Log("Selecting Attack");
            actionSelectGroup.gameObject.SetActive(false);
            attackButtonGroup.gameObject.SetActive(true);
        }

        private void OnAttackSelected(Attack attack)
        {
            if (hasAction)
            {
                return;
            }

            hasAction = false;
            
            Debug.Log($"Selected Attack: {attack.Name}");
            
            AttackAction attackAction = new(attack, 
                                            BattleController.Instance.PlayerPokemon, 
                                            BattleController.Instance.EnemyPokemon);
            
            callback?.Invoke(attackAction);
        }
    }
}
