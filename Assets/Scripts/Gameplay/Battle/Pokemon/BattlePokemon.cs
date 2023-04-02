using System;
using System.Collections.Generic;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Pokemon;
using Sirenix.OdinInspector;
using UnityEngine;
using Type = ProjectCatch.Data.Pokemon.Types.Type;

namespace ProjectCatch
{
    public class BattlePokemon : MonoBehaviour
    {
        #region Data
        
        private PokemonInstance instance;

        public PokemonInstance Instance => instance;
        
        public string Name => instance.Name;

        public Data.Pokemon.Types.Type Type => instance.Type;
        
        #endregion
        
        public List<Attack> Attacks => instance.Attacks;
        
        private int level;

        public int Level => level;

        public Health Health => instance.Health;
    
        private GameObject model;

        [Title("Sockets")]

        [SerializeField]
        private Transform modelSocket;
        
        public bool Active { get; private set; }

        public void Init(PokemonInstance instance)
        {
            this.instance = instance;
            
            Debug.Log($"{Name} - Level: {level} - Health: {Health.CurrentHealth}/{Health.MaxHealth}");

            model = Instantiate(instance.Model, modelSocket);

            Active = true;
        }

        public void Damage(int amount)
        {
            Health.Damage(amount);
            Debug.Log($"{Name} takes {amount} damage. Current Health: {Health.CurrentHealth}");
        }

        public void Faint(Action callback)
        {
            Debug.Log($"{Name} has fainted.");
            Destroy(model);

            Active = false;
            callback?.Invoke();
        }
    }
}
