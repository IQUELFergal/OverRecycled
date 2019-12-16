using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplayer : MonoBehaviour
{
    public GameObject[] orderDouble;
    public Canvas canvas;
    public Vector2 spawnPoint = new Vector2(-675, -500);

    float delayBetweenRecipe = 5f;
    int i = 0;
    int prevScore = -1;
    int numberMaxOfRecipe = 3;
    bool orderIsCompleted = false;

    List<GameObject> currentRecipe = new List<GameObject>();

    public Recipe[] recipe;
    public GameObject[] recipes;



    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("RecipeNumber", 0);

        InvokeRepeating("Update5Seconds", 0f, delayBetweenRecipe);
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore1", 0);

        if (orderIsCompleted == true || currentScore % 5 == 0 && currentScore != 0)
        {
            if (currentScore != this.prevScore)
            {
                // Suppression de la recette actuelle et création d'une nouvelle            
                this.DestroyRecipe(0);
                prevScore = currentScore;
            }
        }
        if (orderIsCompleted == true || currentScore % 2 == 0 && currentScore != 0)
        {
            if (currentScore != this.prevScore)
            {
                // Suppression de la recette actuelle et création d'une nouvelle            
                this.DestroyRecipe(1);
                prevScore = currentScore;
            }
        }
    }

    void Update5Seconds()
    {
        OrderDisplay time = FindObjectOfType<OrderDisplay>();

        if (PlayerPrefs.GetInt("RecipeNumber", 0) < numberMaxOfRecipe)
        {
            Debug.Log(PlayerPrefs.GetInt("RecipeNumber", 0));
            Create();
        }
        else if (PlayerPrefs.GetInt("RecipeNumber", 0) >= numberMaxOfRecipe && time.timeLeft == 0)
        {
            this.DestroyRecipe(0);
        }
    }

    public void DestroyRecipe(int i, bool createNewRecipe = true)
    {
        Destroy(currentRecipe[i]);
        currentRecipe.RemoveAt(i);
        this.i--;
        PlayerPrefs.SetInt("RecipeNumber", this.i);
        if (createNewRecipe)
        {
            Create();
        }
    }

    void Create()
    {
        //Number of recipe
        i++;
        PlayerPrefs.SetInt("RecipeNumber", this.i);

        //remplacer par les probas d'apparition des recettes
        if (i % 2 == 0)
        {
            GameObject order = Instantiate(orderDouble[0], spawnPoint, Quaternion.identity);
            order.transform.SetParent(canvas.transform, false);
            currentRecipe.Add(order);
        }
        else
        {
            GameObject order = Instantiate(orderDouble[1], spawnPoint, Quaternion.identity) as GameObject;
            order.transform.SetParent(canvas.transform, false);
            currentRecipe.Add(order);
        }
    }
}
