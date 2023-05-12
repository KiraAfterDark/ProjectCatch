using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectCatch.Gameplay.Ui
{
    public class HealthbarWithText : Healthbar
    {
        [Title("Text References")]

        [SerializeField]
        private TextMeshProUGUI healthText;

        public override void Initialize(Health health)
        {
            base.Initialize(health);

            healthText.text = $"{health.CurrentHealth} / {health.MaxHealth}";
        }

        protected override void OnHealthChange()
        {
            base.OnHealthChange();
            
            healthText.text = $"{health.CurrentHealth} / {health.MaxHealth}";
        }
    }
}
