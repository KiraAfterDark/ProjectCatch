using System;
using System.Collections;
using Fsi.Runtime;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Gameplay.Battle.Ui;
using ProjectCatch.Gameplay.Pokemon.Types;
using ProjectCatch.Ui;
using ProjectCatch.Input;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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

        [Min(0)]
        [SerializeField]
        private float introduceTrainerTime = 1f;

        [Title("Pokemon")]

        [SerializeField]
        private string usePokemonString = "{0} uses {1}!";

        [Min(0)]
        [SerializeField]
        private float usePokemonTime = 1f;

        [SerializeField]
        private string pokemonFaintedString = "{0} fainted!";

        [SerializeField]
        private string pokemonSwitchOut = "{0} switches out {1}.";

        [Min(0)]
        [SerializeField]
        private float switchOutTime = 1.5f;

        [Title("Attack Strings")]

        [SerializeField]
        private string useAttackString = "{0} uses {1}.";

        [Min(0)]
        [SerializeField]
        private float useAttackTime = 1f;

        [SerializeField]
        private string superEffectiveString = "It's super effective!";

        [SerializeField]
        private string notVeryEffectiveString = "It's not very effective.";

        [SerializeField]
        private string noEffectString = "It had no effect.";

        [Min(0)]
        [SerializeField]
        private float effectivenessTime = 1f;

        [Title("Finished Battle Strings")]

        [SerializeField]
        private string trainerWinString = "{0} wins the battle.";

        [FormerlySerializedAs("playerHealthbar")]
        [Title("Health bars")]

        [SerializeField]
        private PokemonBattleInfo playerInfo;

        [FormerlySerializedAs("enemyHealthbar")]
        [SerializeField]
        private PokemonBattleInfo enemyInfo;

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

            playerInput.Battle.Submit.performed += ctx => Confirm();
        }

        private void OnEnable()
        {
            playerInput.Battle.Enable();
        }

        private void OnDisable()
        {
            playerInput.Battle.Disable();
        }

        private IEnumerator ShowBattleTextInput(string display, Action callback, params string[] args)
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

        private IEnumerator ShowBattleTextTimed(string display, float time, Action callback, params string[] args)
        {
            battleTextCanvas.gameObject.SetActive(true);
            string formatted = string.Format(display, args);
            textBox.Display(formatted);

            waitingForConfirm = false;
            hasConfirmed = false;
            yield return new WaitForSeconds(time);
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
            StartCoroutine(ShowBattleTextTimed(introduceEnemyString, introduceTrainerTime, callback, trainer));
        }

        #endregion
        
        #region Pokemon

        public void UsePokemon(string trainer, string pokemon)
        {
            StartCoroutine(ShowBattleTextTimed(usePokemonString, usePokemonTime, null, trainer, pokemon));
        }

        public void PokemonFainted(string pokemon)
        {
            StartCoroutine(ShowBattleTextInput(pokemonFaintedString, null, pokemon));
        }

        public void SwitchOut(string pokemon, string trainer)
        {
            StartCoroutine(ShowBattleTextTimed(pokemonSwitchOut, switchOutTime, null, pokemon, trainer));
        }

        #endregion
        
        #region Player Trainer Action Select

        public void PlayerActionSelect(BattleTrainer trainer, Action<BattleAction> actionSelectedCallback)
        {
            actionSelectCanvas.gameObject.SetActive(true);
            actionSelect.Init(trainer, battleAction =>
            {
                actionSelectCanvas.gameObject.SetActive(false);
                PlayerActionSelected(battleAction, actionSelectedCallback);
            });
        }

        public void PlayerActionSelected(BattleAction battleAction, Action<BattleAction> actionSelectedCallback)
        {
            actionSelectedCallback?.Invoke(battleAction);
        }
        
        #endregion
        
        #region Attacking

        public void UseAttack(string pokemon, string attack)
        {
            StartCoroutine(ShowBattleTextTimed(useAttackString, useAttackTime, null, pokemon, attack));
        }

        public void ShowEffectiveness(Effectiveness effectiveness)
        {
            switch (effectiveness)
            {
                case Effectiveness.NoEffect:
                    StartCoroutine(ShowBattleTextTimed(noEffectString, useAttackTime, null));
                    break;

                case Effectiveness.NotVeryEffective:
                    StartCoroutine(ShowBattleTextTimed(notVeryEffectiveString, useAttackTime, null));
                    break;

                case Effectiveness.Effective:
                    break;

                case Effectiveness.SuperEffective:
                    StartCoroutine(ShowBattleTextTimed(superEffectiveString, useAttackTime, null));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(effectiveness), effectiveness, null);
            }
        }

        #endregion
        
        #region Finished Battle

        public void TrainerWin(BattleTrainer trainer, Action callback)
        {
            StartCoroutine(ShowBattleTextInput(trainerWinString, callback, trainer.Name));
        }
        
        #endregion
        
        #region Healthbars

        public void ShowPlayerHealth(BattlePokemon pokemon)
        {
            playerHealthCanvas.gameObject.SetActive(true);
            playerInfo.Init(pokemon);
        }

        public void ShowEnemyHealth(BattlePokemon pokemon)
        {
            enemyHealthCanvas.gameObject.SetActive(true);
            enemyInfo.Init(pokemon);
        }

        public void HideHealthbar(PokemonBattleInfo info)
        {
            if (info == enemyInfo)
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
        
        #endregion
    }
}
