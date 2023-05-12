using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Battle.Initializer
{
    public class BattleInitializer
    {
        public TrainerInstance PlayerTrainerInstance { get; }

        public BattleInitializer(TrainerInstance playerTrainerInstance)
        {
            PlayerTrainerInstance = playerTrainerInstance;
        }
    }
}
