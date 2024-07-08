using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] internal CharacterData characterData;
        [SerializeField] internal GameObject healthbar;
        [SerializeField] internal Weapon weapon;

        internal Slider _healthbarElement;

        public WeaponData CurrentWeapon { get; set; }
        public ArmorData Helmet { get; set; }
        public ArmorData Chest { get; set; }
        public ArmorData Arms { get; set; }
        public ArmorData Legs { get; set; }
        
        public CharacterData CharacterData => characterData;

        internal float _health;
        public float Health
        {
            get => _health;
            set
            {
                UpdateHealthbar();
                if (!((value + Health) >= characterData.MaxHealthPoints))
                {
                    return;
                }
                _health = characterData.MaxHealthPoints;

            }
        }

        internal void UpdateHealthbar()
        {
            if (_healthbarElement is null)
            {
                return;
            }
            _healthbarElement.value = Mathf.Clamp(_health / characterData.MaxHealthPoints, 0, 1);
        }

        
        internal void Start()
        {
            _healthbarElement = healthbar.GetComponent<Slider>();
            _health = CharacterData.HealthPoints;

            CurrentWeapon = CharacterData.Weapon;
            Helmet = CharacterData.Helmet;
            Chest = CharacterData.Chest;
            Arms = CharacterData.Arms;
            Legs = CharacterData.Legs;
            
            if (CurrentWeapon is null)
            {
                return;
            }
            weapon.UpdateVisual();
        }

        public void AddHealth(float amount)
        {
            UpdateHealthbar();
            _health += amount;
            
        }

        public void RemoveHealth(float amount)
        {
            // Debug.Log(_health);
            _health -= amount;
            UpdateHealthbar();
            if (_health <= 0)
            {
                Die();
            }
        }

        public abstract void Die();

    }
}