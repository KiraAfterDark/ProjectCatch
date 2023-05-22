using System;
using System.Collections.Generic;
using Fsi.Runtime;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Ui;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCatch.Gameplay.Ui
{
    public class PartyManagerScreen : MbSingleton<PartyManagerScreen>
    {
        private Action<PokemonInstance> pokemonSelectCallback;
        private Action cancelCallback;
        
        [Title("Containers")]

        [SerializeField]
        private List<PokemonButton> pokemonButtons = new List<PokemonButton>();

        [SerializeField]
        private Button cancelButton;
        
        [Title("Canvas")]
        
        [SerializeField]
        private Canvas canvas;

        protected override void OnAwake()
        {
            canvas.gameObject.SetActive(false);
            
            DontDestroyOnLoad(gameObject);
        }

        public void RequestPokemon(List<PokemonInstance> party, BattlePokemon currentPokemon, Action<PokemonInstance> selectCallback, Action cancelCallback)
        {
            pokemonSelectCallback = selectCallback;
            this.cancelCallback = cancelCallback;
            
            for (int i = 0; i < pokemonButtons.Count; i++)
            {
                if (party.Count > i)
                {
                    pokemonButtons[i].gameObject.SetActive(true);
                    pokemonButtons[i].Init(party[i], OnPokemonSelected);
                    
                    if (currentPokemon != null && party[i] == currentPokemon.Instance)
                    {
                        pokemonButtons[i].Deactivate();
                    }
                }
                else
                {
                    pokemonButtons[i].gameObject.SetActive(false);
                }
            }

            canvas.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(cancelCallback != null);
        }

        private void OnPokemonSelected(PokemonInstance pokemon)
        {
            canvas.gameObject.SetActive(false);
            pokemonSelectCallback?.Invoke(pokemon);
        }

        public void Cancel()
        {
            canvas.gameObject.SetActive(false);
            cancelCallback?.Invoke();
        }
    }
}
