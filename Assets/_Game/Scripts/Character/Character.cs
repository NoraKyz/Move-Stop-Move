using System;
using System.Collections.Generic;
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
        private List<Character> EnemiesInRange { get; } = new List<Character>();
        public bool HasEnemyInRange => EnemiesInRange.Count > 0;
        private void Start()
        {
            OnInit();
        }
        protected virtual void OnInit() { }
        protected virtual void OnDespawn() { }
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
        public void OnEnemyEnterRange(Character enemy)
        {
            EnemiesInRange.Add(enemy);
        }
        public void OnEnemyExitRange(Character enemy)
        {
            EnemiesInRange.Remove(enemy);
        }
    }
}
