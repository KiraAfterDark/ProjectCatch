using System;
using System.Collections;
using System.Collections.Generic;
using Fsi.Runtime;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Gameplay.Battle.Ui;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Input;
using ProjectCatch.Ui;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Battle.Ui
{
    public class BattleUi : MbSingleton<BattleUi>
    {
        private PlayerInput playerInput;

        private bool waitingForConfirm = false;
        private bool hasConfirmed = false;

        [Title("Introduce Enemy Trainer")]

        [SerializeField]
        private string introduceEnemyString = "{0} would like to battle!";

        [Title("Use Pokemon")]

        [SerializeField]
        private string usePokemonString = "{0} uses {1}!";

        [Title("Attack Strings")]

        [SerializeField]
        private string useAttackString = "{0} uses {1}.";

        [SerializeField]
        private string superEffectiveString = "It's super effective!";

        [SerializeField]
        private string notVeryEffectiveString = "It's not very effective.";

        [SerializeField]
        private string noEffectString = "It had no effect.";

        [Title("Finished Battle Strings")]

        [SerializeField]
        private string trainerWinString = "{0} wins the battle.";

        [Title("Health bars")]

        [SerializeField]
        private BattleHealthbar playerHealthbar;

        [SerializeField]
        private BattleHealthbar enemyHealthbar;

        [Title("Canvas Groups")]

        [SerializeField]
        private Canvas actionSelectCanvas;

        [SerializeField]
        private Canvas battleTextCanvas;

        [SerializeField]
        private Canvas playerHealthCanvas;

        [SerializeField]
        private Canvas enemyHealthCanvas;

        [Title("Hookups")]

        [SerializeField]
        private TextBox textBox;

        [SerializeField]
        private ActionSelect actionSelect;

        protected override void OnAwake()
        {
            playerInput = new PlayerInput();

            playerInput.Battle.Confirm.performed += ctx => Confirm();
        }

        private void OnEnable()
        {
            playerInput.Battle.Enable();
        }

        private void OnDisable()
        {
            playerInput.Battle.Disable();
        }

        private IEnumerator ShowBattleText(string display, Action callback, params string[] args)
        {
            battleTextCanvas.gameObject.SetActive(true);
            string formatted = string.Format(display, args);
            textBox.Display(formatted);

            waitingForConfirm = true;
            hasConfirmed = false;
            yield return new WaitWhile(() => !hasConfirmed);
            waitingForConfirm = false;
            hasConfirmed = false;
            
            textBox.Clear();
            battleTextCanvas.gameObject.SetActive(false);
            callback?.Invoke();
        }
        
        #region Input

        private void Confirm()
        {
            if (waitingForConfirm)
            {
                hasConfirmed = true;
            }
        }
        
        #endregion

        #region Start Battle
        
        public void StartBattle(string trainer, Action callback)
        {
            StartCoroutine(ShowBattleText(introduceEnemyString, callback, trainer));
        }

        #endregion
        
        #region Use Pokemon

        public void UsePokemon(string trainer, string pokemon, Action callback)
        {
            StartCoroutine(ShowBattleText(usePokemonString, callback, trainer, pokemon));
        }

        #endregion
        
        #region Player Trainer Action Select

        public void PlayerActionSelect(BattleTrainer trainer, Action<BattleAction> callback)
        {
            actionSelectCanvas.gameObject.SetActive(true);
            actionSelect.Init(trainer, battleAction =>
            {
                actionSelectCanvas.gameObject.SetActive(false);
                PlayerActionSelected(battleAction, callback);
            });
        }

        public void PlayerActionSelected(BattleAction battleAction, Action<BattleAction> callback)
        {
            callback?.Invoke(battleAction);
        }
        
        #endregion
        
        #region Attacking

        public void UseAttack(string pokemon, string attack, Action callback)
        {
            StartCoroutine(ShowBattleText(useAttackString, callback, pokemon, attack));
        }

        public void SuperEffective(Action callback)
        {
            StartCoroutine(ShowBattleText(superEffectiveString, callback));
        }

        public void NotVeryEffective(Action callback)
        {
            StartCoroutine(ShowBattleText(notVeryEffectiveString, callback));
        }

        public void NoEffect(Action callback)
        {
            StartCoroutine(ShowBattleText(noEffectString, callback));
        }
        
        #endregion
        
        #region Finished Battle

        public void TrainerWin(BattleTrainer trainer, Action callback)
        {
            StartCoroutine(ShowBattleText(trainerWinString, callback, trainer.Name));
        }
        
        #endregion
        
        #region Healthbars

        public void ShowPlayerHealth(BattlePokemon pokemon)
        {
            playerHealthCanvas.gameObject.SetActive(true);
            playerHealthbar.Init(pokemon);
        }

        public void ShowEnemyHealth(BattlePokemon pokemon)
        {
            enemyHealthCanvas.gameObject.SetActive(true);
            enemyHealthbar.Init(pokemon);
        }

        public void HideHealthbar(BattleHealthbar healthbar)
        {
            if (healthbar == enemyHealthbar)
            {
                enemyHealthCanvas.gameObject.SetActive(false);
            }
            else
            {
                playerHealthCanvas.gameObject.SetActive(false);
            }
        }
        
        #endregion
        
        #region PokemonSelect

        public void SelectPokemon(List<PokemonInstance> party, Action<PokemonInstance> callback)
        {
            
        }
        
        #endregion
    }
}
