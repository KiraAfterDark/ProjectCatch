using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Utilities
{
    public class WeightedRandomizer<T> : Randomizer<T>
    {
        private int totalWeight = 0;

        public WeightedRandomizer() : base()
        {
            totalWeight = 0;
        }

        public override void Add(RandomizerEntry<T> entry)
        {
            if (entry is WeightedRandomizerEntry<T> weightedRandomizerEntry)
            {
                Entries.Add(weightedRandomizerEntry);
                totalWeight += weightedRandomizerEntry.Weight;
            }
            else
            {
                Debug.LogError("Must use Weighted Randomizer Entry with Weighted Randomizer");
            }
        }

        public override T GetValue()
        {
            int check = 0;
            int value = Random.Range(0, totalWeight);

            foreach (RandomizerEntry<T> entry in Entries)
            {
                if (entry is WeightedRandomizerEntry<T> weightedEntry)
                {
                    check += weightedEntry.Weight;

                    if (value <= check)
                    {
                        return weightedEntry.Entry;
                    }
                }
            }
            
            Debug.LogError($"Couldn't find weighted entry for value {value}, using base.");
            return base.GetValue();
        }
    }
}
