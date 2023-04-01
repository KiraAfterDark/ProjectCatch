using System;
using ProjectMaster.Battle.Actions;
using ProjectMaster.Battle.Ui;

namespace ProjectMaster
{
    public class PlayerBattleTrainer : BattleTrainer
    {
        private Action<BattleAction> actionSelectedCallback;
        
        public override void StartBattle(Action callback)
        {
            currentPokemon = Instantiate(battlePokemonPrefab, pokemonSocket);
            currentPokemon.Init(party[0]);
            
            TrainerBattleUi.Instance.UsePokemon(Name, currentPokemon.Name, callback);
            TrainerBattleUi.Instance.ShowPlayerHealth(currentPokemon);
        }

        public override void SelectAction(Action<BattleAction> callback)
        {
            actionSelectedCallback = callback;
            battleUi.PlayerActionSelect(this, ActionSelected);
        }

        private void ActionSelected(BattleAction battleAction)
        {
            actionSelectedCallback?.Invoke(battleAction);
        }
    }
}
