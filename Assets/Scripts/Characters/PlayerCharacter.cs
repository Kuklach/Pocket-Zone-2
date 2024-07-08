using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class PlayerCharacter : Character
    {
        [SerializeField] private CharacterInventoryData inventoryData;
        public CharacterInventoryData InventoryData => inventoryData;

        internal new void Start()
        {
            StartCoroutine(LateStart());

        }

        private IEnumerator LateStart()
        {
            yield return new WaitForSeconds(0.5f);
            _healthbarElement = healthbar.GetComponent<Slider>();
            _health = characterData.HealthPoints;
            CurrentWeapon = CharacterData.Weapon;
            Helmet = CharacterData.Helmet;
            Chest = CharacterData.Chest;
            Arms = CharacterData.Arms;
            Legs = CharacterData.Legs;

            transform.position = CharacterData.position;
            if (CurrentWeapon is null)
            {
                yield break;
            }
            // weapon.UpdateVisual();
            // (weapon as PlayerWeapon)?.UpdateAmmoVisual();
            ((PlayerWeapon)weapon).UpdateAmmoVisual();
            weapon.UpdateVisual();
            UpdateHealthbar();
        }
        private void OnApplicationQuit()
        {
            CharacterData.Weapon = CurrentWeapon;
            CharacterData.Helmet = Helmet;
            CharacterData.Chest = Chest;
            CharacterData.Arms = Arms;
            CharacterData.Legs = Legs;
            CharacterData.position = this.transform.position;
            CharacterData.HealthPoints = _health;
        }

        public override void Die()
        {
                //any code
            gameObject.SetActive(false);
        }
    }
}