using System;
using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using _SDK.Pool.Scripts;
using _SDK.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterSight : GameUnit
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private Character owner;
        [SerializeField] private List<Character> enemiesInRange = new ();
        
        public List<Character> EnemiesInRange => enemiesInRange;
        
        #endregion

        public void OnEnable()
        {
            Character.OnDeathAction += OnEnemyExitRange;
        }

        public void OnInit()
        {
            enemiesInRange.Clear();
            
            // Set default sight
            TF.localScale = Vector3.one * Constants.DEFAULT_ATTACK_RANGE;
        }
        
        public void OnDisable()
        {
            Character.OnDeathAction -= OnEnemyExitRange;
        } 

        private void OnTriggerEnter(Collider other)
        {
            Character character = Cache<Character>.GetComponent(other);
            OnEnemyEnterRange(character);
        }
        
        private void OnTriggerExit(Collider other)
        {
            Character character = Cache<Character>.GetComponent(other);
            OnEnemyExitRange(character);
        }

        private void OnEnemyEnterRange(Character character)
        {
            if (character != null && !character.IsDie && character != owner)
            {
                enemiesInRange.Add(character);
            }
        }
        
        private void OnEnemyExitRange(Character character)
        {
            enemiesInRange.Remove(character);
        }
    }
}