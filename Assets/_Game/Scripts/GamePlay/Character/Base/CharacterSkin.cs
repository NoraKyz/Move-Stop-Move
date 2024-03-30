using System;
using _Game.Scripts.GamePlay.Skin.Base;
using _Game.Scripts.Other.Utils;
using _SDK.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public abstract class CharacterSkin : GameUnit
    {
        #region Config
        
        [Header("References")]
        [SerializeField] Transform head;
        [SerializeField] Transform rightHand;
        [SerializeField] Transform leftHand;
        [SerializeField] Renderer pant;
        [SerializeField] private Animator anim;

        [Header("Config")] 
        [SerializeField] private SkinDataSO<Hair> hairData;
        [SerializeField] private SkinDataSO<Weapon.Weapon> weaponData;
        [SerializeField] private SkinDataSO<Shield> shieldData;
        [SerializeField] private SkinDataSO<Material> pantData;
        
        
        private Weapon.Weapon _currentWeapon;
        private Shield _currentShield;
        private Hair _currentHair;
        
        private string _currentAnimName;
        
        private CharacterAttack _characterAttack;

        protected Character owner;
        
        #endregion

        public virtual void OnInit(Character character)
        {
            TakeOffClothes();
            
            owner = character;
            _characterAttack = character.CharacterAttack;
            // reset rotation
            TF.localRotation = Quaternion.identity;
        }
        
        private void TakeOffClothes()
        {
            DespawnHair();
            DespawnPant();
            DespawnShield();
            DespawnWeapon();
        }

        protected void ChangeWeapon(WeaponType weaponType)
        {
            _currentWeapon = Instantiate(weaponData.GetSkin((int)weaponType), rightHand);
            _characterAttack.SetWeapon(_currentWeapon);
        }

        protected void ChangeShield(ShieldType shieldType)
        {
            if (shieldType != ShieldType.None)
            {
                _currentShield = Instantiate(shieldData.GetSkin((int)shieldType), leftHand);
            }
        }
        
        protected void ChangeHair(HairType hairType)
        {
            if (hairType != HairType.None)
            {
                _currentHair = Instantiate(hairData.GetSkin((int)hairType), head);
            }
        }

        protected void ChangePant(PantType pantType)
        {
            if (pantType != PantType.None)
            {
                pant.material = pantData.GetSkin((int) pantType);
            }
        }

        protected void DespawnHair()
        {
            if (_currentHair)
            {
                Destroy(_currentHair.gameObject);
            }
        }
        
        protected void DespawnPant()
        {
            pant.materials = Array.Empty<Material>();
        }
        
        protected void DespawnShield()
        {
            if (_currentShield)
            {
                Destroy(_currentShield.gameObject);
            }
        }

        protected void DespawnWeapon()
        {
            if (_currentWeapon)
            {
                Destroy(_currentWeapon.gameObject);
            }
        }
        
        public void LookAtTarget(Vector3 targetPos)
        {
            Vector3 lookPos = targetPos - TF.position;
            lookPos.y = 0;
            owner.TF.forward = lookPos.normalized;
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
    }
}