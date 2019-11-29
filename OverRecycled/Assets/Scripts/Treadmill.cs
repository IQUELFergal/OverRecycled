using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{

    /*[System.Serializable]
    public class DropCurrency
    {
        public Item item;
        public int dropRarity;
    }*/

    protected PlayerController player;
    public float speed;

    public Transform spawnPoint;
    public Transform destructionPoint;
    public Table table;
    public Item[] itemsToSpawn;

    /*public int dropChance=25;
    public int commonItemsChance;
    public DropCurrency[] commonItems;
    public int uncommonItemsChance;
    public DropCurrency[] uncommonItems;
    public int rareItemsChance;
    public DropCurrency[] rareItems;*/


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        /*commonItemsChance = (int)(dropChance * 0.6);
        uncommonItemsChance = (int)(dropChance * 0.3);
        rareItemsChance = (int)(dropChance * 0.1);
        if (dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance) != 0) dropChance -= dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance);*/
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
        if (Random.Range(0, 5) == 0) table.item = itemsToSpawn[0];
        else table.item = null;

        /*int calc_dropChance = Random.Range(0, 101);
        if (calc_dropChance > dropChance)
        {
            table.itemHolder = null;
            dropChance=(int)(dropChance * 1.5f);
            UpdateChances();
        }
        else
        {
            dropChance = (int)(dropChance / 1.5f);
            UpdateChances(calc_dropChance);
        }*/

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

    /*void UpdateChances()
    {
        commonItemsChance = (int)(dropChance * 0.6);
        uncommonItemsChance = (int)(dropChance * 0.3);
        rareItemsChance = (int)(dropChance * 0.1);
        if (dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance) != 0) dropChance -= dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance);
    }

    void UpdateChances(int randomNumber)
    {
        if (randomNumber< rareItemsChance)
        {
            commonItemsChance = (int)(dropChance * 0.65);
            uncommonItemsChance = (int)(dropChance * 0.35);
            rareItemsChance = 0;
            if (dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance) != 0) dropChance -= dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance);

        }
        else if (randomNumber < rareItemsChance+uncommonItemsChance)
        {
            commonItemsChance = (int)(dropChance * 0.65);
            uncommonItemsChance = (int)(dropChance * 0.20);
            rareItemsChance = (int)(dropChance * 0.15);
            if (dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance) != 0) dropChance -= dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance);

        }
        else if (randomNumber < rareItemsChance + uncommonItemsChance+ commonItemsChance)
        {
            commonItemsChance = (int)(dropChance * 0.5);
            uncommonItemsChance = (int)(dropChance * 0.35);
            rareItemsChance = (int)(dropChance * 0.15);
            if (dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance) != 0) dropChance -= dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance);

        }
        else
        {
            commonItemsChance = (int)(dropChance * 0.6);
            uncommonItemsChance = (int)(dropChance * 0.3);
            rareItemsChance = (int)(dropChance * 0.1);
            if (dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance) != 0) dropChance -= dropChance - (commonItemsChance + uncommonItemsChance + rareItemsChance);

        }


    }*/
}
