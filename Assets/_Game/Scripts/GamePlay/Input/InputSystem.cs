using _Pattern.Singleton;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Input
{
    public class InputSystem : Singleton<InputSystem>
    {
        [Header("References")]
        [SerializeField] private FloatingJoystick joystick;

        public float HorizontalAxis => joystick.Horizontal;
        public float VerticalAxis => joystick.Vertical;
        
        public void GetInputEntity()
        {
            if (joystick != null)
            {
                return;
            }
            
            joystick = FindObjectOfType<FloatingJoystick>();
        }
        public bool HasInput()
        {
            return joystick.Direction != Vector2.zero;
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
