using System;
using Characters;
using Inventory;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class ItemArmor : ItemEquipable
    {
        public override void PickThisUp(Collider2D other)
        {
            if (!other.transform.gameObject.CompareTag(_playerTag))
            {
                return;
            }

            if(!EquipArmor(other.gameObject))
            {
                return;
            }
            if (!other.GetComponent<PlayerInventory>().AddItem(Data, AmountToAdd))
            {
                return;
            }

            transform.gameObject.SetActive(false);
        }

        public bool EquipArmor(GameObject character)
        {
            switch (((ArmorData)Data).Type)
            {
                case ArmorType.Helmet:
                    if (character.GetComponent<Character>().Helmet is not null)
                    {
                        return true;
                    }
                    character.GetComponent<Character>().Helmet = (ArmorData)Data;
                    return false;
                case ArmorType.Chest:
                    if (character.GetComponent<Character>().Chest is not null)
                    {
                        return true;
                    }
                    character.GetComponent<Character>().Chest = (ArmorData)Data;
                    return false;
                case ArmorType.Arms:
                    if (character.GetComponent<Character>().Arms is not null)
                    {
                        return true;
                    }
                    character.GetComponent<Character>().Arms = (ArmorData)Data;
                    return false;

                case ArmorType.Legs:
                    if (character.GetComponent<Character>().Legs is not null)
                    {
                        return true;
                    }
                    character.GetComponent<Character>().Legs = (ArmorData)Data;
                    return false;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}