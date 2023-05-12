using System.Collections.Generic;
using ProjectCatch.Data.Attacks;
using Sirenix.OdinInspector;
using UnityEngine;
using RangeInt = Fsi.Math.RangeInt;

namespace ProjectCatch.Gameplay.Pokemon
{
    [CreateAssetMenu(menuName = "Pokemon/Instance", fileName = "New Pokemon Instance")]
    public class PokemonInstanceData : ScriptableObject
    {
        [Title("Information")]

        [Required]
        [SerializeField]
        private PokemonData data;

        public PokemonData Data => data;
        
        [ShowIf(nameof(hasNickname))]
        [SerializeField]
        private string nickname = "";

        public string Name
        {
            get
            {
                if (hasNickname)
                {
                    return nickname;
                }

                return data.Name;
            }
        }

        [SerializeField]
        private bool hasNickname = false;

        [TitleGroup("Stats")]
        [Range(1, 100)]
        [SerializeField]
        private int level = 1;

        public int Level => level;

        [TitleGroup("Stats")]
        [SerializeField]
        private StatBlock ivs = new StatBlock();

        public StatBlock Ivs => ivs;

        public StatBlock Stats
        {
            get
            {
                cachedStats ??= new StatBlock(level, data.BaseStats, ivs);
                return cachedStats;
            }
        } 
        private StatBlock cachedStats;

        [TitleGroup("Stats")]
        [HideLabel]
        [DisplayAsString(false)]
        [SerializeField]
        private string statString;

        [Title("Attacks")]

        [SerializeField]
        private List<Attack> attacks = new List<Attack>();

        public IEnumerable<Attack> Attacks => attacks;

        private void OnValidate()
        {
            cachedStats = new StatBlock(level, data.BaseStats, ivs);
            statString = Stats.ToString();
        }

        [TitleGroup("Stats")]
        [Button("Randomize IVs")]
        private void RandomizeIvs()
        {
            RangeInt range = new RangeInt(1, 31);
            ivs = new StatBlock(range.Random(), range.Random(), range.Random(), range.Random(), range.Random(), range.Random());
            cachedStats = new StatBlock(level, data.BaseStats, ivs);
            statString = Stats.ToString();
        }
    }
}
