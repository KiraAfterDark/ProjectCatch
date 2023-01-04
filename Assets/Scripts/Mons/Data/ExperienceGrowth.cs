using System;
using UnityEngine;

namespace ProjectCatch.Mons.Data
{
    public static class ExperienceGrowth
    {
        public enum ExperienceType
        {
            Fast = 0,
            Medium = 1,
            Slow = 2,
        }

        public static int GetExpFromLevel(int level, ExperienceType experienceType)
        {
            return experienceType switch
            {
                ExperienceType.Fast => GetExpFromLevelFast(level),
                ExperienceType.Slow => GetExpFromLevelSlow(level),
                ExperienceType.Medium => GetExpFromLevelMedium(level),
                _ => throw new ArgumentOutOfRangeException(nameof(experienceType), experienceType, null),
            };
        }

        private static int GetExpFromLevelFast(int level)
        {
            float exp = (4f / 5f) * Mathf.Pow(level, 3);
            return (int)exp;
        }

        private static int GetExpFromLevelMedium(int level)
        {
            float exp = Mathf.Pow(level, 3);
            return (int)exp;
        }

        private static int GetExpFromLevelSlow(int level)
        {
            float exp = (6f / 5f) * Mathf.Pow(level, 3)
                        - 15 * Mathf.Sqrt(level)
                        + 100 * level
                        - 140;
            return (int)exp;
        }
    }
}
