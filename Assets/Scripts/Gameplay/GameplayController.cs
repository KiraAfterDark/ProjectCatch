using Fsi.Runtime;
using ProjectCatch.Gameplay.Battle.Initializer;
using ProjectCatch.Gameplay.Pokemon.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Gameplay
{
    public class GameplayController : MbSingleton<GameplayController>
    {
        [Title("Game Settings")]

        [SerializeField]
        private TypeChart typeChart;

        public TypeChart TypeChart => typeChart;
        
        #region Player

        [Title("Player")]

        [SerializeField]
        private TrainerInstanceData playerTrainerInstanceData;
        
        private TrainerInstance playerTrainerInstance;

        #endregion

        #region Enemy

        [Title("Enemy")]

        [SerializeField]
        private TrainerInstanceData enemyTrainerInstanceData;

        #endregion
        
        #region Battle Init

        private BattleInitializer battleInitializer;
        private bool isInitializerReady = false;
        
        #endregion

        protected override void OnAwake()
        {
            playerTrainerInstance = new TrainerInstance(playerTrainerInstanceData);
            PrepareBattle();
        }

        private void PrepareBattle()
        {
            var enemyTrainerInstance = new TrainerInstance(enemyTrainerInstanceData);
            battleInitializer = new TrainerBattleInitializer(playerTrainerInstance, enemyTrainerInstance);
            isInitializerReady = true;
        }
        
        public bool TryGetBattleInitializer(out BattleInitializer initializer)
        {
            if (isInitializerReady)
            {
                initializer = battleInitializer;
                return true;
            }

            initializer = null;
            return false;
        }
    }
}
