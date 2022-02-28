using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [SerializeField] public GameObject player;

    public GameObject[] InventoryGameObjects;

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
                return;
            }
        }
    }
}
