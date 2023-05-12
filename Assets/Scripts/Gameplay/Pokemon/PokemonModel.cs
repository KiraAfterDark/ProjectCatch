using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCatch.Gameplay.Pokemon
{
    public class PokemonModel : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public Animator Animator => animator;
    }
}
