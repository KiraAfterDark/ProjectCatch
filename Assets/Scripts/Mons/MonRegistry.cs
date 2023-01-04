using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCatch.Mons
{
    [CreateAssetMenu(fileName = "New Mon Registry", menuName = "Project Catch/Mons/Registry")]
    public class MonRegistry : ScriptableObject
    {
        [Serializable]
        private struct Entry
        {
            public int id;
            [FormerlySerializedAs("monReference")]
            public MonBase monBase;
        }

        [SerializeField]
        private List<Entry> entries = new List<Entry>();

        public Dictionary<int, MonBase> Dex { get; private set; } = new Dictionary<int, MonBase>();

        public void Init()
        {
            Dex = new Dictionary<int, MonBase>();
            foreach (Entry entry in entries)
            {
                Dex.Add(entry.id, entry.monBase);
            }
            
            Debug.Log("Mon Registry Successfully Initialized");
        }
    }
}
