using System.Collections.Generic;
using _SDK.UI.Base;
using _SDK.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterAttack : MonoBehaviour
    {
        private const float ATTACK_COOLDOWN = 2f;
        
        #region Config

        [Header("References")] 
        [SerializeField] private Character owner;
        [SerializeField] private CharacterSight characterSight;
        [SerializeField] private Weapon.Weapon currentWeapon;

        private bool _isAttackAble;
        private CountDownTimer _countDownTimer = new();
        private List<Character> EnemiesInRange => characterSight.EnemiesInRange;
        
        
        public bool IsAttackAble => _isAttackAble;
        public bool HasEnemyInRange => EnemiesInRange.Count > 0;

        #endregion

        public void OnInit()
        {
            _isAttackAble = true;
            
            characterSight.OnInit();
        }

        private void Update()
        {
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                return;
            }
            
            _countDownTimer.Execute();
        }

        public void SetWeapon(Weapon.Weapon weapon)
        {
            currentWeapon = weapon;
        }
        
        public Character GetEnemyNearest()
        {
            Character enemyNearest = EnemiesInRange[0];
            
            for (int i = 1; i < EnemiesInRange.Count; i++)
            {
                if (Vector3.Distance(owner.TF.position, EnemiesInRange[i].TF.position) <
                    Vector3.Distance(owner.TF.position, enemyNearest.TF.position))
                {
                    enemyNearest = EnemiesInRange[i];
                }
            }
            
            return enemyNearest;
        }
        
        public void Attack(Vector3 targetPos)
        {
            currentWeapon.SpawnBullet(owner, targetPos);
            ResetAttack();
        }

        private void ResetAttack()
        {
            SetAttackAble(false);
            _countDownTimer.Start(EnableAttack, ATTACK_COOLDOWN);
        }
        
        private void SetAttackAble(bool isAttackAble)
        {
            _isAttackAble = isAttackAble;
            currentWeapon.SetVisible(_isAttackAble);
        }
        
        private void EnableAttack()
        {
            SetAttackAble(true);
        }
    }
}