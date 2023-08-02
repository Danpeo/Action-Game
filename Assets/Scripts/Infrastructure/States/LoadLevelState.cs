using System;
using CameraLogic;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Player;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadState<string>
    {
        private const string InitialPoint = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitializeGameWorld();

            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitializeGameWorld()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPoint);
            GameObject player = _gameFactory.CreatePlayer(initialPoint);

            InitializeHud(player);

            CameraFollow(player);
        }

        private void InitializeHud(GameObject player)
        {
            GameObject hud = _gameFactory.CreateHud();
            
            hud.GetComponentInChildren<ActorUI>()
                .Construct(player.GetComponent<PlayerHealth>());
        }

        private void CameraFollow(GameObject player) =>
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(player);
    }
}