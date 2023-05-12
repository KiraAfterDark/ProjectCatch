using System.Collections.Generic;
using ProjectCatch.Assets.Pokemon.Abilities;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Pokemon.Evolutions;
using ProjectCatch.Gameplay.Pokemon.Types;
using ProjectCatch.Pokedex;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon
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
		private PokemonType typeOne = PokemonType.Normal;

		public PokemonType TypeOne => typeOne;

		[SerializeField]
		private PokemonType typeTwo = PokemonType.None;

		[SerializeField]
		private int captureRate = 50;

		public int CaptureRate => captureRate;

		[Title("Visuals")]

		[SerializeField]
		private PokemonModel model;

		public PokemonModel Model => model;

		[SerializeField]
		private GameObject icon;

		public GameObject Icon => icon;

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
