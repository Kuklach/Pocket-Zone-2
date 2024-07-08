using System;
using UnityEngine;

namespace Characters
{
    public class EnemyWeapon : Weapon
    {
        private void Awake()
        {
            // UpdateVisual();
        }
        internal override void Shoot()
        {
            if (character.CurrentWeapon is null)
            {
                return;
            }
            if (characterVision.Target is null)
            {
                return;
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