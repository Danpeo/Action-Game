using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentMoveToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent _agent;
        private const float MinimalDistance = 1f;
        private Transform _playerTransform;
        private IGameFactory _gameFactory;

        public void Contruct(Transform playerTransform) => 
            _playerTransform = playerTransform;

        /*private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.PlayerGameObject != null)
                InitializePlayerTransform();
            else
                _gameFactory.PlayerCreated += PlayerCreated;
        }*/

        private void Update()
        {
            if (_playerTransform != null && PlayerNotReached())
                MoveToPlayer();
        }

        /*private void PlayerCreated() =>
            InitializePlayerTransform();*/

        /*private void InitializePlayerTransform() =>
            _playerTransform = _gameFactory.PlayerGameObject.transform;*/

        private void MoveToPlayer() =>
            _agent.destination = _playerTransform.position;

        private bool PlayerNotReached()
        {
            return Vector3.Distance(gameObject.transform.position, _playerTransform.position) >= MinimalDistance;
        }
    }
}