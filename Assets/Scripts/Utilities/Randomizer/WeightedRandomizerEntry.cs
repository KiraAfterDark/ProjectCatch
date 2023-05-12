using System;
using UnityEngine;

namespace ProjectCatch.Utilities
{
    public class WeightedRandomizerEntry<T> : RandomizerEntry<T>
    {
        public virtual int Weight { get; }
        
        public WeightedRandomizerEntry(T entry, int weight) : base(entry)
        {
            Weight = weight;
        }
    }

    [Serializable]
    public class WeightedRandomizerEntryInt : WeightedRandomizerEntry<int>
    {
        [SerializeField]
        private int entry;

        public override int Entry => entry;

        [SerializeField]
        private int weight;
        
        public override int Weight => weight;
        
        public WeightedRandomizerEntryInt(int entry, int weight) : base(entry, weight)
        {
        }
    }
}
