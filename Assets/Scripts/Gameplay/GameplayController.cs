using Fsi.Runtime;
using ProjectCatch.Data.Pokemon.Types;
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

        #endregion
    }
}
