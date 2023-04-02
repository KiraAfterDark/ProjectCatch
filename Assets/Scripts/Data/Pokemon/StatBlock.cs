using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectCatch.Data.Pokemon
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

		public static StatBlock RandomIVs => new StatBlock(Random.Range(0, 31),
		                                                   Random.Range(0, 31),
		                                                   Random.Range(0, 31),
		                                                   Random.Range(0, 31),
		                                                   Random.Range(0, 31),
		                                                   Random.Range(0, 31));
	}
}
