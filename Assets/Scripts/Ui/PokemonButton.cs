using System;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Gameplay.Ui;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ProjectCatch.Ui
{
    public class PokemonButton : MonoBehaviour
    {
        private PokemonInstance instance;
        private Action<PokemonInstance> callback;

        private bool canSelect = false;

        [Title("Ui Elements")]

        [SerializeField]
        private Button button;
        
        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI levelText;

        [SerializeField]
        private Healthbar healthbar;

        [FormerlySerializedAs("spriteContainer")]
        [Title("Containers")]
        
        [SerializeField]
        private Transform iconContainer;

        private GameObject icon;

        public void Init(PokemonInstance instance, Action<PokemonInstance> callback)
        {
            this.instance = instance;
            this.callback = callback;
            
            healthbar.Initialize(instance.Health);

            nameText.text = instance.Name;
            levelText.text = $"{instance.Level}";

            if (icon != null)
            {
                Destroy(icon);
            }

            icon = Instantiate(instance.Icon, iconContainer);

            canSelect = true;

            button.interactable = true;
        }

        public void Deactivate()
        {
            button.interactable = false;
        }

        public void OnSelected()
        {
            if (!canSelect)
            {
                Debug.LogWarning($"{gameObject.name} cannot be selected.");
                return;
            }

            canSelect = true;

            callback?.Invoke(instance);
        }
    }
}
