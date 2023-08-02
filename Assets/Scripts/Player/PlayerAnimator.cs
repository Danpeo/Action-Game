using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        
        private static readonly int MoveHash = Animator.StringToHash("Walking");

        private void Update()
        {
            _animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }
    }
}