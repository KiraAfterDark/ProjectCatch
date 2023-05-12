using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Battle.Initializer
{
    public class TrainerBattleInitializer : BattleInitializer
    {
        public TrainerInstance EnemyTrainerInstance { get; }
        
        public TrainerBattleInitializer(TrainerInstance playerTrainerInstance, TrainerInstance enemyTrainerInstance) : base(playerTrainerInstance)
        {
            EnemyTrainerInstance = enemyTrainerInstance;
        }
    }
}
