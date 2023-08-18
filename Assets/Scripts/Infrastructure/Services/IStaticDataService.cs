using StaticData;
using StaticData.Windows;
using UI.Services.Windows;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemies(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindows(WindowId windowId);
    }
}