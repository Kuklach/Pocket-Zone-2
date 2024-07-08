using ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class EnemyCharacter : Character
    {
        [SerializeField] private EnemyLootTable lootTable;
        [SerializeField] private float lootSpawnRadius = 1;
        public override void Die()
        {
            gameObject.SetActive(false);
            foreach (LootItem lootItem in lootTable.Table)
            {
                float rand = Random.value;
                if (rand > lootItem.Chance)
                {
                    return;
                }
                Instantiate(lootItem.ItemPrefab, (Vector2)transform.position + Random.insideUnitCircle * lootSpawnRadius,
                    Quaternion.identity,
                    transform.parent);
            }
        }
    }
}