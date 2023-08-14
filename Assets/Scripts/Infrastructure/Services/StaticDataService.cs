using System.Collections.Generic;
using System.Linq;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;

        
        public void LoadEnemies()
        {
            _enemies = Resources.LoadAll<EnemyStaticData>("StaticData/Enemies")
                .ToDictionary(x => x.EnemyTypeId, x => x);
            
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
                .ToDictionary(x => x.LevelKey, x => x);
        }

        public EnemyStaticData ForEnemies(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData) 
                ? enemyStaticData 
                : null;

        public LevelStaticData ForLevel(string sceneKey)=>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData) 
                ? levelStaticData 
                : null;
    }
}