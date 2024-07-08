using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/Characters/Basic", order = 0)]
    public class CharacterData : ScriptableObject
    {
        
        [SerializeField] private float maxHealthPoints;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float sightRange;
        
        [SerializeField] private WeaponData weapon;
        [SerializeField] private ArmorData helmet;
        [SerializeField] private ArmorData chest;
        [SerializeField] private ArmorData arms;
        [SerializeField] private ArmorData legs;        
        [SerializeField] private float healthPoints;
        public float MaxHealthPoints => maxHealthPoints;
        public Vector2 position;
        public WeaponData Weapon
        {
            get => weapon;
            set => weapon = value;
        }
        public ArmorData Helmet
        {
            get => helmet;
            set => helmet = value;
        }
        public ArmorData Chest
        {
            get => chest;
            set => chest = value;
        }
        public ArmorData Arms
        {
            get => arms;
            set => arms = value;
        }
        public ArmorData Legs
        {
            get => legs;
            set => legs = value;
        }
        
        public float HealthPoints
        {
            get => healthPoints;
            set => healthPoints = value;
        }

        public float MoveSpeed => moveSpeed;
        public float SightRange => sightRange;
    }
}