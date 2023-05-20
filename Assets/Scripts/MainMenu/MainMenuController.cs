using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectCatch.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Title("Scene Control")]

        [SerializeField]
        private SceneAsset characterBuilderScene;
        
        public void SelectPlayButton()
        {
            SceneManager.LoadSceneAsync(characterBuilderScene.name);
        }
    }
}
