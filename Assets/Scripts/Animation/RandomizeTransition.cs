using UnityEngine;

namespace ProjectCatch
{
    public class RandomizeTransition : StateMachineBehaviour
    {
        [SerializeField]
        private int numberOfTransitions = 0;

        [SerializeField]
        private string randomizeParam = "Randomize";
        
         // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            int rand = Random.Range(0, numberOfTransitions);
            animator.SetInteger(randomizeParam, rand);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetInteger(randomizeParam, -1);
        }
    }
}
