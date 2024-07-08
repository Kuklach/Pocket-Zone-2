using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Items/Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private int maxAmountInStack;
        [SerializeField] private int maxToCarry = -1;


        [SerializeField] private Sprite sprite;
        
        public int MaxToCarry => maxToCarry;
        public string ItemName => itemName;

        public string Description => description;

        public int MaxAmountInStack => maxAmountInStack;

        public Sprite Sprite => sprite;
    }
}