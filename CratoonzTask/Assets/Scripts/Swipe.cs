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
    public float tangent;
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

    void SwipeDrop()
    {
        // sag
        if (tangent < 45f && tangent > -45f && firstX < table.width - 1)
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

        // yukari
        else if (tangent > 45f && tangent < 135f && firstY < table.height - 1)
        {
            Debug.Log("First: " + firstX + ", " + firstY);
            Debug.Log("Second: " + firstX + ", " + (firstY + 1));
            if (match.MatchDrop(firstX, firstY, firstX, firstY + 1))
            {
                Debug.Log("tamam oldu");
            
                if (table.allDrops[firstX, firstY + 1] != null)
                {
                    table.allDrops[firstX, firstY + 1].transform.position = new Vector2(firstX, firstY);
                    table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY + 1];
            
                }
                else if (table.allDrops[firstX, firstY] != null)
                {
                    table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY + 1);
                    table.allDrops[firstX, firstY + 1] = table.allDrops[firstX, firstY];
                }
            }
            else
            {
                // animasyonla ters duz olacak
            }
        }

        // sol
        else if ((tangent > 135f || tangent < -135f) && firstX > 0)
        {
            Debug.Log("First: " + firstX + ", " + firstY);
            Debug.Log("Second: " + firstX + ", " + (firstY + 1));
            if (match.MatchDrop(firstX, firstY, firstX - 1, firstY))
            {
                Debug.Log("tamam oldu");
            
                if (table.allDrops[firstX - 1, firstY] != null)
                {
                    table.allDrops[firstX - 1, firstY ].transform.position = new Vector2(firstX, firstY);
                    table.allDrops[firstX, firstY] = table.allDrops[firstX - 1, firstY];
            
                }
                else if (table.allDrops[firstX, firstY] != null)
                {
                    table.allDrops[firstX, firstY].transform.position = new Vector2(firstX - 1, firstY);
                    table.allDrops[firstX - 1, firstY] = table.allDrops[firstX, firstY];
                }
            }
            else
            {
                // animasyonla ters duz olacak
            }
        }

        // asagı
        else if (tangent < -45f && tangent > -135f && firstY > 0)
        {
            Debug.Log("First: " + firstX + ", " + firstY);
            Debug.Log("Second: " + firstX + ", " + (firstY - 1));
            if (match.MatchDrop(firstX, firstY, firstX, firstY - 1))
            {
                Debug.Log("tamam oldu");
            
                if (table.allDrops[firstX, firstY - 1] != null)
                {
                    table.allDrops[firstX, firstY - 1].transform.position = new Vector2(firstX, firstY);
                    table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY - 1];
            
                }
                else if (table.allDrops[firstX, firstY] != null)
                {
                    table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY - 1);
                    table.allDrops[firstX, firstY - 1] = table.allDrops[firstX, firstY];
                }
            }
            else
            {
                // animasyonla ters duz olacak
            }
        }
    }
}
