using System;
using Characters;
using Inventory;
using ScriptableObjects;
using UnityEngine;

namespace SavingData
{
    public class SaveData : MonoBehaviour
    {

        [SerializeField] private CharacterInventoryData inventory;
        [SerializeField] private CharacterData character;
        private string path1;
        private string path2;
#if UNITY_STANDALONE && !UNITY_EDITOR
        private void OnApplicationQuit()
        {            
            string data1 = JsonUtility.ToJson(inventory, true);
            string data2 = JsonUtility.ToJson(character, true);

            Debug.Log(path1 + "\n" + path2);
            System.IO.File.WriteAllText(path1, data1);
            System.IO.File.WriteAllText(path2, data2);
        }

        private void Awake()
        {        
            path1 = Application.persistentDataPath + "/inventoryData.json";
            path2 = Application.persistentDataPath + "/characterData.json";
            if (!System.IO.File.Exists(path1) || !System.IO.File.Exists(path2))
            {
                return;
            }
            string data1 = System.IO.File.ReadAllText(path1);
            string data2 = System.IO.File.ReadAllText(path2);
            JsonUtility.FromJsonOverwrite(data1, inventory);
            JsonUtility.FromJsonOverwrite(data2, character);
        }
#endif
    }

}