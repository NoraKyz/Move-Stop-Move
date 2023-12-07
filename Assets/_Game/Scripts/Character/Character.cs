using System;
using System.Collections;
using System.Collections.Generic;
using _Framework.Pool.Scripts;
using _Game.Utils;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Character : GameUnit
    {
        public static event Action<Character> OnCharacterDie;
        
        public UnityEvent<int> onScoreChange;
        
        [Header("Components")]
        [SerializeField] private Animator anim;
        [SerializeField] private Transform model;
        
        [Header("Skins")]
        [SerializeField] private Transform weaponSkin;
        
        [Header("Config")]
        [SerializeField] protected Weapon.Weapon currentWeapon;
        [SerializeField] protected float moveSpeed;
        [SerializeField] private List<Character> enemiesInRange = new List<Character>();

        private float _attackRange;
        private bool _isAttackAble;
        private bool _isDie;
        
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
            _isDie = false;
            _isAttackAble = true;
            _attackRange = Constants.DefaultAttackRange;
            
            OnCharacterDie += OnEnemyExitRange;
            
            SetScore(0);
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

        #endregion

        public virtual void OnHit()
        {
            _isDie = true;
            OnCharacterDie?.Invoke(this);
        }
        public virtual void OnDespawn()
        {
            OnCharacterDie -= OnEnemyExitRange;
            enemiesInRange.Clear();
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
        public void SetScore(int score)
        {
            if (score < 0)
            {
                score = 0;
            }
            
            _score = score;
            onScoreChange?.Invoke(score);
        }
    }
}
