using System;
using System.Collections.Generic;
using Inventory;
using Items;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewPlayerInventoryData", menuName = "Data/Inventory", order = 0)]
    public class CharacterInventoryData : ScriptableObject
    {
        [SerializeField] private InventorySlot[] _currentItems;
        public InventorySlot[] CurrentItems
        {
            get => _currentItems;
            set => _currentItems = value;
        }
    }
}