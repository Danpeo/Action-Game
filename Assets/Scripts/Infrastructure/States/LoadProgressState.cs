using Infrastructure.Data;
using Infrastructure.SaveLoad;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("Main");
            progress.PlayerState.MaxHp = 50;
            progress.PlayerStats.Damage = 15;
            progress.PlayerStats.AttackRadius = 5.5f;
            progress.PlayerState.ResetHp();
            
            return progress;
        }
    }
}