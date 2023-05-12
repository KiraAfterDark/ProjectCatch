using System;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon
{
	[Serializable]
	public class StatBlock
	{
		[Min(0)]
		[SerializeField]
		private int hp = 0;

		public int Hp => hp;
		
		[Min(0)]
		[SerializeField]
		private int attack = 0;

		public int Attack => attack;
		
		[Min(0)]
		[SerializeField]
		private int defense = 0;

		public int Defense => defense;
		
		[Min(0)]
		[SerializeField]
		private int specialAttack = 0;

		public int SpecialAttack => specialAttack;
		
		[Min(0)]
		[SerializeField]
		private int specialDefense = 0;

		public int SpecialDefense => specialDefense;
		
		[Min(0)]
		[SerializeField]
		private int speed = 0;

		public int Speed => speed;

		public StatBlock()
		{
			hp = 0;
			attack = 0;
			defense = 0;
			specialAttack = 0;
			specialDefense = 0;
			speed = 0;
		}

		public StatBlock(int h, int a, int d, int sa, int sd, int s)
		{
			hp = h;
			attack = a;
			defense = d;
			specialAttack = sa;
			specialDefense = sd;
			speed = s;
		}

		public StatBlock(int level, StatBlock baseStats, StatBlock ivs)
		{
			hp = Mathf.FloorToInt(0.01f * (2 * baseStats.Hp + ivs.Hp) * level) + level + 10;
			attack = Mathf.FloorToInt(0.01f * (2 * baseStats.Attack * ivs.Attack) * level) + level;
			defense = Mathf.FloorToInt(0.01f * (2 * baseStats.Defense * ivs.Defense) * level) + level;
			specialAttack = Mathf.FloorToInt(0.01f * (2 * baseStats.SpecialAttack * ivs.SpecialAttack) * level) + level;
			specialDefense = Mathf.FloorToInt(0.01f * (2 * baseStats.SpecialDefense * ivs.SpecialDefense) * level) + level;
			speed = Mathf.FloorToInt(0.01f * (2 * baseStats.Speed * ivs.Speed) * level) + level;
		}
		
		public override string ToString()
		{
			string s = $"HP: {Hp}";
			s += $"\nAttack: {Attack}";
			s += $"\nDefense: {Defense}";
			s += $"\nSpecialAttack: {SpecialAttack}";
			s += $"\nSpecialDefense: {SpecialDefense}";
			s += $"\nSpeed: {Speed}";

			return s;
		}
	}
}
