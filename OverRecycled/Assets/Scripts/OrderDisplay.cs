using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    float gravityValue = 800f;
        
    public Recipe recipe;

    public Image backgroundImage;
    public Image[] inputImage;
    public Image outputImage;
    public Text orderNameText;
    public Text scoreValueText;

    //timer
    public Image timerBar;
    float maxTime = 15f;
    public float timeLeft;

    void Start()
    {
        orderNameText.text = recipe.name;
        scoreValueText.text = recipe.scoreValue.ToString();

        for (int i = 0; i < recipe.inputs.Length; i++)
        {
            inputImage[i].sprite = recipe.inputs[i].GetSprite();
        }

        backgroundImage.sprite = recipe.background;

        outputImage.sprite = recipe.output.GetSprite();

        timeLeft = maxTime;

        Physics2D.gravity = new Vector2(0, gravityValue);
    }

    void Update()
    {
        RecipeDisplayerSecondVersion recipe = FindObjectOfType<RecipeDisplayerSecondVersion>();

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            recipe.DestroyRecipe(0);
        }
    }
}
