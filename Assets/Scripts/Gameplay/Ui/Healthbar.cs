using System;
using System.Collections;
using DG.Tweening;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCatch.Gameplay.Ui
{
    public class Healthbar : MonoBehaviour
    {
        protected Health health;
        
        #if UNITY_EDITOR
        
        [Title("Debugging")]

        [Range(0, 1)]
        [SerializeField]
        private float value;
        
        #endif

        [Title("Properties")]

        [SerializeField]
        private Gradient healthGradient;
        
        [Title("References")]
        
        [SerializeField]
        private Image foreground;

        // tween
        private bool isTweening;
        private Tween healthTween;

        private void OnEnable()
        {
            if (health != null)
            {
                health.OnDamage += OnHealthChange;
                health.OnHeal += OnHealthChange;
            }
        }

        private void OnDisable()
        {
            if (health != null)
            {
                health.OnDamage -= OnHealthChange;
                health.OnHeal -= OnHealthChange;
            }
        }

        public virtual void Initialize(Health health)
        {
            this.health = health;
            
            health.OnDamage += OnHealthChange;
            health.OnHeal += OnHealthChange;
            
            foreground.transform.localScale = new Vector3(health.Normalized, 1, 1);
            foreground.color = healthGradient.Evaluate(health.Normalized);
        }

        protected virtual void OnHealthChange()
        {
            if (isTweening)
            {
                healthTween.Kill();
            }

            float norm = foreground.transform.localScale.x;
            healthTween = DOTween.To(() => norm, x => norm = x, health.Normalized, 0.5f)
                                 .OnUpdate(() =>
                                 {
                                     foreground.transform.localScale = new Vector3(norm, 1, 1);
                                     foreground.color = healthGradient.Evaluate(norm);
                                 })
                                 .OnComplete(() =>
                                 {
                                     isTweening = false;
                                     foreground.transform.localScale = new Vector3(health.Normalized, 1, 1);
                                     foreground.color = healthGradient.Evaluate(health.Normalized);
                                 });

            healthTween.Play();
            isTweening = true;
        }

        public void Clear(Action callback)
        {
            if (isTweening)
            {
                healthTween.Kill();
            }
            
            float norm = foreground.transform.localScale.x;
            healthTween = DOTween.To(() => norm, x => norm = x, health.Normalized, 0.5f)
                                 .OnUpdate(() =>
                                 {
                                     foreground.transform.localScale = new Vector3(norm, 1, 1);
                                     foreground.color = healthGradient.Evaluate(norm);
                                 })
                                 .OnComplete(() =>
                                 {
                                     isTweening = false;
                                     foreground.transform.localScale = new Vector3(health.Normalized, 1, 1);
                                     foreground.color = healthGradient.Evaluate(health.Normalized);
                                     StartCoroutine(FinishDelay(callback));
                                 });
        }

        private IEnumerator FinishDelay(Action callback)
        {
            yield return new WaitForSeconds(0.8f);
            callback?.Invoke();
        }

        #if UNITY_EDITOR
        
        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                OnHealthChangeDebug();
            }
        }
        
        protected virtual void OnHealthChangeDebug()
        {
            foreground.transform.localScale = new Vector3(value, 1, 1);
            foreground.color = healthGradient.Evaluate(value);
        }
        
        #endif
    }
}
