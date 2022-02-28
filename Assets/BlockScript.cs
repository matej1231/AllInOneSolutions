using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public Blocks blocks;

    SpriteRenderer spriteRenderer;
    public string nameOfBlock;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = blocks.color;
        nameOfBlock = blocks.name;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(InventoryManager.instance != null)
            {
                InventoryManager.instance.AddItemToInventory(this.nameOfBlock, this.gameObject);
            }
        }
    }
}
