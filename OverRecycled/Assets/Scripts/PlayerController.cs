using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Déplacement
    public float moveSpeed;
    public Vector2 lastMove;
    private Rigidbody2D playerRigidbody;

    //Interaction
    public GameObject target;
    public KeyCode interactionKey;
    public Item item;
    public SpriteRenderer itemOverlay;
    private bool isInteracting;

    //Animation
    private bool isPlayerMoving;

    int i = 0;

    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        
        if (item) itemOverlay.sprite = item.GetSprite();
        else itemOverlay.sprite = null;
    }

    // Update is called once per frames
    void Update()
    {
        Move();
        Interact();

        if (Input.GetKeyDown(KeyCode.B))
        {
            i++;
        }
        if (Input.GetKeyDown(KeyCode.U) && i == 1)
        {
            i++;
        }
        if (Input.GetKeyDown(KeyCode.C) && i == 2)
        {
            i++;
        }
        if (Input.GetKeyDown(KeyCode.H) && i == 3)
        {
            i++;
        }
        if (Input.GetKeyDown(KeyCode.E) && i == 4)
        {
            FindObjectOfType<AudioManager>().Play("Pet");
        }
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.B) && !Input.GetKeyDown(KeyCode.U) && !Input.GetKeyDown(KeyCode.C) && !Input.GetKeyDown(KeyCode.H) && !Input.GetKeyDown(KeyCode.E))
        {
            i = 0;
        }
    }

    void Move()
    {
        isPlayerMoving = false;
        float speedMod = 1;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0) speedMod = 1 / Mathf.Sqrt(2); //coef de vitesse diagonale
            playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed * speedMod, Input.GetAxisRaw("Vertical") * moveSpeed * speedMod);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isPlayerMoving = true;
            if (Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0) lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        MoveItemHeld();
    }

    void MoveItemHeld()
    {
        if ((isPlayerMoving) && (lastMove != new Vector2(0, -1))) itemOverlay.transform.position = transform.position + 0.75f * new Vector3(lastMove.x, lastMove.y, 0f);
        else itemOverlay.transform.position = transform.position;

        if ((lastMove == new Vector2(0, 1))&&(Input.GetAxisRaw("Vertical")>0.5f)) itemOverlay.sortingLayerName = "BackItem";
        else itemOverlay.sortingLayerName = "FrontItem";
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (target != col.gameObject && target) Deselect();

        target = col.gameObject;
        Select();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (target != col.gameObject && target) Deselect();

        target = col.gameObject;
        Select();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == target)
        {
            Deselect();
            target = null;
        }
    }

    void Select()
    {
        Table table = target.GetComponent<Table>();
        if (table) table.Select();
    }

    void Deselect()
    {
        Table table = target.GetComponent<Table>();
        if (table) table.DeSelect();
    }
    

    public void SetItem(Item i)
    {
        item = i;
    }

    public Item GetItem()
    {
        return item;
    }

    void Interact()
    {
        if (!target) return;

        Table table = target.GetComponent<Table>();
        if (table && Input.GetKeyDown(interactionKey))
        {
            if (table.GetItem() && !item && !isInteracting) //Take
            {
                Take(table);
                StartCoroutine(UpdateInteraction());
            }
            if (!table.GetItem() && item && !isInteracting) //Drop
            {
                Drop(table);
                StartCoroutine(UpdateInteraction());
            }
            if (table.GetItem() && item && !isInteracting) //Switch
            {
                Switch(table);
                StartCoroutine(UpdateInteraction());
            }
        }

        if (item) itemOverlay.sprite = item.GetSprite();
        else itemOverlay.sprite = null;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("Pet");
            PauseMenu pause = FindObjectOfType<PauseMenu>();
            if (PauseMenu.GameIsPaused || pause.optionsMenuUI.activeSelf || pause.choiceMenuUI.activeSelf)
            {
                pause.Resume();
            }
            else
            {
                pause.Pause();
            }
        }
    }

    IEnumerator UpdateInteraction()
    {
        isInteracting = true;
        yield return new WaitForSeconds(0.1f);
        isInteracting = false;
    }

    //Take an item
    public void Take(Table table)
    {
        Debug.Log("Took " + table.item.name.ToString());
        SetItem(table.GetItem());
        table.SetItem(null);
    }

    //Drop an item
    public void Drop(Table table)
    {
        Debug.Log("Dropped " + item.name.ToString());
        table.SetItem(GetItem());
        SetItem(null);
        if (table.transform.GetComponentInParent(typeof(Machine)))
        {
            Machine machine = table.GetComponentInParent(typeof(Machine)) as Machine;
            if(machine.IsFull())
            { machine.Transform(machine.recipes[0]);}
        }
    }

    //Switch two items
    public void Switch(Table table)
    {
        Debug.Log("Switched " + item.name + " with " + table.item.name.ToString());
        Item tmpItem = GetItem();
        SetItem(table.GetItem());
        table.SetItem(tmpItem);
    }
}