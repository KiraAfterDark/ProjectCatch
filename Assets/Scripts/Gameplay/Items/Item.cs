using System;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectCatch.Gameplay.Items
{
    public abstract class Item : ScriptableObject
    {
        [Title("Information")]

        [SerializeField]
        private new string name = "";

        public string Name => name;

        [SerializeField]
        private string description = "";

        public string Description => description;

        [Title("Visual")]

        [SerializeField]
        private Sprite sprite;

        public Sprite Sprite => sprite;

        [Title("Fling")]

        [Min(0)]
        [SerializeField]
        private int flingDamage;

        public int FlingDamage => flingDamage;

        [Title("Store")]

        [Min(0)]
        [SerializeField]
        private int buy = 0;

        public int Buy => buy;

        [Min(0)]
        [SerializeField]
        private int sell;

        public int Sell => buy;
        
        public abstract bool UsableInBattle { get; }
        public abstract bool UsableOutBattle { get; }
        
        public abstract void Apply(PokemonInstance pokemon, Action callback);

        public abstract void Apply(BattlePokemon pokemon, Action callback);

        public override string ToString()
        {
            return Name;
        }
    }
}
