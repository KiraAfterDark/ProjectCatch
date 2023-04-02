using ProjectCatch.Battle.Ui;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCatch.Gameplay.Battle.Ui
{
    public class BattleHealthbar : MonoBehaviour
    {
        private BattlePokemon pokemon;

        [Title("Battle Healthbar")]

        [Range(0,1)]
        public float value;
        
        [Title("Hookups")]

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private TextMeshProUGUI healthText;

        [SerializeField]
        private Image healthbarImage;

        private void OnEnable()
        {
            if (pokemon != null)
            {
                pokemon.Health.OnDamage += UpdateHealth;
                pokemon.Health.OnHeal += UpdateHealth;
                pokemon.Health.OnDeath += OnDeath;
            }
        }

        private void OnDisable()
        {
            if (pokemon != null)
            {
                pokemon.Health.OnDamage -= UpdateHealth;
                pokemon.Health.OnHeal -= UpdateHealth;
                pokemon.Health.OnDeath -= OnDeath;
            }
        }

        public void Init(BattlePokemon pokemon)
        {
            this.pokemon = pokemon;

            nameText.text = pokemon.Name;
            levelText.text = $"Lvl. {pokemon.Level}";

            healthText.text = $"{pokemon.Health.CurrentHealth} / {pokemon.Health.MaxHealth}";
            float norm = pokemon.Health.Normalized;
            healthbarImage.transform.localScale = new Vector3(norm, 1, 1);
            value = norm;

            pokemon.Health.OnDamage += UpdateHealth;
            pokemon.Health.OnHeal += UpdateHealth;
            pokemon.Health.OnDeath += OnDeath;
        }

        private void UpdateHealth()
        {
            healthText.text = $"{pokemon.Health.CurrentHealth} / {pokemon.Health.MaxHealth}";
            float norm = pokemon.Health.Normalized;
            healthbarImage.transform.localScale = new Vector3(norm, 1, 1);
            value = norm;
        }

        private void OnDeath()
        {
            pokemon.Health.OnDeath -= OnDeath;
            pokemon.Health.OnDamage -= UpdateHealth;
            pokemon.Health.OnHeal -= UpdateHealth;
            pokemon = null;
            BattleUi.Instance.HideHealthbar(this);
        }

        private void OnValidate()
        {
            healthbarImage.transform.localScale = new Vector3(value, 1, 1);
        }
    }
}
