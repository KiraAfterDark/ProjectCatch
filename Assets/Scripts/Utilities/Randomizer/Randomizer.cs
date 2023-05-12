using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Utilities
{
    public class Randomizer<T>
    {
        protected List<RandomizerEntry<T>> Entries { get; }

        public Randomizer()
        {
            Entries = new List<RandomizerEntry<T>>();
        }

        public virtual void Add(RandomizerEntry<T> entry)
        {
            Entries.Add(entry);
        }

        public virtual T GetValue()
        {
            return Entries[Random.Range(0, Entries.Count)].Entry;
        }
    }
}
