using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : Table
{
    public float timeBeforeDestruction=1.5f;
    private float timeLeft;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (item) item = null;
        timeLeft = timeBeforeDestruction;
    }

    // Update is called once per frame
    void Update()
    {
        //move the item to the table
        if (item)
        {
            if (item.HasItem()) itemOverlay.sprite = item.GetItemSprite();
            item.transform.position = transform.position;
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) DestroyItem();
        }
        else
        {
            itemOverlay.sprite = null;
            timeLeft = timeBeforeDestruction;
        }
    }


    public void DestroyItem()
    {
        if (item)
        {
            Debug.Log("Destroyed " + item.item.name);
            item = null;
            
        }
    }
}
