using System;
using System.Collections;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;
using ProjectCatch.Gameplay.Pokemon;
using ProjectCatch.Gameplay.Ui;
using UnityEngine;

namespace ProjectCatch
{
    public class PlayerBattleTrainer : BattleTrainer
    {
        public override void UsePokemon(PokemonInstance pokemon, Action callback)
        {
            StartCoroutine(UsePokemonSequence(pokemon, callback));
        }
        
        private IEnumerator UsePokemonSequence(PokemonInstance pokemon, Action callback)
        {
            if (currentPokemon == null)
            {
                currentPokemon = Instantiate(battlePokemonPrefab, pokemonSocket);
            }

            currentPokemon.Init(pokemon);
            currentPokemon.SwitchIn();
            
            BattleUi.Instance.UsePokemon(Name, currentPokemon.Name);
            BattleUi.Instance.ShowPlayerHealth(currentPokemon);

            yield return new WaitForSeconds(2f);

            callback?.Invoke();
        }

        public override void SelectAction(Action<BattleAction> actionSelectCallback)
        {
            battleUi.PlayerActionSelect(this, actionSelectCallback);
        }

        public override void SelectPokemon(Action<PokemonInstance> callback, Action cancelCallback)
        {
            PartyManagerScreen.Instance.RequestPokemon(Party, currentPokemon, callback, cancelCallback);
        }

        private IEnumerator SwapPokemonSequence(PokemonInstance instance, Action callback)
        {
            if (currentPokemon != null)
            {
                currentPokemon.SwitchOut();
            }

            yield return new WaitForSeconds(1f);

            UsePokemon(instance, callback);
        }
    }
}
