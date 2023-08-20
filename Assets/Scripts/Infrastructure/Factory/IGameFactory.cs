using System.Collections.Generic;
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
        GameObject CreatePlayer(Vector3 at);
        GameObject CreateHud();
        void CreateSpawner(Vector3 spawnerPosition, string spawnerId, EnemyTypeId spawnerEnemyTypeId);
        void Cleanup();
        GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent);
        LootPiece CreateLoot();
    }
}