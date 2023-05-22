using Fsi.Runtime;
using ProjectCatch.Gameplay.Battle.Initializer;
using ProjectCatch.Gameplay.Maps;
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
        
        #region Battle Initializer

        private BattleInitializer battleInitializer;
        private bool isInitializerReady = false;
        
        #endregion

        #region Map

        public Map Map { get; private set; }

        [Title("Map")]

        [SerializeField]
        private MapProperties mapProperties;

        #endregion

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            StartRun();
        }

        #region Battle 
        
        private void PrepareBattle(TrainerInstanceData enemyInstanceData)
        {
            var enemyTrainerInstance = new TrainerInstance(enemyInstanceData);
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
        
        #endregion

        #region Map

        public void GenerateMap()
        {
            Map = new Map(mapProperties);
        }

        #endregion
        
        #region Start Run

        private void StartRun()
        {
            GameplaySceneManager.Instance.StartCharacterBuilderScene();
        }
        
        #endregion
    }
}
