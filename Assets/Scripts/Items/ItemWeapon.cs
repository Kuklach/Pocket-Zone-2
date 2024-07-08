using Characters;
using Inventory;
using Player;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class ItemWeapon : ItemEquipable
    {
        public override void PickThisUp(Collider2D other)
        {
            if (!other.transform.gameObject.CompareTag(_playerTag))
            {
                return;
            }
            Character character = other.GetComponent<Character>();
            if (character.CurrentWeapon is null)
            {
                character.CurrentWeapon = Data as WeaponData;
                transform.gameObject.SetActive(false);
                PlayerWeapon playerWeapon = other.transform.GetComponent<PlayerWeapon>();
                playerWeapon.UpdateVisual();
                playerWeapon.UpdateAmmoVisual();
                return;
            }
            other.GetComponent<PlayerInventory>().AddItem(Data, AmountToAdd);
            transform.gameObject.SetActive(false);


            //we do not remove gameobject for possible future object pool system   
        }
    }
}