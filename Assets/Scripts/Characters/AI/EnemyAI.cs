using System;
using System.Collections;
using UnityEngine;

namespace Characters
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private CharacterVision vision;
        private float _weaponRange;
        private WaitForSeconds _intervalWait;
        private void Start()
        {
            if (weapon.character.CurrentWeapon is null)
            {
                return;
            }

            _intervalWait =new WaitForSeconds( weapon.character.CurrentWeapon.AttackIntervalInSeconds);
            _weaponRange = weapon.character.CurrentWeapon.MaxRange * weapon.character.CurrentWeapon.MaxRange;
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            
            while (true)
            {
                yield return _intervalWait;
                if(vision.Target is null)
                {
                   continue;
                }

                if (vision.sqrMagnitude <= _weaponRange)
                {
                   weapon.Shoot();
                }
            }
        }
    }
}