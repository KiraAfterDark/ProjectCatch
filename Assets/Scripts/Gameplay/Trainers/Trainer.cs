using System.Collections;
using System.Collections.Generic;
using ProjectMaster.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster
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
