using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplayerFirstVersion : MonoBehaviour
{
    int i = 0;
    int prevScore = -1;
    int numberMaxOfRecipe = 3;

    float gravityValue = 800f;
    float xPositionImage = -860f, yPositionImage = -450f; //-860f
    float xPositionText = 0f, yPositionText = 0f; //-860f
    float delayBetweenRecipe = 5f;

    bool orderIsCompleted = false;

    public Sprite firstImage;

    List<GameObject> currentRecipe = new List<GameObject>();
    //List<Recipe> recipes = new List<Recipe>();

    public Font font;
    
    void Start()
    {
        PlayerPrefs.SetInt("RecipeNumber", 0);

        InvokeRepeating("Update5Seconds", 0f, delayBetweenRecipe);

        Physics2D.gravity = new Vector2(0, gravityValue);  
    }

    void Update()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore1", 0);

        if (orderIsCompleted == true || currentScore % 5 == 0 && currentScore!=0)
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

        //Create an other GameObject to add a Text
        GameObject text = new GameObject();

        //Make the GameObject child of the Canvas
        newRecipe.transform.SetParent(canvas.transform);
        newRecipe.layer = 1;
        text.transform.SetParent(newRecipe.transform);
        text.layer = 2;

        //Set the position of newRecipe
        newRecipe.transform.position = new Vector2(0f, 0f);

        //Add a Text to the text GameObject
        Text testText = text.AddComponent<Text>();
        testText.text = "TEST";
        text.transform.localScale = new Vector2(0.125f, 0.125f);

        //Set the position of the text and scale it
        testText.transform.position = new Vector2(50f, 40f);

        //Change the font size of the Text and its color
        testText.font = font;
        testText.fontSize = 100;
        testText.color = Color.black;

        //Change the RectTransform size to allow larger fonts and sentences
        RectTransform myRectTransform = text.GetComponent<RectTransform>();
        myRectTransform.sizeDelta = new Vector2(testText.fontSize * 10, 100);
        
        //Add a Rigidbody2D to the Image Gameobject and freeze the rotation
        Rigidbody2D rgbd2D = newRecipe.AddComponent<Rigidbody2D>();
        rgbd2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Add a BoxCollider2D to the Image Gameobject and scale it
        BoxCollider2D collider2D = newRecipe.AddComponent<BoxCollider2D>();
        collider2D.size = new Vector2(100f, 110f);

        //Add Image Component to it(This will add RectTransform as-well)
        Image myImageComponent = newRecipe.AddComponent<Image>();

        //Add a sprite to the Image
        myImageComponent.sprite = firstImage;
        myImageComponent.transform.localScale = new Vector2(0.25f, 0.25f);

        //Set the position and the scale of the Image GameObject
        newRecipe.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPositionImage, yPositionImage);
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
