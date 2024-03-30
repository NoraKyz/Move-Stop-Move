using System;
using _Game.Scripts.Interface;
using _SDK.Observer.Scripts;
using _SDK.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class Character : PoolUnit, IHit
    {
        #region Config

        protected const float MinSize = 1f;
        protected const float MaxSize = 3f;

        [Header("References")] 
        [SerializeField] protected CharacterAttack characterAttack;
        [SerializeField] protected CharacterSkin characterSkin;
        [SerializeField] protected CircleTargetIndicator circleTargetIndicator;
        [SerializeField] protected Transform targetIndicatorPos;
        
        [Header("Config")]  
        [SerializeField] private float size;
        [SerializeField] private int score;
        [SerializeField] private string charName;

        private TargetIndicator _targetIndicator;
        
        public bool HasEnemyInRange => characterAttack.HasEnemyInRange;
        public bool IsAttackAble => characterAttack.IsAttackAble;

        public float Size => size;
        public int Score => score;
        public string CharName => charName;
        public CharacterAttack CharacterAttack => characterAttack;
        public bool IsDie { get; private set; }
        
        #endregion

        public virtual void OnInit()
        {
            score = 0;
            IsDie = false;
         
            characterAttack.OnInit();
            circleTargetIndicator.OnInit();
            _targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
            _targetIndicator.SetTarget(targetIndicatorPos);
        }
        
        public virtual void OnHit(Action hitAction, Character killer)
        {
            if (IsDie)
            {
                return;
            }
            
            IsDie = true;
            hitAction?.Invoke();
            this.PostEvent(EventID.OnCharacterDie, this);
        }

        public virtual void OnDespawn()
        {
            SimplePool.Despawn(_targetIndicator);
        }

        protected void SetName(string value)
        {
            charName = value;
            _targetIndicator.SetName(charName);
        }   
        
        public virtual void AddScore(int amount = 1)
        {
            SetScore(score + amount);
        }

        public void SetScore(int value)
        {
            score = value > 0 ? value : 0;
            _targetIndicator.SetScore(score);
            SetSize(MinSize + score * 0.1f);
        }
        
        protected virtual void SetSize(float value)
        {
            size = Mathf.Clamp(value, MinSize, MaxSize);
            TF.localScale = Vector3.one * size;
        }
        
        public void Attack(Vector3 targetPos) => characterAttack.Attack(targetPos);
        
        public Character GetEnemy() => characterAttack.GetRandomEnemyInRange();
        
        public void LookAtTarget(Vector3 target) => characterSkin.LookAtTarget(target);
        
        public void ChangeAnim(string animName) => characterSkin.ChangeAnim(animName);
        
        public void SetCircleTargetIndicator(bool isVisible) => circleTargetIndicator.SetVisible(isVisible);
    }
}
