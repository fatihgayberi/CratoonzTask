using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDrop : MonoBehaviour
{
    public int firstX, firstY, finalX, finalY;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public Vector2 touchObject;
    public CreateDrops table;
    private GameObject emptyDrop;
    public float tangent;
    // Start is called before the first frame update
    void Start()
    {

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
                Touch touch = Input.GetTouch(0);
                touchObject = Camera.main.ScreenToWorldPoint(touch.position);
                firstTouchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                
                if (0 < touchObject.x && touchObject.x < CreateDrops.width && 0 < touchObject.y && touchObject.y < CreateDrops.height)
                {
                    firstX = (int)(touchObject.x + 0.5f);
                    firstY = (int)(touchObject.y + 0.5f);
                }
                
                Debug.Log("First: " + firstX + ", " + firstY);
                //Debug.Log("FirstPosition: " + firstTouchPosition.x);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Touch touch = Input.GetTouch(0);
                touchObject = Camera.main.ScreenToWorldPoint(touch.position);
                finalTouchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                TangentCalculator();

                if (0 < touchObject.x && touchObject.x < CreateDrops.width && 0 < touchObject.y && touchObject.y < CreateDrops.height)
                {
                    finalX = (int)(touchObject.x + 0.5f);
                    finalY = (int)(touchObject.y + 0.5f);
                    
                    Swipe();
                }
                
                Debug.Log("First: " + finalX + ", " + finalY);
                //Debug.Log("FinalPosition: " + finalTouchPosition.x);
                
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
        if (tangent < 45f && tangent > -45f)
        {
            Debug.Log("hi");
            //Debug.Log("nam: " + table.allDrops[finalX, finalY].name + "\n" + finalX + ", " + finalY);
            //emptyDrop = table.allDrops[finalX, finalY];
            //table.allDrops[finalX, finalY].transform.position = new Vector2(table.allDrops[firstX, firstY].transform.position.x, table.allDrops[firstX, firstY].transform.position.y);
            //table.allDrops[finalX, finalY] = table.allDrops[firstX, firstY];
            //table.allDrops[firstX, firstY].transform.position = new Vector2(finalX, finalY);
            //table.allDrops[firstX, firstY] = emptyDrop;
        }
    }


}
