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

        [Header("References")] 
        [SerializeField] private CharacterAttack characterAttack;
        [SerializeField] private CharacterModel characterModel;
        [SerializeField] private CharacterSkin characterSkin;
        [SerializeField] private CircleTargetIndicator circleTargetIndicator;

        [Header("Config")] 
        [SerializeField] private float size;

        public bool HasEnemyInRange => characterAttack.HasEnemyInRange;
        public bool IsAttackAble => characterAttack.IsAttackAble;
        
        public float Size => size;
        public bool IsDie { get; private set; }
        
        #endregion

        #region Init
        
        public virtual void OnInit()
        {
            IsDie = false;
            SetSize(size > 0 ? size : 1);
            
            characterAttack.OnInit();
            characterModel.OnInit();
            characterSkin.OnInit();
            circleTargetIndicator.OnInit();
        }

        #endregion
        
        public virtual void OnHit(Action hitAction)
        {
            IsDie = true;
            hitAction?.Invoke();
            this.PostEvent(EventID.OnCharacterDie, this);
        }
        
        public virtual void OnDespawn() { }

        public void SetSize(float value)
        {
            size = value;
            TF.localScale = Vector3.one * size;
        } 
        
        public void Attack(Vector3 targetPos) => characterAttack.Attack(targetPos);
        
        public Character GetEnemy() => characterAttack.GetRandomEnemyInRange();
        
        public void LookAtTarget(Vector3 target) => characterModel.LookAtTarget(target);
        
        public void ChangeAnim(string animName) => characterModel.ChangeAnim(animName);
        
        public void SetCircleTargetIndicator(bool isVisible) => circleTargetIndicator.SetVisible(isVisible);
    }
}
