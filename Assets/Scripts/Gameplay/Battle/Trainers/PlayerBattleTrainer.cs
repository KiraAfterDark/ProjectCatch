using System;
using ProjectCatch.Battle.Actions;
using ProjectCatch.Battle.Ui;

namespace ProjectCatch
{
    public class PlayerBattleTrainer : BattleTrainer
    {
        private Action<BattleAction> actionSelectedCallback;
        
        public override void StartBattle(Action callback)
        {
            currentPokemon = Instantiate(battlePokemonPrefab, pokemonSocket);
            currentPokemon.Init(party[0]);
            
            BattleUi.Instance.UsePokemon(Name, currentPokemon.Name, callback);
            BattleUi.Instance.ShowPlayerHealth(currentPokemon);
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
