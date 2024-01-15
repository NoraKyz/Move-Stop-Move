using System;
using System.Collections;
using System.Collections.Generic;
using _Framework.Pool.Scripts;
using _Game.Utils;
using _Pattern.Event.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] protected Transform model;
        [SerializeField] protected AttackRange attackRangeDetector;
        
        [Header("Config")]
        [SerializeField] protected Weapon.Weapon currentWeapon;
        [SerializeField] protected float moveSpeed;
        [SerializeField] private List<Character> enemiesInRange = new List<Character>();

        private Action<object> _onCharacterDie;
        
        private bool _isDie;
        private bool _isAttackAble;
        private float _attackRange;
        
        private string _currentAnimName;

        private int _score;
        
        #region Getter 
        public float AttackRange => _attackRange;
        public bool HasEnemyInRange => enemiesInRange.Count > 0;
        public bool IsAttackAble => _isAttackAble;
        public bool IsDie => _isDie;
        public int Score => _score;

        #endregion
        public virtual void OnInit()
        {
            enemiesInRange.Clear();
            
            _isDie = false;
            _isAttackAble = true;
            _attackRange = Constants.DefaultAttackRange;

            _score = 0;
            
            RegisterEvents();
            ResetModelRotation();
            
            currentWeapon.OnInit(this);
            attackRangeDetector.OnInit(this);
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
        public void SetScore(int score)
        {
            if (score < 0)
            {
                score = 0;
            }
            
            _score = score;
        }
    }
}
