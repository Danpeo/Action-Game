using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public ProgressState PlayerState;
        public WorldData WorldData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new ProgressState();
        }
    }
}