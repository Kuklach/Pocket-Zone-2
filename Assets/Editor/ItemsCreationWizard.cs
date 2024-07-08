using System;
using System.IO;
using System.Reflection;
using Items;
using ScriptableObjects;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Editor
{
    public class CreatingPrefabs : ScriptableWizard
    {
        public string PrefabsSubFolder = "Items";
        public ItemData Data;
        public string SortingTag = "Player";
        public string SortingLayerUI = "Entity";
        public ItemType NewItemType = ItemType.Item;

        [MenuItem("Assets/Create New Item Prefab/New Pickable Item")]
        static void CreateWizard()
        {
            DisplayWizard<CreatingPrefabs>("Create Item Pickable", "Create");
        }


        //from stackoverflow
        public string[] GetSortingLayerNames() 
        {
            Type internalEditorUtilityType = typeof(InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            return (string[])sortingLayersProperty.GetValue(null, Array.Empty<object>());
        }
        void OnWizardCreate()
        {
            if (!Directory.Exists("Assets/Prefabs"))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }

            if (!Directory.Exists("Assets/Prefabs/" + PrefabsSubFolder))
            {
                AssetDatabase.CreateFolder("Assets/Prefabs", PrefabsSubFolder);
            }

            string localPath = "Assets/Prefabs/" + PrefabsSubFolder + "/" + Data.ItemName + ".prefab";
        
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            GameObject go = new GameObject();
            ItemPickable itemData;
            switch (NewItemType)
            {
                case ItemType.Item:
                    itemData = go.AddComponent<ItemPickable>();
                    break;
                case ItemType.Weapon:
                    itemData = go.AddComponent<ItemWeapon>();
                    break;
                case ItemType.Armor:
                    itemData = go.AddComponent<ItemArmor>();
                    break;
                case ItemType.Ammo:
                    itemData = go.AddComponent<ItemAmmo>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            itemData.ObjectItemData = Data;
            itemData.PlayerTag = SortingTag;
            go.name = Data.ItemName;
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            Collider2D collider2D = go.AddComponent<CircleCollider2D>();
            collider2D.isTrigger = true;
            spriteRenderer.sprite = Data.Sprite;
            string[] sortingLayers = Array.Empty<string>();
            sortingLayers = GetSortingLayerNames();
            string toPrint = "\n";
            foreach (string sortingLayer in sortingLayers)
            {
                toPrint += (sortingLayer + ", ");
            }
            Debug.Log("Current Sorting Layers: " + toPrint);
            string sortingLayerName = "Default";
            foreach (string sortingLayer in sortingLayers)
            {
                if (sortingLayer == SortingLayerUI)
                {
                    sortingLayerName = SortingLayerUI;
                    Debug.Log("Sorting Layer has changed successfully");
                }
            }
            spriteRenderer.sortingLayerName = sortingLayerName;

            PrefabUtility.SaveAsPrefabAsset(go, localPath, out var prefabSuccess);
            if (prefabSuccess)
            {
                Debug.Log("Prefab was saved successfully");
            }
            else
            {
                Debug.Log("Prefab failed to save" + false);
            }

            DestroyImmediate(go);
        }
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Item,
        Ammo
    }
}
