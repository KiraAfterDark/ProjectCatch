using System.Collections;
using System.Collections.Generic;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Gameplay.Pokemon.Types;
using UnityEngine;

namespace ProjectCatch.Utilities
{
    public static class PokemonMath
    {
        public struct DamageCalculation
        {
            public int damage;
            public Effectiveness effectiveness;
            public bool critical;
        }
        
        public static DamageCalculation CalculateDamage(Attack attack, PokemonInstance attacker, PokemonInstance defender)
        {
            float damage = 2.0f * attacker.Level;
            damage /= 5.0f;
            damage += 2.0f;
            damage *= attack.Power;

            float ratio = 1;
            if (attack.Category == AttackCategory.Physical)
            {
                ratio = (float)attacker.Stats.Attack / (float)defender.Stats.Defense;
            }
            else
            {
                ratio = (float)attacker.Stats.SpecialAttack / (float)defender.Stats.SpecialDefense;
            }

            damage *= ratio;
            damage /= 50.0f;
            damage += 2;
            damage *= Random.Range(0.90f, 1.10f);
            // critical
            Effectiveness effectiveness = GameplayController.Instance.TypeChart.GetEffectiveness(attacker.PokemonType, defender.PokemonType);

            return new DamageCalculation
                   {
                       damage = Mathf.RoundToInt(damage),
                       effectiveness = effectiveness,
                       critical = false,
                   };
        }
    }
}
