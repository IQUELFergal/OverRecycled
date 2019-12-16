using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    protected PlayerController player;

    public Sprite sprite;
    public Item item;
    public SpriteRenderer itemOverlay;
    public SpriteRenderer selectionOverlay;

    

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
        if (item) { itemOverlay.sprite = item.GetSprite(); Debug.Log("test affichage"); }

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
        selectionOverlay.color = new Color(1, 1, 1, 0.4f);
    }

    public void DeSelect()
    {
        selectionOverlay.color = new Color(1, 1, 1, 0);
    }

}