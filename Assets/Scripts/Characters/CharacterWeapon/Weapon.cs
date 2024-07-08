using Inventory;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Characters
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] internal Character character;
        [SerializeField] internal CharacterVision characterVision;
        [SerializeField] internal GameObject weaponPivot;
        [SerializeField] private GameObject weaponSprite;
        internal abstract void Shoot();

        internal void UpdateVisual()
        {
            if (character.CurrentWeapon is null)
            {
                weaponPivot.gameObject.SetActive(false);
                return;
            }
            weaponPivot.gameObject.SetActive(true);
            weaponSprite.GetComponent<SpriteRenderer>().sprite = character.CurrentWeapon.Sprite;
        }
        internal abstract void Reload();

    }
}