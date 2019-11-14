using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Recipe[] recipes;
    public Table[] tables;
    public ItemHolder emptyItem;

    
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(recipes.Length != 0, this.ToString()+" : Machine sans recette");
        foreach (Recipe r in recipes)
        {
            if (r.inputs.Length!=tables.Length) Debug.LogError(r.name + " : Recette non conforme (établi : "+this.ToString()+")");
        }
        timeLeft= recipes[0].craftingTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isFull())
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) Debug.Log(timeLeft);//Transform(recipes[0]); 
            
        }
        else
        {
            timeLeft = recipes[0].craftingTime;
        }
    }

    void Transform(Recipe r)
    {
        foreach (Table table in tables)
        {
            table.item = null;
        }
        Item result = r.output;
        tables[0].item = emptyItem;
        tables[0].item.item = result;
        Debug.Log("Crafted " + result.name);
    }

    bool isFull()
    {
        foreach (Table table in tables)
        {
            if (!table.item) return false;
        }
        return true;
    }
}