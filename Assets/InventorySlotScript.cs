using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,/* IPointerClickHandler,*/ IBeginDragHandler, IDragHandler, IDropHandler
{
    public GameObject textGameObject;
    public GameObject image;

    public int buttonID, previousButtonID;

    public RectTransform[] rectTransform; //probat prebacit na samo 1 childa

    GraphicRaycaster raycast;
    List<RaycastResult> onStartedDraggingResult, onDraggedResults;
    int layerMask = 6;

    private void Awake()
    {
        rectTransform = GetComponentsInChildren<RectTransform>(true);
        raycast = GetComponent<GraphicRaycaster>();
        onDraggedResults = new List<RaycastResult>();
        onStartedDraggingResult = new List<RaycastResult>();
    }

    public void Start()
    {
        InventoryManager.UpdateUIEvent += UpdateInventoryUI;
    }

    public void UpdateInventoryUI(int id)
    {
        if (id == buttonID)
        {
            if (InventoryManager.instance.InventoryGameObjects[buttonID])
            {
                image.SetActive(true);
                image.gameObject.GetComponent<Image>().color = InventoryManager.instance.InventoryGameObjects[buttonID].GetComponent<SpriteRenderer>().color;
                textGameObject.GetComponentInChildren<TMP_Text>().text = InventoryManager.instance.InventoryGameObjects[buttonID].name;
            }
        }
    }

    public void DropItemFromInventory()
    {
        if (InventoryManager.instance.InventoryGameObjects[buttonID] != null)
        {
            image.SetActive(false);
            image.transform.position = this.gameObject.transform.position; //za swap WIP
            InventoryManager.instance.InventoryGameObjects[buttonID].gameObject.SetActive(true);
            InventoryManager.instance.InventoryGameObjects[buttonID].gameObject.transform.position = new Vector2(InventoryManager.instance.player.transform.position.x + 2, InventoryManager.instance.player.transform.position.y + 2);
            InventoryManager.instance.InventoryGameObjects[buttonID] = null;
        }
        else
        {
            Debug.Log("There is no stored gameobject on " + buttonID + ". inventory slot!");
        }
    }

    /*public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if(InventoryManager.instance.isAlreadyClicked == true)
            {
                //SWAP
                
                InventoryManager.instance.isAlreadyClicked = false;
            }
            else
            {
                InventoryManager.instance.isAlreadyClicked = true;
            }
        }
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        EventSystem.current.RaycastAll(eventData, onStartedDraggingResult);
        if (onStartedDraggingResult.Count >= 1)
        {
            foreach (RaycastResult results in onStartedDraggingResult)
            {
                if (results.gameObject.layer == layerMask)
                {
                    Debug.Log("Prvi: " + results.gameObject.name);
                    previousButtonID = buttonID;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (image.activeSelf)
        {
            rectTransform[1].anchoredPosition += eventData.delta;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            EventSystem.current.RaycastAll(eventData, onDraggedResults);
            if (onDraggedResults.Count >= 1)
            {
                foreach (RaycastResult result in onDraggedResults)
                {
                    if (result.gameObject.layer == layerMask) //promjenio sam layer placeholderimage svima bili su na UI - ZA DEBUG SAM UKLJUCIO I SLOTU TAJ LAYER
                    {
                        Debug.Log(previousButtonID);
                        Debug.Log(buttonID);
                        SwapItems(previousButtonID, buttonID);
                        Debug.Log(result.gameObject.name);
                    }
                }
            }
        }
    }

    public void SwapItems(int previousButtonID, int buttonID)
    {
        InventoryManager.instance.SwapGameObjectsInArray(previousButtonID, buttonID);
    }

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
