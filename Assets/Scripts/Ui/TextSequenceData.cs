using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectMaster
{
    [CreateAssetMenu(menuName = "Text Sequence", fileName = "New Text Sequence")]
    public class TextSequenceData : ScriptableObject
    {
        [Title("Text Sequence")]

        [SerializeField]
        private List<string> sequence = new List<string>();

        public IEnumerable<string> Sequence => sequence;
    }
}
