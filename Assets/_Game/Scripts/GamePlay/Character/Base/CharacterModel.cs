using _SDK.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterModel : GameUnit
    {
        #region Config

        [Header("References")]
        [SerializeField] private Animator anim;
        [SerializeField] protected Transform model;
        
        private string _currentAnimName;

        #endregion

        #region Init

        public void OnInit()
        {
            model.localRotation = Quaternion.identity;
        }

        #endregion

        public void LookAtTarget(Vector3 targetPos)
        {
           Vector3 lookPos = targetPos - model.position;
           lookPos.y = 0;
           TF.forward = lookPos.normalized;
        }
        
        public void ChangeAnim(string animName)
        {
            if (_currentAnimName == animName)
            { 
                return;
            }
        
            anim.ResetTrigger(animName);
            _currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }
}