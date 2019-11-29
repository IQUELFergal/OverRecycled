using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Packer : Table //Renommer en 
{
    public float timeBeforeDestruction=1.5f;
    public bool isBin;
    public Score score;
    public GameObject recipeDisplayer;
    private float timeLeft;

    protected override void Start()
    {
        base.Start();
        if (item) item = null;
        timeLeft = timeBeforeDestruction;
    }

    // Update is called once per frame
    void Update()
    {
        //move the item to the table
        if (item)
        {
            itemOverlay.sprite = item.GetSprite();
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                if (isBin) DestroyItem();
                /*else
                {
                    foreach (Recipe recipe in recipeDisplayer.GetCurrentRecipes())
                    {
                        if (item == recipe.output) SendItem();
                    }
                }*/
            }
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
            Debug.Log("Destroyed " + item.name);
            SetItem(null);
        }
    }

    public void SendItem()
    {
        if (item)
        {
            Debug.Log("Sent " + item.name);
            SetItem(null);
        }
    }
}
