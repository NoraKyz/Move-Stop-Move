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
            
            this.RegisterListener(EventID.OnCharacterDie, (param) => OnEnemyExitRange((Character) param));
            
            currentWeapon.OnInit(this);
            attackRangeDetector.OnInit(this);
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
        
        public virtual void OnHit()
        {
            _isDie = true;
            this.PostEvent(EventID.OnCharacterDie, this);
        }
        public virtual void OnDespawn()
        {
            this.RegisterListener(EventID.OnCharacterDie, (param) => OnEnemyExitRange((Character) param));
            enemiesInRange.Clear();
        }
        public void LookAtTarget(Vector3 target)
        {
            model.LookAt(target);
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
