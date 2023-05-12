using System;
using System.Collections;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;
using ProjectCatch.Gameplay.Pokemon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectCatch
{
    public class EnemyBattleTrainer : BattleTrainer
    {
        public override void UsePokemon(PokemonInstance pokemon, Action callback)
        {
            StartCoroutine(UsePokemonSequence(pokemon, callback));
        }

        private IEnumerator UsePokemonSequence(PokemonInstance pokemon, Action callback)
        {
            currentPokemon = Instantiate(battlePokemonPrefab, pokemonSocket);
            currentPokemon.Init(pokemon);
            currentPokemon.SwitchIn();
            
            BattleUi.Instance.UsePokemon(Name, currentPokemon.Name);
            BattleUi.Instance.ShowEnemyHealth(currentPokemon);

            yield return new WaitForSeconds(2f);

            callback?.Invoke();
        }

        public override void SelectAction(Action<BattleAction> actionSelectCallback)
        {
            AttackAction attack = new (currentPokemon.Attacks[Random.Range(0, currentPokemon.Attacks.Count)], 
                                       currentPokemon,
                                       battleController.PlayerPokemon);
            
            actionSelectCallback?.Invoke(attack);
        }

        public override void SelectPokemon(Action<PokemonInstance> callback, Action cancelCallback)
        {
            foreach (PokemonInstance pokemon in Party)
            {
                if (pokemon.Health.Fainted)
                {
                    continue;
                }

                callback?.Invoke(pokemon);
                break;
            }
        }
    }
}
