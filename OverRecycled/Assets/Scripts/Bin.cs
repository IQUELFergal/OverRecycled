using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bin : MonoBehaviour
{
    public Image hitman;
    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    private bool moving;

    public float timeBeteweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;

    private Vector2 moveDirection;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = timeBeteweenMove;
        timeToMoveCounter = timeToMove;
        hitman.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            i = 1;
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
            //FindObjectOfType<AudioManager>().Play("Pet");
            hitman.enabled = true;
        }
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.B) && !Input.GetKeyDown(KeyCode.U) && !Input.GetKeyDown(KeyCode.C) && !Input.GetKeyDown(KeyCode.H) && !Input.GetKeyDown(KeyCode.E))
        {
            i = 0;
        }

        if (hitman.enabled == true)
        {
            if (moving)
            {
                timeToMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = moveDirection;

                if (timeToMoveCounter < 0f)
                {
                    moving = false;
                    timeBetweenMoveCounter = timeBeteweenMove;
                }
            }
            else
            {
                timeBetweenMoveCounter -= Time.deltaTime;
                myRigidbody.velocity = Vector2.zero;

                if (timeBetweenMoveCounter < 0f)
                {
                    moving = true;
                    timeToMoveCounter = timeToMove;

                    moveDirection = new Vector2(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed);
                    Vector2 position = transform.position;
                    position.x = Mathf.Clamp(transform.position.x, -1.0f, 1.0f);
                    position.y = Mathf.Clamp(transform.position.y, -1.0f, 1.0f);
                    transform.position = position;
                }
            }
        }
    }

    public void Destroy()
    {
        //FindObjectOfType<AudioManager>().Play("Pet");
        Destroy(hitman);
        FindObjectOfType<Score>().AddScore(100);
    }
}
