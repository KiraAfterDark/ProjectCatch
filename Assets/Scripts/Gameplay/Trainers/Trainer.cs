using System.Collections;
using System.Collections.Generic;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch
{
    public class Trainer
    {
        // Information
        public string Name { get; }
        
        
        // Party
        public List<PokemonInstance> Party;

        public Trainer()
        {
            Name = "Default";

            Party = new();
        }
    }
}
