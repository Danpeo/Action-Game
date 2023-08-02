using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        private const string HorizontalMouse = "Mouse X";
        private const string VerticalMouse = "Mouse Y";
        public override Vector2 MoveAxis
        {
            get
            {
                Vector2 axis = SimpleVectorAxis();
                if (axis == Vector2.zero)
                {
                    axis = UnityMoveAxis();
                }

                return axis;
            }
        }
        
        public override Vector2 LookAxis => UnityLookAxis();
        
        private static Vector2 UnityMoveAxis() => new(UnityEngine.Input.GetAxis(HorizontalMove), UnityEngine.Input.GetAxis(VerticalMove));
        private static Vector2 UnityLookAxis() => new(UnityEngine.Input.GetAxis(HorizontalMouse), UnityEngine.Input.GetAxis(VerticalMouse));
    }
}