using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipesDisplay : MonoBehaviour
{
    int speedTime = 50;

    int i = 0;

    public Sprite firstImage;

    Image myImageComponent;

    void Start()
    {
        PlayerPrefs.SetInt("RecipeNumber", 0);

        InvokeRepeating("Create", 5f, 5f);
    } 

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("RecipeNumber", 0) != 0)
        {
            GameObject myObject = GameObject.Find(PlayerPrefs.GetInt("RecipeNumber", 0).ToString());

            //Move the Image
            myObject.transform.Translate(Vector2.up * Time.deltaTime * speedTime );
        }

        if (PlayerPrefs.GetInt("RecipeNumber", 0) >= 2)
        {
            CancelInvoke();
        }
    }
    void Create()
    {
        i++;
        PlayerPrefs.SetInt("RecipeNumber", i);

        //Create Canvas
        GameObject canvas = CreateCanvas(false);

        //Create your Image GameObject
        GameObject newObject = new GameObject(i.ToString());

        //Add a Tag to the Image GameObject
        //newObject.gameObject.tag = "Recipe" + i.ToString();

        //Make the GameObject child of the Canvas
        newObject.transform.SetParent(canvas.transform);

        //Add Image Component to it(This will add RectTransform as-well)
        newObject.AddComponent<Image>();

        //Add a sprite to the Image
        myImageComponent = newObject.GetComponent<Image>();
        myImageComponent.sprite = firstImage;

        //Set the position of the Image
        newObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        newObject.transform.position = new Vector2(50f, 150f);
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
        cvsl.referenceResolution = new Vector2(800f, 600f);
        cvsl.matchWidthOrHeight = 0.5f;
        cvsl.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        cvsl.referencePixelsPerUnit = 100f;
    }
}
