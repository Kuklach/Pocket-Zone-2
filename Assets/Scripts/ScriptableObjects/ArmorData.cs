using Items;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewArmorData", menuName = "Data/Items/Armor", order = 0)]
    public class ArmorData : ItemData
    {
        [SerializeField] private ArmorType type;
        [SerializeField] private float protection;

        public float Protection => protection;
        public ArmorType Type => type;
    }
}