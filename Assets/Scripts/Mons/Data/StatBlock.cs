using System;
using UnityEngine;

namespace ProjectCatch.Mons.Data
{
    [Serializable]
    public class StatBlock
    {
        [Min(0)]
        [SerializeField]
        private int hp;

        private int Hp => hp;

        [Min(0)]
        [SerializeField]
        private int attack;

        public int Attack => attack;

        [Min(0)]
        [SerializeField]
        private int defense;

        public int Defense => defense;

        [Min(0)]
        [SerializeField]
        private int spAttack;

        public int SpAttack => spAttack;

        [Min(0)]
        [SerializeField]
        private int spDefense;

        public int SpDefense => spDefense;

        [Min(0)]
        [SerializeField]
        private int speed;

        private int Speed => speed;
    }
}
