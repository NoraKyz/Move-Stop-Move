using _Pattern.Singleton;
using UnityEngine;

namespace _Game.Scripts.Input
{
    public class InputSystem : Singleton<InputSystem>
    {
        [SerializeField] private FloatingJoystick joystick;

        public float HorizontalAxis => joystick.Horizontal;
        public float VerticalAxis => joystick.Vertical;
        
        public void GetInputEntity()
        {
            if(joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
        }
        public bool HasInput()
        {
            if(joystick == null)
            {
                return false;
            }
            
            return Vector2.Distance(joystick.Direction, Vector2.zero) > 0.1f;
        }
    }

    public static class InputManager
    {
        // this is "shortcut" for InputSystem.Instance
        
        public static float HorizontalAxis => InputSystem.Instance.HorizontalAxis;
        public static float VerticalAxis => InputSystem.Instance.VerticalAxis;
        public static bool HasInput() => InputSystem.Instance.HasInput();
        public static void GetInputEntity() => InputSystem.Instance.GetInputEntity();
    }
}
