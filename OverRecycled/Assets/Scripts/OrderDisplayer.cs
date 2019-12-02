using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Order", menuName ="OrderDisplayer")]
public class OrderDisplayer : ScriptableObject
{
    public new string name;
    public string score;

    public Sprite[] image;
}
