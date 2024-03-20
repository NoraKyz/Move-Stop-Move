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
        
        protected Coroutine resetAttackCoroutine;
        
        public bool IsAttackAble => _isAttackAble;
        public bool HasEnemyInRange => EnemiesInRange.Count > 0;

        #endregion

        public void OnInit()
        {
            _isAttackAble = true;
            
            characterSight.OnInit();
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
            resetAttackCoroutine = StartCoroutine(ResetAttack());
        }

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