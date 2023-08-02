using Infrastructure.Data;
using Infrastructure.Services;

namespace Infrastructure.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}