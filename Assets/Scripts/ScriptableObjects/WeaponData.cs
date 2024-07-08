using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Data/Items/Weapon", order = 0)]
    public class WeaponData : ItemData
    {
        [SerializeField] private float baseDamage = 10f;
        [SerializeField] private float maxRange = 10f;
        [SerializeField] private float ammoInMagazine = 20f;
        [SerializeField] private AmmoData ammoType;
        [SerializeField] private bool needAmmo;
        [SerializeField] private float attackIntervalInSeconds;

        public float BaseDamage => baseDamage;

        public float AttackIntervalInSeconds => attackIntervalInSeconds;
        public float MaxRange => maxRange;

        public float AmmoInMagazine => ammoInMagazine;

        public AmmoData AmmoType => ammoType;
        public bool NeedAmmo => needAmmo;
    }
}