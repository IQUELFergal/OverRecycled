using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplayer : MonoBehaviour
{
    int i = 0;
    int prevScore = -1;
    int numberMaxOfRecipe = 3;

    float gravityValue = 800f;
    float xPosition = -860f, yPosition = -450f;
    float delayBetweenRecipe = 5f;

    bool orderIsCompleted = false;

    public Sprite firstImage;

    Image myImageComponent;

    Rigidbody2D constraint;
    BoxCollider2D colliderScaler;

    List<GameObject> currentRecipe = new List<GameObject>();
    
    void Start()
    {
        PlayerPrefs.SetInt("RecipeNumber", 0);

        InvokeRepeating("Update5Seconds", 0f, delayBetweenRecipe);

        Physics2D.gravity = new Vector2(0, gravityValue);  
    }

    void Update()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore1", 0);

        if (orderIsCompleted == true || currentScore % 5 == 0)
        {
            if (currentScore != this.prevScore)
            {
                // Suppression de la recette actuelle et création d'une nouvelle            
                this.DestroyRecipe(0);
                prevScore = currentScore;
            }           
        }
    }

    void Update5Seconds()
    {       
        if (PlayerPrefs.GetInt("RecipeNumber", 0) < numberMaxOfRecipe)
        {
            Debug.Log(PlayerPrefs.GetInt("RecipeNumber", 0));
            Create();
        }
        else if (PlayerPrefs.GetInt("RecipeNumber", 0) >= numberMaxOfRecipe)
        {
            this.DestroyRecipe(0);
        }
    }

    void DestroyRecipe(int i, bool createNewRecipe = true)
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
        this.i++;
        PlayerPrefs.SetInt("RecipeNumber", this.i);

        //Create Canvas
        GameObject canvas = CreateCanvas(false);

        //Create your Image GameObject and add it to the list of the current recipe
        GameObject newRecipe = new GameObject(this.i.ToString());
        currentRecipe.Add(newRecipe);

        //Add a Rigidbody2D to the Image Gameobject and freeze the rotation
        newRecipe.AddComponent<Rigidbody2D>();
        constraint = newRecipe.GetComponent<Rigidbody2D>();
        constraint.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Add a BoxCollider2D to the Image Gameobject and scale it
        newRecipe.AddComponent<BoxCollider2D>();
        colliderScaler = newRecipe.GetComponent<BoxCollider2D>();
        colliderScaler.size = new Vector2(100f, 100f);

        //Make the GameObject child of the Canvas
        newRecipe.transform.SetParent(canvas.transform);

        //Add Image Component to it(This will add RectTransform as-well)
        newRecipe.AddComponent<Image>();

        //Add a sprite to the Image
        myImageComponent = newRecipe.GetComponent<Image>();
        myImageComponent.sprite = firstImage;

        //Set the position and the scale of the Image
        newRecipe.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);
        newRecipe.transform.localScale = new Vector2(1.5f, 1.5f);
    }

    //Creates Hidden GameObject and attaches Canvas component to it
    private GameObject CreateCanvas(bool hide)
    {
        //Create Canvas GameObject
        GameObject tempCanvas = new GameObject("Canvas");
        if (hide)
        {
            tempCanvas.hideFlags = HideFlags.HideAndDontSave;
        }

        //Create and Add Canvas Component
        Canvas cnvs = tempCanvas.AddComponent<Canvas>();
        cnvs.renderMode = RenderMode.ScreenSpaceOverlay;
        cnvs.pixelPerfect = false;

        //Set Cavas sorting order to be above other Canvas sorting order
        cnvs.sortingOrder = 12;

        cnvs.targetDisplay = 0;

        AddCanvasScaler(tempCanvas);
        return tempCanvas;
    }

    //Adds CanvasScaler component to the Canvas GameObject 
    private void AddCanvasScaler(GameObject parentaCanvas)
    {
        CanvasScaler cvsl = parentaCanvas.AddComponent<CanvasScaler>();
        cvsl.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cvsl.referenceResolution = new Vector2(1920f, 1080f);
        cvsl.matchWidthOrHeight = 0.5f;
        cvsl.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        cvsl.referencePixelsPerUnit = 100f;
    }
}
