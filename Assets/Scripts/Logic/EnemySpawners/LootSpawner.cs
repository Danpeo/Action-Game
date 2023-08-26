using System.Threading.Tasks;
using Enemy;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.Ranomizer;
using UnityEngine;

namespace Logic.EnemySpawners
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        private IGameFactory _gameFactory;
        private IRandomService _randomService;
        private int _lootMax;
        private int _lootMin;

        public void Construct(IGameFactory gameFactory, IRandomService randomService)
        {
            _gameFactory = gameFactory;
            _randomService = randomService;
        }

        private void Start()
        {
            EnemyDeath.DeathOccured += SpawnLoot;
        }

        public void SetLoot(int lootMin, int lootMax)
        {
            _lootMin = lootMin;
            _lootMax = lootMax;
        }

        private async void SpawnLoot()
        {
            LootPiece loot = await _gameFactory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();
            
            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _randomService.Next(_lootMin, _lootMax)
            };
        }
    }
}