using System.Collections.Generic;
using System.Linq;
using StaticData;
using StaticData.Windows;
using UI.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataEnemiesPath = "StaticData/Enemies";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowStaticData";
        private Dictionary<EnemyTypeId, EnemyStaticData> _enemies;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;


        public void LoadEnemies()
        {
            _enemies = Resources
                .LoadAll<EnemyStaticData>(StaticDataEnemiesPath)
                .ToDictionary(x => x.EnemyTypeId, x => x);

            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .WindowConfigs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public EnemyStaticData ForEnemies(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStaticData enemyStaticData)
                ? enemyStaticData
                : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData)
                ? levelStaticData
                : null;

        public WindowConfig ForWindows(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
    }
}