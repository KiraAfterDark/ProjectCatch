using System.Collections.Generic;
using ProjectCatch.Gameplay.Items;
using ProjectCatch.Gameplay.Pokemon;
using UnityEngine;

namespace ProjectCatch.Gameplay
{
    public class TrainerInstance
    {
        // Information
        public string Name { get; }

        public GameObject Model { get; }
        
        // Party
        public List<PokemonInstance> Party { get; private set; }
        
        // Inventory
        public Inventory Inventory { get; }
        
        public TrainerInstance()
        {
            Name = "Default Trainer Instance";

            Party = new();

            Inventory = new Inventory();
        }

        public TrainerInstance(TrainerInstanceData data)
        {
            Name = data.name;
            Model = data.Model;

            Party = new List<PokemonInstance>();
            foreach (PokemonInstanceData pokemonInstanceData in data.PartyInstance)
            {
                PokemonInstance pokemonInstance = new PokemonInstance(pokemonInstanceData);
                Party.Add(pokemonInstance);
            }

            Inventory = new Inventory(data.Inventory);
        }
    }
}
