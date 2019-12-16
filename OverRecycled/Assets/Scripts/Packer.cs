using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Packer : Table 
{
    public float timeBeforeDestruction=1.5f;
    public bool isBin;
    public Score score;
    public RecipeDisplayer recipeDisplayer;
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
                if (isBin)
                {
                    DestroyItem();
                }
                
                {
                    foreach (Recipe recipe in recipeDisplayer.recipe)
                    {
                        if (item == recipe.output) 
                        {
                            score.AddScore(recipe.scoreValue);
                            DestroyItem();
                        }
                    }
                }
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

}
