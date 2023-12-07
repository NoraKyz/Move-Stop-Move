using System;
using System.Collections;
using System.Collections.Generic;
using _Framework.Pool.Scripts;
using _Game.Utils;
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
        [SerializeField] protected Weapon.Weapon currentWeapon;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float moveSpeed;

        [SerializeField] private List<Character> enemiesInRange = new List<Character>();
        private string _currentAnimName;
        private bool _attackAble;
        private bool _isDie;
        
        #region Getter
        public float AttackRange => attackRange;
        public bool HasEnemyInRange => enemiesInRange.Count > 0;
        public bool AttackAble => _attackAble;
        public bool IsDie => _isDie;

        #endregion
        
        private void OnEnable()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            _isDie = false;
            _attackAble = true;
            attackRange = Constants.DefaultAttackRange;
            OnCharacterDespawn += OnEnemyExitRange;
            
            currentWeapon.OnInit(this);
        }
        public void LookAt(Vector3 target)
        {
            model.LookAt(target);
        }
        
        #region Attack

        public void Attack(Vector3 targetPos)
        {
            currentWeapon.SpawnBullet(targetPos);
            StartCoroutine(ResetAttack());
        }
        private IEnumerator ResetAttack()
        {
            _attackAble = false;
            currentWeapon.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            _attackAble = true;
            currentWeapon.gameObject.SetActive(true);
        }
        public Vector3 GetRandomEnemyPos()
        {
            int randomIndex = Random.Range(0, enemiesInRange.Count);
            return enemiesInRange[randomIndex].TF.position;
        }

        #endregion

        public virtual void OnHit()
        {
            _isDie = true;
            OnCharacterDespawn?.Invoke(this);
        }
        public virtual void OnDeath()
        {
            enemiesInRange.Clear();
            OnCharacterDespawn -= OnEnemyExitRange;
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
            if (enemy.IsDie)
            {
                return;
            }
            
            enemiesInRange.Add(enemy);
        }
        public void OnEnemyExitRange(Character enemy)
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
