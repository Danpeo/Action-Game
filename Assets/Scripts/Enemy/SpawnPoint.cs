using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        [FormerlySerializedAs("_enemyTypeId")] 
        public EnemyTypeId EnemyTypeId;
        public string Id { get; set; }

        private bool _isDead;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
                _isDead = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isDead)
                progress.KillData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject enemy = _gameFactory.CreateEnemy(EnemyTypeId, transform);

            _enemyDeath = enemy.GetComponent<EnemyDeath>();

            _enemyDeath.DeathOccured += OnDeathOccured;
        }

        private void OnDeathOccured()
        {
            if (_enemyDeath != null)
                _enemyDeath.DeathOccured -= OnDeathOccured;

            _isDead = true;
        }
    }
}