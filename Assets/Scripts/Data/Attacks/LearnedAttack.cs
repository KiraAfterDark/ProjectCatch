using System;
using UnityEngine;

namespace ProjectMaster.Data.Attacks
{
    [Serializable]
    public class LearnedAttack
    {
        [SerializeField]
        private int level = 1;

        public int Level => level;

        [SerializeField]
        private Attack attack = null;

        public Attack Attack => attack;
    }
}
