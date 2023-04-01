using Fsi.Runtime;
using ProjectMaster.Data.Pokemon.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster.Gameplay
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
