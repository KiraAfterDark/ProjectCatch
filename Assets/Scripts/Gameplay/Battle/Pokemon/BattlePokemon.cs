using System;
using System.Collections.Generic;
using ProjectMaster.Data.Attacks;
using ProjectMaster.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;
using Type = ProjectMaster.Data.Pokemon.Types.Type;

namespace ProjectMaster
{
    public class BattlePokemon : MonoBehaviour
    {
        #region Data
        
        private PokemonInstance instance;
        
        public string Name => instance.Name;

        public Type Type => instance.Type;
        
        #endregion
        
        public List<Attack> Attacks => instance.Attacks;
        
        private int level;

        public int Level => level;

        public Health Health => instance.Health;
    
        private GameObject model;

        [Title("Sockets")]

        [SerializeField]
        private Transform modelSocket;

        public void Init(PokemonInstance instance)
        {
            this.instance = instance;
            
            Debug.Log($"{Name} - Level: {level} - Health: {Health.CurrentHealth}/{Health.MaxHealth}");

            model = Instantiate(instance.Model, modelSocket);
        }

        public void Damage(int amount)
        {
            Health.Damage(amount);
            Debug.Log($"{Name} takes {amount} damage. Current Health: {Health.CurrentHealth}");
        }
    }
}
