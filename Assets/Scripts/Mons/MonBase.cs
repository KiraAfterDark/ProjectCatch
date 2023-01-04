using System;
using System.Collections.Generic;
using ProjectCatch.Mons.Attacks;
using ProjectCatch.Mons.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectCatch.Mons
{
    [CreateAssetMenu(fileName = "New Mon Reference", menuName = "Project Catch/Mons/Mon")]
    public class MonBase : ScriptableObject
    {
        [Title("Data")]

        [Min(1)]
        [SerializeField]
        private int id;

        public int Id => id;

        [FormerlySerializedAs("name")]
        [SerializeField]
        private string species = "";

        public string Species => species;

        [SerializeField]
        private MonType type;

        public MonType Type => type;

        [SerializeField]
        private ExperienceGrowth.ExperienceType experienceType;

        public ExperienceGrowth.ExperienceType ExperienceType => experienceType;

        [Title("Stats")]

        [SerializeField]
        private StatBlock baseStats;

        public StatBlock BaseStats => baseStats;

        [Serializable]
        private struct LevelAttack
        {
            public int level;
            public AttackReference attack;
        }
        
        [Title("Attacks")]

        [SerializeField]
        private List<LevelAttack> learnedAttacks = new List<LevelAttack>();
        
        // Compatible TMs/HMs

        [Title("Visuals")]

        [SerializeField]
        private MonObject monObject;

        public MonObject MonObject => monObject;

        [SerializeField]
        private Sprite monSprite;

        public Sprite MonSprite => monSprite;
    }
}
