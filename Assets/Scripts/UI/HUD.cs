using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button InventoryButton;
    [SerializeField] private GameObject InventoryWindow;
    [SerializeField] private GameObject ItemWindow;
    [SerializeField] private GameObject ItemDescription;
    [SerializeField] private PlayerInventory Inventory;
    [SerializeField] private Sprite BlankSlot;

    private int _currentSlotId = 0;
    private void Awake()
    {
        InventoryButton.onClick.AddListener(OpenInventory);
        foreach (Transform child in InventoryWindow.transform)
        {
            Button button = child.GetChild(1).GetComponent<Button>();
            Transform position = button.transform.parent;
            child.GetChild(1).GetComponent<Button>().onClick.AddListener(()=> OpenItemWindow(position));
        }
        
    }

    private void OpenInventory()
    {
        InventoryWindow.SetActive(!InventoryWindow.activeSelf);
    }
    private void OpenItemWindow(Transform newPosition)
    {
        _currentSlotId = newPosition.transform.GetSiblingIndex();
        
        if (Inventory.Slots[_currentSlotId].Item is null)
        {
            return;
        }

        ItemWindow.GetComponent<RectTransform>().localPosition = newPosition.localPosition;
        ItemDescription.transform.GetComponent<TextMeshProUGUI>().text =
            Inventory.Slots[_currentSlotId].Item.Description;
        ItemWindow.SetActive(true);
    }

    public void RemoveItemFromInventory()
    {
        Inventory.RemoveItem(_currentSlotId, true, 0);
    }
    
    public void UpdateUI()
    {
        for (int i = 0; i < Inventory.Slots.Length; i++)
        {
            Transform slotRect = InventoryWindow.transform.GetChild(i);
            Transform Image = slotRect.GetChild(1);            
            if (Inventory.Slots[i].Item is null)
            {
                Image.GetComponent<Image>().sprite = BlankSlot;
                slotRect.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                continue;
            }
            Image.GetComponent<Image>().sprite = Inventory.Slots[i].Item.Sprite;
            UpdateSingleSlotTextUI(i);
        }
    }

    public void UpdateSingleSlotTextUI(int id)
    {
        Transform slotRect = InventoryWindow.transform.GetChild(id);
        if (Inventory.Slots[id] is null)
        {
            return;
        }
        string itemName = Inventory.Slots[id].Item.ItemName;
        if (Inventory.Slots[id].Item.MaxAmountInStack > 1)
        {
            itemName += "\n" + Inventory.Slots[id].Amount;
        }
        slotRect.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemName;
    }
}
