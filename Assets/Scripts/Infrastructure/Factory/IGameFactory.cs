using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject PlayerGameObject { get; }
        event Action PlayerCreated;
        GameObject CreatePlayer(GameObject at);
        GameObject CreateHud();
        void Cleanup();
    }
}