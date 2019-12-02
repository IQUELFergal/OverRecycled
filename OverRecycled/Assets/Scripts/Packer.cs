using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Packer : Table 
{
    public float timeBeforeDestruction=1.5f;
    public bool isBin;
    public Score score;
    public GameObject recipeDisplayer;
    public Recipe recipe; //A SUPPRIMER : uniquement la pour les tests
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
                    score.AddScore(-recipe.scoreValue);

                }

                if (item == recipe.output) //Ne fait rien si l'objet ne correspond à aucune recette contenue dans recipeDisplayer
                {
                    score.AddScore(recipe.scoreValue);
                    SendItem();
                }
                /*else
                {
                    foreach (Recipe recipe in recipeDisplayer.GetCurrentRecipes())
                    {
                        if (item == recipe.output) 
                        {
                            score.AddScore(recipe.scoreValue);
                            SendItem(recipe);
                        }
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
