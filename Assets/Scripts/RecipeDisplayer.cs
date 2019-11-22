using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplayer : MonoBehaviour
{
    int i = 0;
    float timerInSeconds = 15f;
    float gravityValue = 200f;
    float xPosition = -860f, yPosition = -450f;

    public Sprite firstImage;

    Image myImageComponent;

    Rigidbody2D constraint;
    BoxCollider2D colliderScaler;

    void Start()
    {
        PlayerPrefs.SetInt("RecipeNumber", 0);

        InvokeRepeating("Create", 0f, 5f);

        Physics2D.gravity = new Vector2(0, gravityValue);  
    } 

    void Create()
    {
        //Number of recipe
        i++;
        PlayerPrefs.SetInt("RecipeNumber", i);

        //Create Canvas
        GameObject canvas = CreateCanvas(false);

        //Create your Image GameObject
        GameObject newRecipe = new GameObject(i.ToString());

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

        //Destroy the Image GameObject in timerInSeconds seconds
        Destroy(newRecipe, timerInSeconds);
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
