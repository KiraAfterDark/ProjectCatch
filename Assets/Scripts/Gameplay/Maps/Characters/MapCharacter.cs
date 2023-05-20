using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectCatch.Gameplay.Maps.Characters
{
    public class MapCharacter : MonoBehaviour
    {
        [Min(0)]
        [SerializeField]
        private float moveTime = 0.5f;

        [SerializeField]
        private Ease moveEase = Ease.Linear;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private string moveParam = "Move";

        private float totalDistanceMoved;

        private int encounterChecks = 1;
        private float encounterCheckDistance = 2;

        public bool CanMove { get; private set; } = true;

        private Tween moveTween;
        
        public void Move(MapNode node, Action<MapNode> callback)
        {
            if (!CanMove)
            {
                return;
            }
            
            CanMove = false;
            
            animator.SetBool(moveParam, true);

            float distance = Vector3.Distance(transform.position, node.WorldPosition);
            moveTween = transform.DOMove(node.WorldPosition, moveTime)
                                 .SetEase(moveEase)
                                 .OnUpdate(() => EncounterCheck(distance))
                                 .OnComplete(() =>
                                 {
                                     CanMove = true;
                                     totalDistanceMoved += distance;
                                     animator.SetBool(moveParam, false);
                                     callback?.Invoke(node);
                                 });
        }

        private void EncounterCheck(float totalDistance)
        {
            float traveled = totalDistanceMoved + moveTween.position * totalDistance;

            if (traveled > encounterChecks * encounterCheckDistance)
            {
                encounterChecks++;
                float roll = Random.Range(0, 1.0f);
                if (roll < -1f)
                {
                    Debug.Log("Battle!");
                    moveTween?.Kill();
                }
                else
                {
                    Debug.Log("No battle");
                }
            }
        }
    }
}
