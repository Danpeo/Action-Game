using Infrastructure.Services;

namespace UI.Services.Factory
{
    public interface IUiFactory : IService
    {
        void CreateShop();
        void CreateUiRoot();
    }
}