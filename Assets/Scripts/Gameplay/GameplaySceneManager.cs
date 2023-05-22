using Fsi.Runtime;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectCatch.Gameplay
{
    public class GameplaySceneManager : MbSingleton<GameplaySceneManager>
    {
        [Title("Character Builder Scenes")]

        [SerializeField]
        private SceneAsset characterBuilderScene;
        
        [Title("Battle Scenes")]

        [SerializeField]
        private SceneAsset trainerBattleScene;

        [SerializeField]
        private SceneAsset wildBattleScene;

        [Title("Battle Scenes", "Environments")]

        [SerializeField]
        private SceneAsset defaultEnvironment;

        [Title("Map Scenes")]

        [SerializeField]
        private SceneAsset mapScene;

        public void StartCharacterBuilderScene()
        {
            SceneManager.LoadScene(characterBuilderScene.name, LoadSceneMode.Single);
        }

        public void StartTrainerBattleScene()
        {
            SceneManager.LoadScene(trainerBattleScene.name, LoadSceneMode.Single);
            SceneManager.LoadSceneAsync(defaultEnvironment.name, LoadSceneMode.Additive);
        }

        public void StartMapScene()
        {
            SceneManager.LoadScene(mapScene.name, LoadSceneMode.Single);
        }
    }
}
