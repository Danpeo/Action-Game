using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 LookAxis { get; }

        bool IsAttackButtonUp();
    }
}