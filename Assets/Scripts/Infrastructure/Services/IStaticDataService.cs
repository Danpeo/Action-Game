using StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemies(EnemyTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
    }
}