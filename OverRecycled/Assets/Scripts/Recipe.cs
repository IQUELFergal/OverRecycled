using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public new string name;
    public float craftingTime=2;
    public Item[] inputs;
    public Item output;
    public Sprite background;
    public int scoreValue;
}