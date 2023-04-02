using System.Collections.Generic;
using ProjectCatch.Assets.Pokemon.Abilities;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Data.Pokemon.Evolutions;
using ProjectCatch.Data.Pokemon.Types;
using ProjectCatch.Pokedex;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization.Tables;

namespace ProjectCatch.Data.Pokemon
{
	[CreateAssetMenu(menuName = "Pokemon/Data", fileName = "New Pokemon Data")]
	public class PokemonData : ScriptableObject
	{
		[Title("Information")]
		
		[SerializeField]
		private DexNumber dexNumber;

		public DexNumber DexNumber => dexNumber;

		[SerializeField]
		private new string name = "";

		public string Name => name;

		[SerializeField]
		private Type typeOne = Type.Normal;

		public Type TypeOne => typeOne;

		[SerializeField]
		private Type typeTwo = Type.None;

		[SerializeField]
		private int captureRate = 50;

		public int CaptureRate => captureRate;

		[Title("Visuals")]

		[SerializeField]
		private GameObject model;

		public GameObject Model => model;

		[Title("Evolution")]
		
		[SerializeField]
		private List<EvolutionData> evolutions = new List<EvolutionData>();

		public List<EvolutionData> Evolutions => evolutions;

		[Title("Attacks")]

		[SerializeField]
		private List<Attack> attacks = new List<Attack>();

		public List<Attack> Attacks => attacks;
		
		[Title("Abilities")]
		
		[SerializeField]
		private List<AbilityValue> abilities = new List<AbilityValue>();

		public List<AbilityValue> Abilities => abilities;
		
		[Title("Stats")]
		
		[SerializeField]
		private StatBlock baseStats = new StatBlock();

		public StatBlock BaseStats => baseStats;
		
		// egg moves??
		
		// move tutor??
	}
}
