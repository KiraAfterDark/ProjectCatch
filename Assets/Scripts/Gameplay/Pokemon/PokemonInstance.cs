using System.Collections.Generic;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Data.Pokemon;
using ProjectCatch.Data.Pokemon.Types;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon
{
    public class PokemonInstance
    {
        #region Pokemon Data
        
        private PokemonData data;
        
        public string Name => data.Name;

        public Type Type => data.TypeOne;

        public GameObject Model => data.Model;
        
        #endregion
        
        public int Level { get; private set; }
        
        #region Stats
        
        private StatBlock ivs;
        
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
            
            ivs = StatBlock.RandomIVs;

            Health = new Health(data.BaseStats.Hp);

            Attacks = new List<Attack>();
            Attacks.AddRange(data.Attacks);
        }

        public void Fainted()
        {
            Debug.Log($"{Name} has fainted at level {Level}");
        }
    }
}
