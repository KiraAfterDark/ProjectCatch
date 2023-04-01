using System;
using ProjectMaster.Battle.Actions;
using ProjectMaster.Battle.Ui;
using Random = UnityEngine.Random;

namespace ProjectMaster
{
    public class EnemyBattleTrainer : BattleTrainer
    {
        public override void StartBattle(Action callback)
        {
            currentPokemon = Instantiate(battlePokemonPrefab, pokemonSocket);
            currentPokemon.Init(party[0]);
            
            TrainerBattleUi.Instance.UsePokemon(Name, currentPokemon.Name, callback);
            TrainerBattleUi.Instance.ShowEnemyHealth(currentPokemon);
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
