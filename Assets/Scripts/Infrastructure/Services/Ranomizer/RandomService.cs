using UnityEngine;

namespace Infrastructure.Services.Ranomizer
{
    public class RandomService : IRandomService
    {
        public int Next(int min, int max) => 
            Random.Range(min, max);
    }
}