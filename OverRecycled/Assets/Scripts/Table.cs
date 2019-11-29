using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    protected PlayerController player;

    //public ItemHolder itemHolder;
    public Item item;
    public SpriteRenderer itemOverlay;
    public SpriteRenderer selectionOverlay;

    public Sprite sprite;
    public Sprite selectedSprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = -(int)transform.position.y;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (item) itemOverlay.sprite = item.GetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (item) itemOverlay.sprite = item.GetSprite();
        else itemOverlay.sprite = null;
    }

    public void SetItem(Item i)
    {
        item = i;
    }

    public Item GetItem()
    {
        return item;
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

}