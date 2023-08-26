using System.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using StaticData.Windows;
using UI.Services.Windows;
using UI.Windows;
using UnityEngine;

namespace UI.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private const string UiRootPath = "UIRoot";
        private readonly IAssets _assets;
        private Transform _uiRoot;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        public UiFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }
        
        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindows(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            window.Construct(_progressService);
        }

        public async Task CreateUiRoot()
        {
            //_uiRoot = _assets.Instantiate(UiRootPath).transform;
            GameObject root = await _assets.Instantiate(UiRootPath);
            _uiRoot = root.transform;
        }
    }
}