using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;
        
        private const float MinimalDistance = 0.1f;

        private void Update()
        {
            if (ShouldMove())
                _animator.StartMoving();
            else
                _animator.StopMoving();
        }

        private bool ShouldMove() => 
            _agent.velocity.magnitude > MinimalDistance && _agent.remainingDistance > _agent.radius;
    }
}