using System;
using UnityEngine;

namespace ProjectCatch.Data.Pokemon
{
    public class GrowthRate
    {
        public enum Rates
        {
            Slow = 0,
            MediumSlow = 1,
            MediumFast = 2,
            Fast = 3,
            Erratic = 4,
            Fluctuating = 5,
        }

        public static int GetExperienceForLevel(int level, Rates rate)
        {
            return rate switch
                   {
                       Rates.Slow => GetExperienceForLevelSlow(level),
                       Rates.MediumSlow => GetExperienceForLevelMediumSlow(level),
                       Rates.MediumFast => GetExperienceForLevelMediumFast(level),
                       Rates.Fast => GetExperienceForLevelFast(level),
                       Rates.Erratic => GetExperienceForLevelErratic(level),
                       Rates.Fluctuating => GetExperienceForLevelFluctuating(level),
                       _ => throw new ArgumentOutOfRangeException(nameof(rate), rate, null)
                   };
        }

        private static int GetExperienceForLevelSlow(int level)
        {
            float val = (5f / 4f) * Mathf.Pow(level, 3);
            return Mathf.RoundToInt(val);
        }
        
        private static int GetExperienceForLevelMediumSlow(int level)
        {
            float val = ((6f / 5f) * Mathf.Pow(level, 3)) - (15f * Mathf.Pow(level, 2)) + (100 * level) - 140;
            return Mathf.RoundToInt(val);
        }
        
        private static int GetExperienceForLevelMediumFast(int level)
        {
            return Mathf.RoundToInt(Mathf.Pow(level, 3));
        }
        
        private static int GetExperienceForLevelFast(int level)
        {
            return Mathf.RoundToInt((Mathf.Pow(level, 3) * 4) / 5);
        }
        
        private static int GetExperienceForLevelErratic(int level)
        {
            Debug.LogError("Not Implemented");
            return 0;
        }
        
        private static int GetExperienceForLevelFluctuating(int level)
        {
            Debug.LogError("Not Implemented");
            return 0;
        }
    }
}
