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
        
        public const float MinSize = 1f;
        public const float MaxSize = 3f;

        [Header("References")] 
        [SerializeField] private CharacterAttack characterAttack;
        [SerializeField] private CharacterModel characterModel;
        [SerializeField] private CharacterSkin characterSkin;
        [SerializeField] private CircleTargetIndicator circleTargetIndicator;
        [SerializeField] protected Transform targetIndicatorPos;
        
        [Header("Config")]  
        [SerializeField] private float size;
        [SerializeField] private int score;

        protected TargetIndicator targetIndicator;
        
        public bool HasEnemyInRange => characterAttack.HasEnemyInRange;
        public bool IsAttackAble => characterAttack.IsAttackAble;

        public float Size => size;
        
        public int Score => score;
        
        public bool IsDie { get; private set; }
        
        #endregion

        public virtual void OnInit()
        {
            IsDie = false;
            score = 0;
            
            characterAttack.OnInit();
            characterModel.OnInit();
            characterSkin.OnInit();
            circleTargetIndicator.OnInit();
            
            targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
            targetIndicator.SetTarget(targetIndicatorPos);
        }
        
        public virtual void OnHit(Action hitAction)
        {
            IsDie = true;
            hitAction?.Invoke();
            this.PostEvent(EventID.OnCharacterDie, this);
        }

        public virtual void OnDespawn()
        {
            SimplePool.Despawn(targetIndicator);
        }

        protected virtual void SetSize(float value)
        {
            size = Mathf.Clamp(value, MinSize, MaxSize);
            TF.localScale = Vector3.one * size;
        }
        
        public void AddScore(int amount = 1)
        {
            SetScore(score + amount);
            //ParticlePool.Play(Utilities.RandomInMember(ParticleType.LevelUp_1, ParticleType.LevelUp_2, ParticleType.LevelUp_3), TF.position);
        }

        public void SetScore(int value)
        {
            score = value > 0 ? value : 0;
            targetIndicator.SetScore(score);
            SetSize(MinSize + score * 0.1f);
        }
        
        public void Attack(Vector3 targetPos) => characterAttack.Attack(targetPos);
        
        public Character GetEnemy() => characterAttack.GetRandomEnemyInRange();
        
        public void LookAtTarget(Vector3 target) => characterModel.LookAtTarget(target);
        
        public void ChangeAnim(string animName) => characterModel.ChangeAnim(animName);
        
        public void SetCircleTargetIndicator(bool isVisible) => circleTargetIndicator.SetVisible(isVisible);
    }
}
