using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Déplacement
    public float moveSpeed;
    public Vector2 lastMove;
    private Rigidbody2D playerRigidbody;

    //Interaction décor
    public GameObject target;
    public ItemHolder item;
    public SpriteRenderer itemOverlay;

    //Animation
    private bool isPlayerMoving;
    


    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        if (item!=null && item.HasItem()) itemOverlay.sprite = item.GetItemSprite();
        else itemOverlay.sprite = null;
    }

    // Update is called once per frames
    void Update()
    {
        Move();

        if (item)  //move item held
        {
            if (item.HasItem()) itemOverlay.sprite = item.GetItemSprite();
            MoveItemHeld();
        }
        else itemOverlay.sprite = null;

        if ((Input.GetAxisRaw("Fire1") != 0) || (Input.GetAxisRaw("Fire2") != 0))
        {
            if (!target) return;

            Table table = target.GetComponent<Table>();
            if (table)
            {
                if (Input.GetAxisRaw("Fire1") > 0.5f && table.item && !item) table.Take();
                if (Input.GetAxisRaw("Fire2") > 0.5f && !table.item && item) table.Drop();
                //if ((Input.GetAxisRaw("Fire1") > 0.5f || Input.GetAxisRaw("Fire2") > 0.5f) && table.item && item) table.Switch();
            }
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
}