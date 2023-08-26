using System.Threading.Tasks;
using Infrastructure.Services;

namespace UI.Services.Factory
{
    public interface IUiFactory : IService
    {
        void CreateShop();
        Task CreateUiRoot();
    }
}