using System;
using Inventory;
using Player;
using UnityEngine;

namespace Items
{
    // [RequireComponent(typeof(Collider2D))]
    public class ItemPickable : Item
    {
        [SerializeField] internal String _playerTag;
        private Collider2D _trigger;

        public string PlayerTag
        {
            get => _playerTag;
            set => _playerTag = value;
        }

        private void Awake()
        {
            _trigger = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PickThisUp(other);
        }

        public override void PickThisUp(Collider2D other)
        {
            if (!other.transform.gameObject.CompareTag(_playerTag))
            {
                return;
            }

            if(other.GetComponent<PlayerInventory>().AddItem(Data, AmountToAdd))//;
            {
                transform.gameObject.SetActive(false);
            } //we do not remove gameobject for possible future object pool system
            
        }
    }
}
