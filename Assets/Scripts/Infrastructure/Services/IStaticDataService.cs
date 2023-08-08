using StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyStaticData ForEnemies(EnemyTypeId typeId);
    }
}