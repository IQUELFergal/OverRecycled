using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularBlocker : MonoBehaviour
{
    public GameObject[] blockers;
    int i = 0;
    float alternateTime = 15f;
    public Collision player;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Alternate", 0.0f, alternateTime);

        // Make the blockers invisible
/*        foreach (GameObject blocker in blockers)
        {
            Color alpha = blocker.GetComponent<SpriteRenderer>().color;
            alpha.a = 0f;
            blocker.GetComponent<SpriteRenderer>().color = alpha;
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // FIRST DIRECTION OF ROTATION
        if (i % 2 == 0 && moveInput.y > 0)
        {
            for (int a = 0; a <=2; a++)
            {
                blockers[a].SetActive(false);
            }
            for (int a = 3; a <= 5; a++)
            {
                blockers[a].SetActive(true);
            }
        }
        if (i % 2 == 0 && moveInput.x > 0)
        {
            blockers[7].SetActive(false);
            blockers[6].SetActive(true);
        }
        if (i % 2 == 0 && moveInput.x < 0)
        {
            blockers[6].SetActive(false);
            blockers[7].SetActive(true);
        }
        if (i % 2 == 0 && moveInput.y < 0)
        {
            for (int a = 0; a <= 2; a++)
            {
                blockers[a].SetActive(true);
            }
            for (int a = 3; a <= 5; a++)
            {
                blockers[a].SetActive(false);
            }
        }

        // SECOND DIRECTION OF ROTATION
        if (i % 2 != 0 && moveInput.y > 0)
        {
            for (int a = 0; a <= 2; a++)
            {
                blockers[a].SetActive(true);
            }
            for (int a = 3; a <= 5; a++)
            {
                blockers[a].SetActive(false);
            }
        }
        if (i % 2 != 0 && moveInput.x > 0)
        {
            blockers[6].SetActive(false);
            blockers[7].SetActive(true);
        }
        if (i % 2 != 0 && moveInput.x < 0)
        {
            blockers[7].SetActive(false);
            blockers[6].SetActive(true);
        }
        if (i % 2 != 0 && moveInput.y < 0)
        {
            for (int a = 0; a <= 2; a++)
            {
                blockers[a].SetActive(false);
            }
            for (int a = 3; a <= 5; a++)
            {
                blockers[a].SetActive(true);
            }
        }
    }

    void Alternate()
    {
        i++;
    }
}
