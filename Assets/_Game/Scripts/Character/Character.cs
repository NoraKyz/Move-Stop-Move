using System.Collections;
using System.Collections.Generic;
using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] private Transform model;
        [SerializeField] private Transform firePoint;
        
        [Header("Config")]
        [SerializeField] protected Weapon.Weapon weapon;
        [SerializeField] protected float size;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float moveSpeed;

        private List<Character> _enemiesInRange = new List<Character>();
        
        private string _currentAnimName;

        #region Getter

        public float Size => size;
        public float AttackRange => attackRange;
        public Transform FirePoint => firePoint;
        public List<Character> EnemiesInRange => _enemiesInRange;
        public bool HasEnemyInRange => _enemiesInRange.Count > 0;

        #endregion
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
        protected virtual void OnInit()
        {
            weapon.OnInit(this);
        }
        protected void LookAt(Vector3 target)
        {
            model.LookAt(target);
        }
        public void Attack()
        {
            Vector3 enemyPos = GetRandomEnemy().TF.position;
            
            LookAt(enemyPos);
            StartCoroutine(Throw(enemyPos));
        }
        private IEnumerator Throw(Vector3 target)
        {
            yield return new WaitForSeconds(0.2f);
            weapon.SpawnBullet(target);
        }   
        public Character GetRandomEnemy()
        {
            int randomIndex = Random.Range(0, _enemiesInRange.Count);
            return _enemiesInRange[randomIndex];
        }
        public void OnHit()
        {
            throw new System.NotImplementedException();
        }
        public void OnEnemyEnterRange(Character enemy)
        {
            _enemiesInRange.Add(enemy);
        }
        public void OnEnemyExitRange(Character enemy)
        {
            _enemiesInRange.Remove(enemy);
        }
    }
}
