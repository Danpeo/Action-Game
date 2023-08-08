using System;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Logic;
using StaticData;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private EnemyTypeId _enemyTypeId;

        private string _id;
        public bool _isDead;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
                _isDead = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isDead)
                progress.KillData.ClearedSpawners.Add(_id);
        }

        private void Spawn()
        {
            GameObject enemy = _gameFactory.CreateEnemy(_enemyTypeId, transform);

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