using _Game.Scripts.GamePlay.Skin.Base;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CharacterSkin : MonoBehaviour
    {
        #region Config
        
        [Header("References")]
        [SerializeField] Transform head;
        [SerializeField] Transform rightHand;
        [SerializeField] Transform leftHand;
        [SerializeField] Renderer pant;

        [Header("Config")] 
        [SerializeField] private ModelSkinDataSO<Hair> hairData;
        [SerializeField] private ModelSkinDataSO<Weapon.Weapon> weaponData;
        [SerializeField] private ModelSkinDataSO<Shield> shieldData;
        [SerializeField] private PaintDataSO pantData;
        
        private Weapon.Weapon _currentWeapon;
        private Shield _currentShield;
        private Hair _currentHair;
        
        public Weapon.Weapon CurrentWeapon => _currentWeapon;

        #endregion
        
        public void ChangeWeapon(WeaponType weaponType)
        {
            _currentWeapon = Instantiate(weaponData.GetSkin((int)weaponType), rightHand);
        }

        public void ChangeShield(ShieldType shieldType)
        {
            if (shieldType != ShieldType.None)
            {
                _currentShield = Instantiate(shieldData.GetSkin((int)shieldType), leftHand);
            }
        }
        
        public void ChangeHair(HairType hairType)
        {
            if (hairType != HairType.None)
            {
                _currentHair = Instantiate(hairData.GetSkin((int)hairType), head);
            }
        }

        public void ChangePant(PantType pantType)
        {
            if (pantType != PantType.None)
            {
                pant.material = pantData.GetMaterial((int)pantType);
            }
        }

        public void OnDespawn()
        {
            DespawnHat();
            DespawnShield();
            DespawnWeapon();
        }

        public void DespawnHat()
        {
            if (_currentHair)
            {
                Destroy(_currentHair.gameObject);
            }
        }
        public void DespawnShield()
        {
            if (_currentShield)
            {
                Destroy(_currentShield.gameObject);
            }
        }

        public void DespawnWeapon()
        {
            if (_currentWeapon)
            {
                Destroy(_currentWeapon.gameObject);
            }
        }
    }
}