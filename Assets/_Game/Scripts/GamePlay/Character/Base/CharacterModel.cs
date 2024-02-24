using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterModel : MonoBehaviour
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
            ResetModelRotation();
        }

        #endregion

        public void LookAtTarget(Vector3 targetPos)
        {
            model.LookAt(targetPos);
        }
        
        public void ResetModelRotation()
        {
            model.localRotation = Quaternion.identity;
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