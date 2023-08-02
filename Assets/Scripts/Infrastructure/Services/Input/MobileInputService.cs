using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 MoveAxis => SimpleVectorAxis();
        public override Vector2 LookAxis { get; }
    }
}