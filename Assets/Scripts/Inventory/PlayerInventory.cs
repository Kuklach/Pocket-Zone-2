using System;
using Characters;
using ScriptableObjects;
using UnityEngine;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Transform inventorySlotsParent;
        [SerializeField] private CharacterInventoryData inventoryData;
        [SerializeField] private HUD inventoryUI;
        [SerializeField] private Weapon weapon;
        [SerializeField] private InventorySlot[] _slots = Array.Empty<InventorySlot>();

        private int _numSlots = 0;

        public InventorySlot[] Slots => _slots;

        public bool AddItem(ItemData item, int amount)
        {
            if (amount < 1)
            {
                return false;
            }
            int freeSlotIndex = -1;
            int amountOfExistingItems = 0;
            if (item.MaxToCarry != -1)
            {
                foreach (InventorySlot slot in _slots)
                {
                    if (slot.Item is null)
                    {
                        continue;
                    }
                    if (slot.Item == item)
                    {
                        amountOfExistingItems++;
                    }

                }                
                if (amountOfExistingItems >= item.MaxToCarry)
                {
                    return false;
                }
            }
            for (var i = 0; i < _slots.Length; i++)
            {
                InventorySlot slot = _slots[i];
                if (slot.Item is null && freeSlotIndex == -1)
                {
                    freeSlotIndex = i;
                    continue;
                }
                
                if (slot.Item != item)
                {
                    continue;
                }
                if (slot.Amount >= slot.Item.MaxAmountInStack)
                {
                    continue;
                }
                
                freeSlotIndex = i;
                    
                int remainingAmount = _slots[freeSlotIndex].AddAmount(amount);
                if(remainingAmount != 0)
                {
                    AddItem(item, remainingAmount);
                }
                inventoryData.CurrentItems = _slots;
                inventoryUI.UpdateUI();
                return true;

            }

            if (freeSlotIndex == -1)
            {
                return false;
            }
            _slots[freeSlotIndex].SetItem(item, amount);
            inventoryData.CurrentItems = _slots;
            inventoryUI.UpdateUI();
            return true;
        }

        public void RemoveItem(int id, bool removeAll, int amount)
        {
            if (removeAll || _slots[id].Amount <= amount)
            {
                _slots[id] = new InventorySlot(_slots[id].Id);
            }
            else
            {
                _slots[id].RemoveAmount(amount);
            }
            inventoryData.CurrentItems = _slots;
            inventoryUI.UpdateUI();
            (weapon as PlayerWeapon)?.UpdateAmmoVisual();
        }
        
        private void Start()
        {
            // UpdateUI();
            _numSlots = inventorySlotsParent.childCount;
            _slots = new InventorySlot[_numSlots];
            
            for (int i = 0; i < _numSlots; i++)
            {
                _slots[i] = new InventorySlot(i);
            }
            if(inventoryData.CurrentItems.Length > 0)
            {
                _slots = inventoryData.CurrentItems;
            }
            inventoryUI.UpdateUI();
        }

        private void OnApplicationQuit()
        {
            inventoryData.CurrentItems = _slots;
        }
    }
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] private int _amount;
        [SerializeField] private ItemData _item;
        [SerializeField] private int id;

        public InventorySlot(int id)
        {
            this.id = id;
        }

        public int Amount => _amount;
        public ItemData Item => _item;
        public int Id => id;

        public void UpdateId(int value)
        {
            id = value;
        }
        public void SetItem(ItemData item, int _value)
        {
            _item = item;
            AddAmount(_value);
        }
        public int AddAmount(int value)
        {
            if (value == 0)
            {
                return 0;
            }
            int error = _item.MaxAmountInStack - _amount;
            int addAmount = error - value;
            if (addAmount < 0)
            {
                _amount = _item.MaxAmountInStack;
                return -addAmount;
            }
            _amount += value;
            return 0;
        }

        public void RemoveAmount(int value)
        {
            if (value == 0)
            {
                return;
            }

            _amount -= value;
        }
    }
}