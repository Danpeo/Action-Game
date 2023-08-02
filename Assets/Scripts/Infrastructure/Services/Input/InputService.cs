using UnityEngine;

namespace Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string HorizontalMove = "Horizontal";
        protected const string VerticalMove = "Vertical";
        private const string Button = "Fire";
        public abstract Vector2 MoveAxis { get; }
        public abstract Vector2 LookAxis { get; }

        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(Button);
        
        protected static Vector2 SimpleVectorAxis() => 
            new(SimpleInput.GetAxis(HorizontalMove), SimpleInput.GetAxis(VerticalMove));
    }
}