using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;

    public ItemHolder() { item = null; }
    public ItemHolder(Item i) { item = i; }

    public Sprite GetItemSprite()
    {
        if (!item) return null;
        else return item.sprite;
    }

    public bool HasItem()
    {
        if (!item) return false;
        else return true;
    }
}