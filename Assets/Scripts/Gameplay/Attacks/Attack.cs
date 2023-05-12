using ProjectCatch.Gameplay.Pokemon.Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCatch.Data.Attacks
{
    [CreateAssetMenu(menuName = "Attack", fileName = "New Attack")]
    public class Attack : ScriptableObject
    {
        [SerializeField]
        private int id = 0;

        public int ID => id;
        
        [SerializeField]
        private new string name;

        public string Name => name;

        [SerializeField]
        private string description;

        public string Description => description;

        [Min(0)]
        [SerializeField]
        private int power = 50;

        public int Power => power;
        
        [FormerlySerializedAs("type")]
        [SerializeField]
        private PokemonType pokemonType;

        public PokemonType PokemonType => pokemonType;

        [SerializeField]
        private AttackCategory category;

        public AttackCategory Category => category;

    }

    public enum AttackCategory
    {
        Physical = 0,
        Special = 1,
    }
}
