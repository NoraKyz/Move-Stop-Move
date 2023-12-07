using System;
using System.Collections;
using System.Collections.Generic;
using _Framework.Pool.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        public static event Action<Character> OnCharacterDespawn;
        
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] private Transform model;
        
        [Header("Skins")]
        [SerializeField] private Transform weaponSkin;
        
        [Header("Config")]
        [SerializeField] protected Weapon.Weapon weapon;
        [SerializeField] protected float size;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float moveSpeed;

        private List<Character> _enemiesInRange = new List<Character>();
        private string _currentAnimName;
        private bool _attackAble;
        
        #region Getter

        public float Size => size;
        public float AttackRange => attackRange;
        public List<Character> EnemiesInRange => _enemiesInRange;
        public bool HasEnemyInRange => _enemiesInRange.Count > 0;
        public bool AttackAble => _attackAble;

        #endregion
        
        private void Start()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            _attackAble = true;
            weapon.OnInit(this);
            OnCharacterDespawn += OnEnemyExitRange;
        }
        public void LookAt(Vector3 target)
        {
            model.LookAt(target);
        }
        
        #region Attack

        public void Attack(Vector3 targetPos)
        {
            weapon.SpawnBullet(targetPos);
            StartCoroutine(ResetAttack());
        }
        private IEnumerator ResetAttack()
        {
            _attackAble = false;
            yield return new WaitForSeconds(1.5f);
            _attackAble = true;
        }
        public Vector3 GetRandomEnemyPos()
        {
            int randomIndex = Random.Range(0, _enemiesInRange.Count);
            return _enemiesInRange[randomIndex].TF.position;
        }

        #endregion

        public virtual void OnHit()
        {
            OnCharacterDespawn?.Invoke(this);
            OnCharacterDespawn -= OnEnemyExitRange;
        }

        public virtual void Despawn()
        {
            SimplePool.Despawn(this);
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
