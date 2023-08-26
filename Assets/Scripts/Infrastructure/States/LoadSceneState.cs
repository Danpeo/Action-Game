using System.Threading.Tasks;
using CameraLogic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Player;
using StaticData;
using UI.Elements;
using UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadSceneState : IPayLoadState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUiFactory _uiFactory;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService, IStaticDataService staticDataService,
            IUiFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private async void OnLoaded()
        {
            await InitializeUiRoot();

            await InitializeGameWorld();

            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitializeUiRoot() =>
            await _uiFactory.CreateUiRoot();

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private async Task InitializeGameWorld()
        {
            LevelStaticData levelData = GetLevelStaticData();

            await InitializeSpawners(levelData);

            GameObject player = await InitializePlayer(levelData);

            await InitializeHud(player);

            CameraFollow(player);
        }

        private LevelStaticData GetLevelStaticData() => 
            _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

        private async Task<GameObject> InitializePlayer(LevelStaticData levelStaticData) => 
            await _gameFactory.CreatePlayer(levelStaticData.InitialPlayerPosition);

        private async Task InitializeSpawners(LevelStaticData levelStaticData)
        {
            foreach (EnemySpawnerData spawner in levelStaticData.EnemySpawners)
                await _gameFactory.CreateSpawner(spawner.Position, spawner.Id, spawner.EnemyTypeId);
        }

        private async Task InitializeHud(GameObject player)
        {
            GameObject hud = await _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>()
                .Construct(player.GetComponent<PlayerHealth>());
        }

        private void CameraFollow(GameObject player) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
    }
}