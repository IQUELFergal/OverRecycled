using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{

    [System.Serializable]
    public struct Category
    {
        public string name;
        public int categoryChance;
        public DropCurrency[] items;
    }

    [System.Serializable]
     public struct DropCurrency
     {
        
        public Item item;
        public int dropRarity;
     }

    protected PlayerController player;
    public float speed;

    public Transform spawnPoint;
    public Transform destructionPoint;
    public Table table;
    public Item[] itemsToSpawn;

    public int spawnChance=20;
    public Category[] categories;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("GenerateItem", 0f, 1/speed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSlots();
        DestroyAtEnd();
    }

    void GenerateItem()
    {
        //Créer un système de pool d'objet random a instancier
        Table slot = Instantiate(table, spawnPoint.position, Quaternion.identity, transform.Find("Slots").transform);
        if (Random.Range(0, 100) < spawnChance)
        {
            table.item = ChooseItem(ChooseCategory());
            //table.item = itemsToSpawn[0];
            spawnChance = 0;
        }
        else
        {
            table.item = null;
            spawnChance += 20;
        }
    }

    Category ChooseCategory()
    {
        int total = 0;
        foreach(Category category in categories)
        {
            total += category.categoryChance;
        }
        int currentProba = 0;
        foreach (Category category in categories)
        {
            currentProba += category.categoryChance;
            if (Random.Range(0, total) < currentProba) return category;
        }
        Debug.LogError("No category found");
        return categories[0];
    }

    Item ChooseItem(Category category)
    {
        int total = 0;
        foreach (DropCurrency item in category.items)
        {
            total += item.dropRarity;
        }
        int currentProba = 0;
        foreach (DropCurrency item in category.items)
        {
            currentProba += item.dropRarity;
            if (Random.Range(0, total) < currentProba) return item.item;
        }
        Debug.Log("No item found");
        return null;
    }

    void MoveSlots()
    {
        foreach (Transform slot in transform.Find("Slots").transform)
        {
            slot.transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));
        }
    }

    void DestroyAtEnd()
    {
        foreach (Transform slot in transform.Find("Slots").transform)
        {
            if (slot.transform.position.x< destructionPoint.transform.position.x)
            {
               Destroy(slot.gameObject);
            }
        }
    }

}
