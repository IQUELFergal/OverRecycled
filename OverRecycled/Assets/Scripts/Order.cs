using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Order", menuName = "Items/Order")]
public class Order : ScriptableObject
{
    public Sprite background;
    public Sprite input1;
    public Sprite input2;
    public Sprite output;
    public new string name;
    public int scoreValue;
}
