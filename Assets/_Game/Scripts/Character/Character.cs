using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Utils;
using _Pattern.Event.Scripts;
using _Pattern.Pool.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Character : PoolUnit
    {
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] protected Transform model;
        [SerializeField] protected AttackRange.AttackDetector attackDetectorDetector;
        
        [Header("Config")]
        [SerializeField] protected Weapon.Weapon currentWeapon;
        [SerializeField] private List<Character> enemiesInRange = new List<Character>();

        private Action<object> _onCharacterDie;
        
        private bool _isDie;
        private bool _isAttackAble;
        private float _attackRange;
        
        private string _currentAnimName;
        
        #region Getter 
        public float AttackRange => _attackRange;
        public bool HasEnemyInRange => enemiesInRange.Count > 0;
        public bool IsAttackAble => _isAttackAble;
        public bool IsDie => _isDie;
        
        #endregion
        public virtual void OnInit()
        {
            enemiesInRange.Clear();
            
            _isDie = false;
            _isAttackAble = true;
            _attackRange = Constants.DefaultAttackRange;

            RegisterEvents();
            ResetModelRotation();
            
            currentWeapon.OnInit(this);
            attackDetectorDetector.OnInit(this);
        }
        public virtual void OnHit()
        {
            _isDie = true;
            
            this.PostEvent(EventID.OnCharacterDie, this);
            
            RemoveEvents();
        }
        public virtual void OnDespawn()
        {
            
        }

        protected virtual void RegisterEvents()
        {
            _onCharacterDie = (param) => OnEnemyExitRange((Character) param);
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        protected virtual void RemoveEvents()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        #region Attack

        public void Attack(Vector3 targetPos)
        {
            currentWeapon.SpawnBullet(targetPos);
            StartCoroutine(ResetAttack());
        }
        private IEnumerator ResetAttack()
        {
            _isAttackAble = false;
            currentWeapon.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(1.5f);
            
            _isAttackAble = true;
            currentWeapon.gameObject.SetActive(true);
        }
        public Vector3 GetRandomEnemyPos()
        {
            int randomIndex = Random.Range(0, enemiesInRange.Count);
            return enemiesInRange[randomIndex].TF.position;
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
        
        #endregion
        
        public void LookAtTarget(Vector3 target)
        {
            model.LookAt(target);
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
