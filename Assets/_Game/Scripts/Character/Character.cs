using System;
using _Framework.Pool.Scripts;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] private Transform model;
        
        [Header("Config")]
        [SerializeField] protected float moveSpeed;
        
        private string _currentAnimName;

        private void Start()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual void OnDespawn()
        {
            
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
        
        protected void LookAt(Vector3 target)
        {
            model.LookAt(target);
        }
    }
}
