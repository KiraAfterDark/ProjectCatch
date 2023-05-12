using System;
using System.Collections;
using System.Collections.Generic;
using ProjectCatch.Battle.Ui;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Gameplay.Pokemon.Types;
using ProjectCatch.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch
{
    public class BattlePokemon : MonoBehaviour
    {
        #region Data
        
        private PokemonInstance instance;

        public PokemonInstance Instance => instance;
        
        public string Name => instance.Name;

        public PokemonType Type => instance.PokemonType;
        
        #endregion
        
        public List<Attack> Attacks => instance.Attacks;
        
        public int Level => instance.Level;

        public Health Health => instance.Health;
    
        private PokemonModel model;

        [Title("Sockets")]

        [SerializeField]
        private Transform modelSocket;

        #region Animation Parameters
        
        private static readonly int AttackAnim = Animator.StringToHash("Attack");
        private static readonly int AttackIdAnim = Animator.StringToHash("AttackId");
        private static readonly int HitAnim = Animator.StringToHash("Hit");
        private static readonly int FaintAnim = Animator.StringToHash("Faint");
        private static readonly int SwitchOutAnim = Animator.StringToHash("SwitchOut");
        private static readonly int SwitchInAnim = Animator.StringToHash("SwitchIn");
        #endregion

        public bool Active { get; private set; }

        public void Init(PokemonInstance instance)
        {
            this.instance = instance;
            
            Debug.Log($"{Name} - Level: {Level} - Health: {Health.CurrentHealth}/{Health.MaxHealth}");

            if (model != null)
            {
                Destroy(model);
            }

            model = Instantiate(instance.Model, modelSocket);

            Active = true;
        }

        public void UseAttack(Attack attack, BattlePokemon target, Action finishAttackCallback)
        {
            StartCoroutine(AttackSequence(attack, target, finishAttackCallback));
        }

        private IEnumerator AttackSequence(Attack attack, BattlePokemon target, Action finishAttackCallback)
        {
            PokemonMath.DamageCalculation damageCalculation = PokemonMath.CalculateDamage(attack, instance, target.Instance);
            
            BattleUi.Instance.UseAttack(Name, attack.Name);

            yield return new WaitForSeconds(1f);
            
            model.Animator.SetTrigger(AttackAnim);
            model.Animator.SetInteger(AttackIdAnim, attack.ID);

            yield return new WaitForSeconds(0.5f);

            target.Damage(damageCalculation.damage);

            yield return new WaitForSeconds(0.5f);

            switch (damageCalculation.effectiveness)
            {
                case Effectiveness.NoEffect:
                    BattleUi.Instance.ShowEffectiveness(damageCalculation.effectiveness);
                    yield return new WaitForSeconds(1.0f);
                    break;

                case Effectiveness.NotVeryEffective:
                    BattleUi.Instance.ShowEffectiveness(damageCalculation.effectiveness);
                    yield return new WaitForSeconds(1.0f);
                    break;

                case Effectiveness.Effective:
                    break;
                
                case Effectiveness.SuperEffective:
                    BattleUi.Instance.ShowEffectiveness(damageCalculation.effectiveness);
                    yield return new WaitForSeconds(1.0f);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            finishAttackCallback?.Invoke();
        }
        
        public void Damage(int amount)
        {
            Health.Damage(amount);
            model.Animator.SetTrigger(HitAnim);
        }

        public void SwitchOut()
        {
            model.Animator.SetTrigger(SwitchOutAnim);
            Active = false;
        }

        public void SwitchIn()
        {
            model.Animator.SetTrigger(SwitchInAnim);
            Active = true;
        }

        // Perform the faint sequence, and then do the callback
        public void Faint()
        {
            model.Animator.SetTrigger(FaintAnim);
            Active = false;
        }
    }
}
