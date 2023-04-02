using System;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon
{
	public class Health
	{
		public event Action OnHeal;
		public event Action OnDamage;
		public event Action OnDeath;
		
		public int CurrentHealth { get; private set; }
		
		public int MaxHealth { get; private set; }

		public float Normalized => (float)CurrentHealth / (float)MaxHealth;

		public bool Fainted => CurrentHealth <= 0;

		public Health(int maxHp)
		{
			MaxHealth = maxHp;
			CurrentHealth = maxHp;
		}

		public void Heal(int heal)
		{
			CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0, MaxHealth);
			OnHeal?.Invoke();
		}

		public void Damage(int damage)
		{
			CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
			OnDamage?.Invoke();

			if (CurrentHealth <= 0)
			{
				OnDeath?.Invoke();
			}
		}

		public void UpdateMaxHealth(int newMax)
		{
			int diff = newMax - MaxHealth;
			MaxHealth = newMax;
			Heal(diff);
		}
	}
}
