using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDrop : MonoBehaviour
{
    public int firstX, firstY;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public CreateDrops table;
    private GameObject emptyDrop;
    public float tangent;
    public bool flag1 = false, flag2 = false;
    // Start is called before the first frame update
    void Start()
    {
        table = FindObjectOfType<CreateDrops>();
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
    }

    void Touch()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                firstTouchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);

                if (hit.collider != null)
                {
                    firstX = (int)hit.collider.transform.position.x;
                    firstY = (int)hit.collider.transform.position.y;
                    Debug.Log("First: " + firstX + ", " + firstY);
                }

            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                finalTouchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                if (hit.collider != null)
                {
                    TangentCalculator();
                    Swipe();
                }
            }
        }
    }

    void TangentCalculator()
    {
        tangent = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log("Anguler: " + tangent);
    }

    void Swipe()
    {
        if (tangent < 45f && tangent > -45f && firstX < table.width - 1) // sag
        {
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX + 1, firstY];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX + 1, firstY] = emptyDrop;
            table.allDrops[firstX + 1, firstY].transform.position = new Vector2(firstX + 1, firstY);
        }

        else if (tangent > 45f && tangent < 135f && firstY < table.height - 1) // yukari
        {
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY + 1];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX, firstY + 1] = emptyDrop;
            table.allDrops[firstX, firstY + 1].transform.position = new Vector2(firstX, firstY + 1);
        }

        else if ((tangent > 135f || tangent < -135f) && firstX > 0) // sol
        {
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX - 1, firstY];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX - 1, firstY] = emptyDrop;
            table.allDrops[firstX - 1, firstY].transform.position = new Vector2(firstX - 1, firstY);
        }

        else if (tangent < -45f && tangent > -135f && firstY > 0) // asagi
        {
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY - 1];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX, firstY - 1] = emptyDrop;
            table.allDrops[firstX, firstY - 1].transform.position = new Vector2(firstX, firstY - 1);
        }
    }
}
