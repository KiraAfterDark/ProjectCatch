
using System;
using ProjectMaster.Data.Attacks;
using ProjectMaster.Gameplay.Pokemon;

namespace ProjectMaster.Battle.Actions
{
    public abstract class BattleAction
    {
        public enum ActionType
        {
            None = 0,
            Attack = 1,
            Item = 2,
            Switch = 3,
            Run = 4,
        }
        
        public BattlePokemon Source { get; protected set; }

        public abstract ActionType Type { get; }
    }
    
    public class AttackAction : BattleAction
    {
        public override ActionType Type => ActionType.Attack;

        public Attack Attack { get; }
        public BattlePokemon Attacker => Source;
        public BattlePokemon Target { get; }
        
        public AttackAction(Attack attack, BattlePokemon attacker, BattlePokemon target)
        {
            Attack = attack;
            Source = attacker;
            Target = target;
        }
    }

    public class SwitchAction : BattleAction
    {
        public override ActionType Type => ActionType.Switch;
        
        public PokemonInstance SwitchTo { get; }

        public SwitchAction(PokemonInstance switchTo)
        {
            SwitchTo = switchTo;
        }
    }
}
