using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IDragHandler, IDropHandler
{
    public GameObject textGameObject;
    public GameObject image;

    public int buttonID;
    //[SerializeField] private RectTransform InventoryPanel;
    //public RectTransform rectTransform;

    private void Awake()
    {

        //rectTransform = GetComponent<RectTransform>();
        //InventoryPanel = GetComponent<RectTransform>();
    }

    public void UpdateInventoryUI()
    {
        if(InventoryManager.instance.InventoryGameObjects[buttonID])
        {
            image.SetActive(true);
            //image.gameObject.GetComponent<Image>().color = Item.GetComponent<SpriteRenderer>().color;
            image.gameObject.GetComponent<Image>().color = InventoryManager.instance.InventoryGameObjects[buttonID].GetComponent<SpriteRenderer>().color;
            //textGameObject.GetComponentInChildren<TMP_Text>().text = nameForInventory;
            textGameObject.GetComponentInChildren<TMP_Text>().text = InventoryManager.instance.InventoryGameObjects[buttonID].name;
        }
    }

    public void DropItemFromInventory()
    {
        if(InventoryManager.instance.InventoryGameObjects[buttonID] != null)
        {
            image.SetActive(false);
            InventoryManager.instance.InventoryGameObjects[buttonID].gameObject.SetActive(true);
            InventoryManager.instance.InventoryGameObjects[buttonID].gameObject.transform.position = new Vector2(InventoryManager.instance.player.transform.position.x + 2, InventoryManager.instance.player.transform.position.y + 2);
            InventoryManager.instance.InventoryGameObjects[buttonID] = null;
        }
        else
        {
            Debug.Log("There is no stored gameobject on " + buttonID + ". inventory slot!");
        }
    }

    /*public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
                Debug.Log("Izbaci");
        }
    }*/

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image.activeSelf)
        {
            textGameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textGameObject.SetActive(false);
    }
}
