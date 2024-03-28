using System;
using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.Pool.Scripts;
using _SDK.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterSight : GameUnit
    {
        #region Config
        
        [Header("Config")]
        [SerializeField] private Character owner;
        [SerializeField] private float range;
        [SerializeField] private List<Character> enemiesInRange = new List<Character>();
        
        private Action<object> _onCharacterDie;
        public List<Character> EnemiesInRange => enemiesInRange;
        
        public float Range => range;                                                                                       
        
        #endregion

        private void OnEnable()
        {
            _onCharacterDie = (param) => OnEnemyExitRange((Character) param);
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
        }

        public void OnInit()
        {
            enemiesInRange.Clear();
            
            TF.localScale = Vector3.one * (range > 0 ? range : Constants.DefaultAttackRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character character = Cache<Character>.GetComponent(other);
                OnEnemyEnterRange(character);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character character = Cache<Character>.GetComponent(other);
                OnEnemyExitRange(character);
            }
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