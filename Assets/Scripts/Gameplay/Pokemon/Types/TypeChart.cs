using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon.Types
{
    [CreateAssetMenu(menuName = "Config/Type Chart", fileName = "New Type Chart")]
    public class TypeChart : SerializedScriptableObject
    {
        [TableMatrix(HorizontalTitle = "Defender", VerticalTitle = "Attacker")]
        public Effectiveness[,] typeChart = new Effectiveness[18, 18];

        public Effectiveness GetEffectiveness(PokemonType attacker, PokemonType defender)
        {
            return typeChart[(int)attacker, (int)defender];
        }
    }
}
