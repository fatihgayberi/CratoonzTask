using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public int firstX, firstY;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public Match match;
    public Table table;
    private GameObject emptyDrop;
    public float tangent;
    public bool flag1 = false, flag2 = false;
    void Start()
    {
        match = FindObjectOfType<Match>();
        table = FindObjectOfType<Table>();
    }

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
                    SwipeDrop();
                }
            }
        }
    }

    void TangentCalculator()
    {
        tangent = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    }

    void SwipeDrop() // acilardan sonrasini kontrol amacli silmeyi unutma 
    {
        if (tangent < 45f && tangent > -45f && firstX < table.width - 1) // sag
        {
            Debug.Log("sag");
            Debug.Log("First: " + firstX + ", " + firstY);
            Debug.Log("Second: " + (firstX + 1) + ", " + firstY);
            
            if (match.MatchDrop(firstX, firstY, firstX + 1, firstY))
            {
                Debug.Log("tamam oldu");
            
                if (table.allDrops[firstX + 1, firstY] != null)
                {
                    table.allDrops[firstX + 1, firstY].transform.position = new Vector2(firstX, firstY);
                    table.allDrops[firstX, firstY] = table.allDrops[firstX + 1, firstY];
                    
                }
                else if (table.allDrops[firstX, firstY] != null)
                {
                    table.allDrops[firstX, firstY].transform.position = new Vector2(firstX + 1, firstY);
                    table.allDrops[firstX + 1, firstY] = table.allDrops[firstX, firstY];
                }
            } else
            {
                // animasyonla ters duz olacak
            }            
        }

        else if (tangent > 45f && tangent < 135f && firstY < table.height - 1) // yukari
        {
            //Debug.Log("First: " + firstX + ", " + firstY);
            //Debug.Log("Second: " + firstX + ", " + (firstY + 1));
            //if (match.MatchDrop(firstX, firstY, firstX, firstY + 1))
            //{
            //    Debug.Log("tamam oldu");
            //
            //    if (table.allDrops[firstX, firstY + 1] != null)
            //    {
            //        table.allDrops[firstX, firstY + 1].transform.position = new Vector2(firstX, firstY);
            //        table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY + 1];
            //
            //    }
            //    else if (table.allDrops[firstX, firstY] != null)
            //    {
            //        table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY + 1);
            //        table.allDrops[firstX, firstY + 1] = table.allDrops[firstX, firstY];
            //    }
            //}
            //else
            //{
            //    // animasyonla ters duz olacak
            //}
            
            //Debug.Log("yukari");
            //emptyDrop = table.allDrops[firstX, firstY];
            //table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY + 1];
            //table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            //table.allDrops[firstX, firstY + 1] = emptyDrop;
            //table.allDrops[firstX, firstY + 1].transform.position = new Vector2(firstX, firstY + 1);
        }

        else if ((tangent > 135f || tangent < -135f) && firstX > 0) // sol
        {
            //Debug.Log("sol");
            //emptyDrop = table.allDrops[firstX, firstY];
            //table.allDrops[firstX, firstY] = table.allDrops[firstX - 1, firstY];
            //table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            //table.allDrops[firstX - 1, firstY] = emptyDrop;
            //table.allDrops[firstX - 1, firstY].transform.position = new Vector2(firstX - 1, firstY);
        }

        else if (tangent < -45f && tangent > -135f && firstY > 0) // asagı
        {
            //Debug.Log("asagı");
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY - 1];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX, firstY - 1] = emptyDrop;
            table.allDrops[firstX, firstY - 1].transform.position = new Vector2(firstX, firstY - 1);
        }
    }
}
