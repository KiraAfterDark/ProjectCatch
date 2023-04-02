using System;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;
using Random = UnityEngine.Random;

namespace ProjectCatch
{
    public class EnemyBattleTrainer : BattleTrainer
    {
        public override void StartBattle(Action callback)
        {
            currentPokemon = Instantiate(battlePokemonPrefab, pokemonSocket);
            currentPokemon.Init(party[0]);
            
            BattleUi.Instance.UsePokemon(Name, currentPokemon.Name, callback);
            BattleUi.Instance.ShowEnemyHealth(currentPokemon);
        }

        public override void SelectAction(Action<BattleAction> callback)
        {
            AttackAction attack = new (currentPokemon.Attacks[Random.Range(0, currentPokemon.Attacks.Count)], 
                                       currentPokemon,
                                       battleController.PlayerPokemon);
            
            callback?.Invoke(attack);
        }
    }
}
