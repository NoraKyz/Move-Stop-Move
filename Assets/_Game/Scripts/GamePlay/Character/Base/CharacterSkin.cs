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
        
        protected Character owner;
        
        #endregion

        public virtual void OnInit(Character character)
        {
            TakeOffClothes();
            
            // set owner
            owner = character;
            
            // reset rotation
            TF.localRotation = Quaternion.identity;
        }
        
        public virtual void OnDespawn()
        {
            TakeOffClothes();
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
            owner.SetWeapon(_currentWeapon);
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

        private void DespawnPant()
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
        
            anim.ResetTrigger(_currentAnimName);
            _currentAnimName = animName;
            anim.SetTrigger(_currentAnimName);
        }
    }
}