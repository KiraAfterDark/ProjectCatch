using Fsi.Runtime;
using ProjectCatch.Mons.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch.Battle
{
    [AddComponentMenu("Project Catch/Battle/Battle Controller")]
    public class BattleController : MbSingleton<BattleController>
    {
        [Title("Battle Settings")]

        [Required]
        [SerializeField]
        private TypeChart typeChart;

        [Title("Sockets")]

        [SerializeField]
        private Transform playerSocket;

        [SerializeField]
        private Transform playerMonSocket;

        [SerializeField]
        private Transform npcSocket;

        [SerializeField]
        private Transform npcMonSocket;

        private void OnDrawGizmos()
        {
            Vector3 size = new Vector3(1.0f, 0.2f, 1.0f);
            Vector3 offset = Vector3.up * 0.1f;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(playerSocket.transform.position + offset, size);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(playerMonSocket.transform.position + offset/2, size/2);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(npcSocket.transform.position + Vector3.up * 0.1f, size);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(npcMonSocket.transform.position + offset/2, size/2);
        }
    }
}
