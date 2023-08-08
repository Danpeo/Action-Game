using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public ProgressState PlayerState;
        public WorldData WorldData;
        public PlayerStats PlayerStats;
        public KillData KillData { get; set; }


        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new ProgressState();
            PlayerStats = new PlayerStats();
            KillData = new KillData();
        }
    }
}