using System.Collections.Generic;
using _SDK.UI.Base;
using _SDK.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterAttack : MonoBehaviour
    {
        private const float AttackCoolDown = 1.5f;
        
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
        
        public Character GetRandomEnemyInRange()
        {
            int randomIndex = Random.Range(0, EnemiesInRange.Count);
            return EnemiesInRange[randomIndex];
        }
        
        public void Attack(Vector3 targetPos)
        {
            currentWeapon.SpawnBullet(owner, targetPos);
            ResetAttack();
        }

        private void ResetAttack()
        {
            _isAttackAble = false;
            currentWeapon.SetVisible(false);
            
            _countDownTimer.Start(() => {
                _isAttackAble = true;
                currentWeapon.SetVisible(true);
            }, AttackCoolDown);
        }
    }
}