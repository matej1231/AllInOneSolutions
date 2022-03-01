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

    //public bool isAlreadyClicked = false;

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
        GameObject temp2 = null;
        for (int i = 0; i < InventoryGameObjects.Length; i++)
        {
            if (i == previousButtonID)
            {
                temp1 = InventoryGameObjects[i];
            }
            else if (i == buttonID)
            {
                temp2 = InventoryGameObjects[i];
            }
        }
        GameObject temp = temp1;
        temp1 = temp2;
        temp2 = temp;
    }
}
