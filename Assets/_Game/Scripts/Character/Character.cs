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
        public List<Character> EnemiesInRange => _enemiesInRange;
        public bool HasEnemyInRange => _enemiesInRange.Count > 0;

        #endregion
        
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
        
        #region Attack

        public void Attack()
        {
            Vector3 enemyPos = GetRandomEnemyPos();
            
            LookAt(enemyPos);
            StartCoroutine(Throw(enemyPos));
        }
        private IEnumerator Throw(Vector3 target)
        {
            yield return new WaitForSeconds(0.2f);
            weapon.SpawnBullet(target);
        }   
        private Vector3 GetRandomEnemyPos()
        {
            int randomIndex = Random.Range(0, _enemiesInRange.Count);
            return _enemiesInRange[randomIndex].TF.position;
        }

        #endregion
        
        public void OnHit()
        {
            throw new System.NotImplementedException();
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
