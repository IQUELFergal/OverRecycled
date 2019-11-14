using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    protected PlayerController player;

    public ItemHolder item;
    public SpriteRenderer itemOverlay;

    public Sprite sprite;
    public Sprite selectedSprite;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if(item && item.HasItem()) itemOverlay.sprite = item.GetItemSprite();
    }

    // Update is called once per frame
    void Update()
    {
        //move the item to the table
        if (item)
        {
            if (item.HasItem()) itemOverlay.sprite = item.GetItemSprite();
            item.transform.position = transform.position;
        }
        else itemOverlay.sprite = null;
    }

    public void Select()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "SelectedFurniture";
        GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    public void DeSelect()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Furniture";
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Take an item
    public void Take()
    {
        if (item && !player.item)
        {
            Debug.Log("Took " + item.item.name.ToString()); 
            player.item = item;
            item = null;
        }
    }

    //Drop an item
    public void Drop()
    {
        if (!item && player.item)
        {
            Debug.Log("Dropped " + player.item.item.name.ToString()); 
            item = player.item;
            player.item = null;
        }
    }

    //Switch two items
    public void Switch()
    {
        if (item && player.item)
        {
            Debug.Log("Switched "+ player.item.item.name+ " with "+ item.item.name); 
            ItemHolder tmpItem = player.item;
            player.item = item;
            item = tmpItem;
        }
    }

}