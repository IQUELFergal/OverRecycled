using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    float gravityValue = 800f;
        
    public Order order;

    public Image backgroundImage;
    public Image input1Image;
    public Image input2Image;
    public Image outputImage;
    public Text orderNameText;
    public Text scoreValueText;

    //timer
    public Image timerBar;
    float maxTime = 15f;
    public float timeLeft;

    void Start()
    {
        orderNameText.text = order.name;
        scoreValueText.text = order.scoreValue.ToString();

        //maxTime = order.orderTime;

        backgroundImage.sprite = order.background;
        input1Image.sprite = order.input1;
        input2Image.sprite = order.input2;
        outputImage.sprite = order.output;

        //timer
        timeLeft = maxTime;

        //move the order to the top
        Physics2D.gravity = new Vector2(0, gravityValue);
    }

    void Update() //or while not destroy
    {
        RecipeDisplayerSecondVersion recipe = FindObjectOfType<RecipeDisplayerSecondVersion>();

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            //destroy the order
            recipe.DestroyRecipe(0);
        }
    }
}
