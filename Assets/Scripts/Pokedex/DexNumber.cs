using System;
using UnityEngine;

namespace ProjectCatch.Pokedex
{
	[Serializable]
	public class DexNumber
	{
		[Min(0)]
		[SerializeField]
		private int national = 0;

		public int National => national;
	}
}
