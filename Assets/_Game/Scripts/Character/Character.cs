using System.Collections.Generic;
using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] private Transform firePoint;
        
        [Header("Config")]
        [SerializeField] protected Weapon.Weapon weapon;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float attackRange;
        [SerializeField] protected List<Character> enemiesInRange;
        
        private string _currentAnimName;
        public List<Character> EnemiesInRange => enemiesInRange;
        public bool HasEnemyInRange => enemiesInRange.Count > 0;
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
        private void Start()
        {
            OnInit();
        }
        protected virtual void OnInit() { }
        protected void LookAt(Vector3 target)
        {
            TF.LookAt(target);
        }
        public void OnEnemyEnterRange(Character enemy)
        {
            EnemiesInRange.Add(enemy);
        }
        public void OnEnemyExitRange(Character enemy)
        {
            EnemiesInRange.Remove(enemy);
        }
        public void Attack(Character enemy)
        {
            LookAt(enemy.TF.position);
            //SimplePool.Spawn(weaponPrefab, , Quaternion.identity);
        }

    }
}
