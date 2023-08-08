using System;
using Infrastructure.Data;
using Infrastructure.Factory;
using Infrastructure.Services.Ranomizer;
using UnityEngine;

namespace Enemy
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

        private void SpawnLoot()
        {
            GameObject loot = _gameFactory.CreateLoot();
            loot.transform.position = transform.position;

            var lootItem = new Loot()
            {
                Value = _randomService.Next(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int lootMin, int lootMax)
        {
            _lootMin = lootMin;
            _lootMax = lootMax;
        }
    }
}