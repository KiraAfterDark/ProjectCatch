using System.Collections.Generic;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Pokemon.Types;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon
{
    public class PokemonInstance
    {
        #region Pokemon Data
        
        private PokemonData data;

        public string Name => data.Name;

        public PokemonType PokemonType => data.TypeOne;

        public PokemonModel Model => data.Model;

        public GameObject Icon => data.Icon;
        
        #endregion
        
        public int Level { get; private set; }
        
        #region Stats

        private StatBlock ivs;

        public StatBlock Stats { get; private set; }
        
        #endregion
        
        #region Health
        
        public Health Health { get; }
        
        #endregion
        
        #region Attacks

        public List<Attack> Attacks { get; private set; }

        #endregion

        public PokemonInstance(PokemonData data, int level)
        {
            // Get the data
            this.data = data;

            Level = level;

            Fsi.Math.RangeInt range = new (1, 31);
            ivs = new StatBlock(range.Random(), range.Random(), range.Random(), range.Random(), range.Random(), range.Random());

            Stats = new StatBlock(level, data.BaseStats, ivs);

            Health = new Health(Stats.Hp);
            Attacks = new List<Attack>();
            Attacks.AddRange(data.Attacks);
        }

        public PokemonInstance(PokemonInstanceData instanceData)
        {
            data = instanceData.Data;
            Level = instanceData.Level;
            ivs = instanceData.Ivs;
            Stats = instanceData.Stats;
            Health = new Health(Stats.Hp);
            Attacks = new List<Attack>(instanceData.Attacks);
        }

        public void Fainted()
        {
            Debug.Log($"{Name} has fainted at level {Level}");
        }
    }
}
