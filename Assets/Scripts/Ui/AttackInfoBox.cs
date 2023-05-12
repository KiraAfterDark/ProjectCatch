using ProjectCatch.Data.Attacks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectCatch.Ui
{
    public class AttackInfoBox : MonoBehaviour
    {
        private Attack attack;
        
        [Title("References")]

        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI powerText;

        [SerializeField]
        private TextMeshProUGUI categoryText;

        [SerializeField]
        private TextMeshProUGUI typeText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        public void Show(Attack attack)
        {
            nameText.text = attack.Name;
            powerText.text = attack.Power.ToString();
            categoryText.text = attack.Category.ToString();
            typeText.text = attack.PokemonType.ToString();
            descriptionText.text = attack.Description;
        }
    }
}
