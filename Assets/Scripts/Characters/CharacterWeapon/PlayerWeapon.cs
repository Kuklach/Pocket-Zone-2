using System;
using Characters;
using Inventory;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Characters
{
    public class PlayerWeapon : Weapon
    {
        [SerializeField] private Button fireButton;
        [SerializeField] private GameObject ammoCounter;
        
        [SerializeField] private HUD hud;
        [SerializeField] private PlayerInventory inventory;

        private int _currentAmmo;
        private int _totalAmmo;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Start()
        {
            _textMeshProUGUI = ammoCounter.GetComponent<TextMeshProUGUI>();
            fireButton.onClick.AddListener(Shoot);
            _currentAmmo = -1;
            UpdateVisual();
            UpdateAmmoVisual();
        }

        public void UpdateAmmoVisual()
        {
            if (character.CurrentWeapon is null)
            {
                return;
            }
            _totalAmmo = 0;
            foreach (InventorySlot currentItem in inventory.Slots)
            {
                if (currentItem?.Item is not AmmoData data)
                {
                    continue;
                }

                if (data != character.CurrentWeapon.AmmoType)
                {
                    continue;
                }
                _currentAmmo = currentItem.Id;
                _totalAmmo += currentItem.Amount;
            }
            
            _textMeshProUGUI.text = _totalAmmo.ToString();            
        }

        internal override void Shoot()
        {
            if (_totalAmmo <= 0 && character.CurrentWeapon.NeedAmmo)
            {
                return;
            }
            if (character.CurrentWeapon is null)
            {
                return;
            }
            if (characterVision.Target is null)
            {
                return;
            }

            if (character.CurrentWeapon.NeedAmmo)
            {
                inventory.RemoveItem(_currentAmmo, false, 1);
                if (inventory.Slots[_currentAmmo] is null || inventory.Slots[_currentAmmo].Amount <= 0)
                {
                    UpdateAmmoVisual();
                }
                hud.UpdateSingleSlotTextUI(_currentAmmo);

                UpdateAmmoVisual();
            }
            Vector2 position = weaponPivot.transform.position;
                RaycastHit2D hit = Physics2D.Raycast(position,
                ((Vector2)characterVision.Target.transform.position - position).normalized,
                character.CurrentWeapon.MaxRange, characterVision.TargetLayerMask);
                if (hit && hit.transform.TryGetComponent<Character>(out var characterOther))
                {

                    characterOther.RemoveHealth(character.CurrentWeapon.BaseDamage);
                }
                
        }

        internal override void Reload()
        {
            throw new NotImplementedException();
        }
    }
}