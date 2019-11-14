using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{
    protected PlayerController player;
    public float speed;
    
    public Transform spawnPoint;
    public Transform destructionPoint;

    public ItemHolder[] itemsToSpawn; //These items will spawn randomly 
    public Table table;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("GenerateItem", 2.0f, 1.5f/speed);
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveItems();
        
        DestroyAtEnd();

    }

    void GenerateItem()
    {
        //Créer un système de pool d'objet random a instancier
        Table slot = Instantiate(table, spawnPoint.position, Quaternion.identity, transform.Find("Slots").transform);
        if (Random.Range(0, 5) == 0) table.item = itemsToSpawn[0];
        else table.item = null;
    }

    void MoveItems()
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
