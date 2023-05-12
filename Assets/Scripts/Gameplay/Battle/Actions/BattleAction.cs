using System;
using ProjectCatch.Data.Attacks;
using ProjectCatch.Gameplay.Items;
using ProjectCatch.Gameplay.Pokemon;

namespace ProjectCatch.Battle.Actions
{
    public abstract class BattleAction
    {
        public BattlePokemon Source { get; }

        protected BattleAction(BattlePokemon source)
        {
            Source = source;
        }

        public abstract void Resolve(Action resolveCallback);
    }
    
    public class AttackAction : BattleAction
    {
        public Attack Attack { get; }
        public BattlePokemon Attacker => Source;
        public BattlePokemon Target { get; }
        
        public AttackAction(Attack attack, BattlePokemon attacker, BattlePokemon target) : base(attacker)
        {
            Attack = attack;
            Target = target;
        }

        public override void Resolve(Action resolveCallback)
        {
            Attacker.UseAttack(Attack, Target, resolveCallback);
        }
    }

    public class SwapAction : BattleAction
    {
        public BattleTrainer Trainer { get; }
        
        public PokemonInstance SwapTo { get; }

        public SwapAction(BattleTrainer trainer, PokemonInstance swapTo) : base(trainer.CurrentPokemon)
        {
            Trainer = trainer;
            SwapTo = swapTo;
        }

        public override void Resolve(Action resolveCallback)
        {
            Trainer.SwitchPokemon(SwapTo, resolveCallback);
        }
    }

    public class ItemAction : BattleAction
    {
        public Item Item { get; }
        
        public PokemonInstance Target { get; }

        public ItemAction(Item item, PokemonInstance target, BattlePokemon source) : base(source)
        {
            Item = item;
            Target = target;
        }

        public override void Resolve(Action resolveCallback)
        {
            Item.Apply(Target, resolveCallback);
        }
    }
}
