using ProjectCatch.Battle.Ui;
using ProjectCatch.Gameplay.Ui;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectCatch.Gameplay.Battle.Ui
{
    public class PokemonBattleInfo : MonoBehaviour
    {
        private BattlePokemon pokemon;

        [Title("Healthbar")]

        [SerializeField]
        private Healthbar healthbar;
        
        [Title("Text")]

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI levelText;
        
        private void OnEnable()
        {
            if (pokemon != null)
            {
                pokemon.Health.OnDeath += OnDeath;
            }
        }

        private void OnDisable()
        {
            if (pokemon != null)
            {
                pokemon.Health.OnDeath -= OnDeath;
            }
        }

        public void Init(BattlePokemon pokemon)
        {
            this.pokemon = pokemon;

            nameText.text = pokemon.Name;
            levelText.text = $"Lvl. {pokemon.Level}";

            float norm = pokemon.Health.Normalized;
            healthbar.Initialize(pokemon.Health);
            
            pokemon.Health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            pokemon.Health.OnDeath -= OnDeath;
            pokemon = null;

            healthbar.Clear(DoClear);
        }

        private void DoClear()
        {
            BattleUi.Instance.HideHealthbar(this);
        }
    }
}
