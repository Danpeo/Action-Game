using System.Collections.Generic;
using System.Threading.Tasks;
using Enemy;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using StaticData;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        Task<GameObject> CreatePlayer(Vector3 at);
        Task<GameObject> CreateHud();
        Task CreateSpawner(Vector3 spawnerPosition, string spawnerId, EnemyTypeId spawnerEnemyTypeId);
        void Cleanup();
        Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        Task<LootPiece> CreateLoot();
        Task WarmUp();
    }
}