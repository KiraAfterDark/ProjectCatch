using System;
using System.Collections.Generic;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Battle;
using ProjectCatch.Gameplay.Items;
using ProjectCatch.Gameplay.Items.Ui;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Gameplay.Ui;
using ProjectCatch.Ui;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectCatch
{
    public class ActionSelect : MonoBehaviour
    {
        private bool hasAction = false;
        
        private Action<BattleAction> callback;

        private BattleTrainer battleTrainer;

        [Title("Buttons")]

        [SerializeField]
        private GameObject initSelectedButton;

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

            EventSystem.current.SetSelectedGameObject(initSelectedButton);
        }
        
        #region Attack
        
        public void SelectAttack()
        {
            Debug.Log("Selecting Attack");
            actionSelectGroup.gameObject.SetActive(false);
            attackButtonGroup.gameObject.SetActive(true);
            
            EventSystem.current.SetSelectedGameObject(attackButtons[0].gameObject);
            EventSystem.current.sendNavigationEvents = true;
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
        
        #endregion
        
        #region Switch

        public void SelectSwitch()
        {
            Debug.Log("Selected Switch");
            actionSelectGroup.gameObject.SetActive(false);

            PartyManagerScreen.Instance.RequestPokemon(battleTrainer.Party, battleTrainer.CurrentPokemon, OnSwitchPokemonSelected, OnCancelSwitchPokemon);
        }

        private void OnSwitchPokemonSelected(PokemonInstance switchTo)
        {
            Debug.Log($"{battleTrainer.Name} switching {battleTrainer.CurrentPokemon.Name} for {switchTo.Name}");
            SwapAction swap = new (battleTrainer, switchTo);
            callback?.Invoke(swap);
        }

        private void OnCancelSwitchPokemon()
        {
            actionSelectGroup.gameObject.SetActive(true);
        }
        
        #endregion

        #region Item

        public void SelectItemAction()
        {
            actionSelectGroup.gameObject.SetActive(false);
            BagScreen.Instance.RequestItem(battleTrainer.GetInventory(), SelectItem, CancelSelectItem);
        }

        private void SelectItem(Item item)
        {
            actionSelectGroup.gameObject.SetActive(false);
            
            PartyManagerScreen.Instance.RequestPokemon(battleTrainer.Party, 
                                                       null, 
                                                       (pokemonInstance) => 
                                                       {
                                                           SelectItemTarget(item, pokemonInstance);
                                                       }, 
                                                       CancelSelectItem);
        }

        private void CancelSelectItem()
        {
            actionSelectGroup.gameObject.SetActive(true);
        }

        private void SelectItemTarget(Item item, PokemonInstance target)
        {
            ItemAction itemAction = new ItemAction(item, target, battleTrainer.CurrentPokemon);
            callback.Invoke(itemAction);
        }
        
        #endregion
    }
}
