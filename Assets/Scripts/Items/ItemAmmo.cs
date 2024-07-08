using Characters;
using Inventory;
using Player;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class ItemAmmo : ItemPickable
    {
        public override void PickThisUp(Collider2D other)
        {
            if (!other.transform.gameObject.CompareTag(_playerTag))
            {
                return;
            }

            if (!other.GetComponent<PlayerInventory>().AddItem(Data, AmountToAdd))
            {
                return;
            } //;

            Character character = other.GetComponent<Character>();
            character.CurrentWeapon ??= Data as WeaponData;
            transform.gameObject.SetActive(false);
            other.transform.GetComponent<PlayerWeapon>().UpdateAmmoVisual();
            //we do not remove gameobject for possible future object pool system   
        }
    }
}