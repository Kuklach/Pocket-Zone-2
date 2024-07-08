using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private ItemData data;

        public ItemData ObjectItemData
        {
            get => data;
            set => data = value;
        }

        [SerializeField] private int amountToAdd;

        protected int AmountToAdd    
        {
            get //=> amountToAdd;
            {
               if(amountToAdd > data.MaxAmountInStack)
               {
                   amountToAdd = data.MaxAmountInStack;
               }

               return amountToAdd;
            }
        }

        protected ItemData Data => data;

        public abstract void PickThisUp(Collider2D other);

    }

    public enum ArmorType
    {
        Helmet,
        Chest,
        Arms,
        Legs
    }
}