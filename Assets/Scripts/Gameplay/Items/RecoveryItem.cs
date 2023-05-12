using System;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Items
{
    [CreateAssetMenu(fileName = "New Recovery Item", menuName = "Items/Recovery", order = 0)]
    public class RecoveryItem : Item
    {
        [Title("Recovery")]

        [Min(0)]
        [SerializeField]
        private int amount;

        public int Amount => amount;

        public override bool UsableInBattle => true;
        public override bool UsableOutBattle => true;

        public override void Apply(PokemonInstance pokemon, Action callback)
        {
            pokemon.Health.Heal(amount);
            callback?.Invoke();
        }

        public override void Apply(BattlePokemon pokemon, Action callback)
        {
            Apply(pokemon.Instance, callback);
        }
    }
}
