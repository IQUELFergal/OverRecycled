using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Sprite sprite;
    public Recipe[] recipes;
    public Table[] tables;
    //public RecipeDisplayer recipeDisplayer;

    
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(recipes.Length != 0, this.ToString()+" : Machine sans recette");

        foreach (Recipe r in recipes)
        {
            if (r.inputs.Length != tables.Length) Debug.LogError(r.name + " : Recette non conforme (établi : " + this.ToString() + ")");
        }
        
        timeLeft= recipes[0].craftingTime;
    }

    // Update is called once per frame
    /*void Update()
    {
        
        if (IsFull())
        {
            //timeLeft -= Time.deltaTime;
            //if (timeLeft <= 0) Debug.Log(timeLeft);//Transform(recipes[0]);
            Transform(recipes[0]);
        }
        else
        {
            //timeLeft = recipes[0].craftingTime;
        }
    }*/

    public void Transform(Recipe r)
    {
        Item result = GetResult();
        if (!result) return;
        foreach (Table table in tables)
        {
            table.item = null;
        }

        if (result != null)
        {
            tables[0].item = result;
            Debug.Log("Crafted " + result.name);
        }
    }

    public bool IsFull()
    {
        foreach (Table table in tables)
        {
            if (!table.GetItem()) return false;
        }
        return true;
    }

    Item GetResult()
    {
        Item result = null; //= Resources.Load junkItem; Trouver la bonne fonction
        foreach (Recipe recipe in recipes)
        {
            switch (tables.Length)
            {
                default:
                    break;
                case 1:
                    if (tables[0].item == recipe.inputs[0])
                    {
                        result = recipe.output;
                    }
                    break;
                case 2:
                    if ((tables[0].item == recipe.inputs[0] && tables[1].item == recipe.inputs[1]) ||
                        (tables[1].item == recipe.inputs[0] && tables[0].item == recipe.inputs[1]))
                    {
                        result = recipe.output;
                    }
                    break;
                case 3:
                    if ((tables[0].item == recipe.inputs[0] && tables[1].item == recipe.inputs[1] && tables[2].item == recipe.inputs[2]) ||
                        (tables[0].item == recipe.inputs[0] && tables[1].item == recipe.inputs[2] && tables[2].item == recipe.inputs[1]) ||
                        (tables[0].item == recipe.inputs[2] && tables[1].item == recipe.inputs[1] && tables[2].item == recipe.inputs[0]) ||
                        (tables[0].item == recipe.inputs[1] && tables[0].item == recipe.inputs[1] && tables[2].item == recipe.inputs[2]))
                    {
                        result = recipe.output;
                    }
                    break;
            }

        }
        return result;
    }


    /*void CompleteRecipe(Item i)
    {
        foreach(GameObject recipe in recipeDisplayer.currentRecipe)
        {
            if(i==recipe)
        }
    }*/
}