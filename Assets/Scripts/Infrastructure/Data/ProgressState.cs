using System;

namespace Infrastructure.Data
{
    [Serializable]
    public class ProgressState
    {
        public float CurrentHp;
        public float MaxHp;
        public void ResetHp() => 
            CurrentHp = MaxHp;
    }
}