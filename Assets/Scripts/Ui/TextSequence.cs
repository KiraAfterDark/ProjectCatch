using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCatch
{
    public class TextSequence
    {
        public Queue<string> sequence;
        
        public string Current { get; private set; }

        public TextSequence(TextSequenceData data)
        {
            sequence = new Queue<string>(data.Sequence);
        }

        public bool Advance(out string text)
        {
            return sequence.TryDequeue(out text);
        }
    }
}
