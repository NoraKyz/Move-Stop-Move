using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterAttack : MonoBehaviour
    {
        #region Config

        [Header("References")] 
        [SerializeField] private Character owner;
        [SerializeField] private CharacterSight characterSight;
        [SerializeField] private Weapon.Weapon currentWeapon;

        private bool _isAttackAble;
        private List<Character> EnemiesInRange => characterSight.EnemiesInRange;
        
        public bool IsAttackAble => _isAttackAble;
        public bool HasEnemyInRange => EnemiesInRange.Count > 0;

        #endregion

        #region Init
        
        public void OnInit()
        {
            _isAttackAble = true;
            
            characterSight.OnInit();
        }

        #endregion
        
        // Random enemy pos from list enemy in range
        public Character GetRandomEnemyInRange()
        {
            int randomIndex = Random.Range(0, EnemiesInRange.Count);
            return EnemiesInRange[randomIndex];
        }
        
        // Use current weapon spawn bullet to target position, then reset attack
        public void Attack(Vector3 targetPos)
        {
            currentWeapon.SpawnBullet(owner, targetPos);
            StartCoroutine(ResetAttack());
        }
        
        // First disable attack, then wait 1.5s to enable attack
        private IEnumerator ResetAttack()
        {
            _isAttackAble = false;
            currentWeapon.SetVisible(false);
            
            yield return new WaitForSeconds(1.5f);

            _isAttackAble = true;
            currentWeapon.SetVisible(true);
        }
    }
}