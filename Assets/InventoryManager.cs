using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] public GameObject player;

    public GameObject[] InventoryGameObjects;

    public static event Action<int> UpdateUIEvent;

    private void Awake()
    {
        if (instance == null) instance = this;
        InventoryGameObjects = new GameObject[6];
    }

    public void AddItemToInventory(string nameForInventory, GameObject Item)
    {
        for (int i = 0; i < InventoryGameObjects.Length - 1; i++)
        {
            if (InventoryGameObjects[i] == null)
            {
                InventoryGameObjects[i] = Item;
                Item.gameObject.SetActive(false);
                UpdateUIEvent?.Invoke(i);
                return;
            }
        }
    }

    public void SwapGameObjectsInArray(int previousButtonID, int buttonID)
    {
        GameObject temp1 = null;

        temp1 = InventoryGameObjects[previousButtonID];
        InventoryGameObjects[previousButtonID] = InventoryGameObjects[buttonID];
        UpdateUIEvent?.Invoke(previousButtonID);
        InventoryGameObjects[buttonID] = temp1;
        UpdateUIEvent?.Invoke(buttonID);

        temp1 = null;
    }
}
