using System;
using UnityEngine;

namespace ProjectCatch.Data.Pokemon.Evolutions
{
    [Serializable]
    public class EvolutionData
    {
        [SerializeField]
        private PokemonData evolution;

        public PokemonData Evolution => evolution;

        [SerializeField]
        private EvolutionTypes evolutionType = EvolutionTypes.None;

        public EvolutionTypes EvolutionType => evolutionType;
        
        [SerializeField]
        private int level = 1;

        public int Level => level;
    }
    
    public enum EvolutionTypes
    {
        None = 0,
            
        Level = 1,
        Item = 2,
        Trade = 3,
        ItemTrade = 4,
        
    }
}
