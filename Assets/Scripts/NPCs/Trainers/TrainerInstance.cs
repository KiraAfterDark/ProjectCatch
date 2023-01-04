using System.Collections;
using System.Collections.Generic;
using ProjectCatch.Mons;
using ProjectCatch.NPCs.Trainers;
using UnityEngine;

namespace ProjectCatch.NPCs.Trainers
{
    public class TrainerInstance
    {
        public TrainerBase TrainerBase { get; }

        public string Name => TrainerBase.Name;

        public List<MonInstance> Party { get; }

        public TrainerInstance(TrainerBase trainerBase, List<MonInstance> party)
        {
            TrainerBase = trainerBase;
            Party = party;
        }
    }
}
