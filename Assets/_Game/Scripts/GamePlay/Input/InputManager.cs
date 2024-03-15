using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Input
{
    public class InputManager : GameService
    {
        #region Config

        [Header("References")]
        [SerializeField] private FloatingJoystick joystick;

        public float HorizontalAxis => joystick.Horizontal;
        public float VerticalAxis => joystick.Vertical;

        #endregion
        
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
}
