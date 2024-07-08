using System;
using Items;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemyLootList", menuName = "Data/Characters/LootList", order = 0)]
    [Serializable]
    public class EnemyLootTable : ScriptableObject
    {
        [SerializeField] private LootItem[] table;

        public LootItem[] Table => table;
    }
    [Serializable]
    public struct LootItem
    {
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private float _chance;

        public GameObject ItemPrefab => _itemPrefab;

        public float Chance => _chance;
    }
}