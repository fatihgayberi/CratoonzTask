using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class SwipeDrop : MonoBehaviour
{
    public int firstX, firstY;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public Table table;
    private GameObject emptyDrop;
    public float tangent;
    public bool flag1 = false, flag2 = false;
    void Start()
    {
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
                    Swipe();
                }
            }
        }
    }

    void TangentCalculator()
    {
        tangent = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    }

    void Swipe() // acilardan sonrasini kontrol amacli silmeyi unutma 
    {
        if (tangent < 45f && tangent > -45f && firstX < table.width - 1) // sag
        {
            //Debug.Log("sag");
            Debug.Log("First: " + firstX + ", " + firstY);
            Debug.Log("Second: " + (firstX + 1) + ", " + firstY);

            if (Match(firstX, firstY, firstX + 1, firstY))
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
            //Debug.Log("yukari");
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX, firstY + 1];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX, firstY + 1] = emptyDrop;
            table.allDrops[firstX, firstY + 1].transform.position = new Vector2(firstX, firstY + 1);
        }

        else if ((tangent > 135f || tangent < -135f) && firstX > 0) // sol
        {
            //Debug.Log("sol");
            emptyDrop = table.allDrops[firstX, firstY];
            table.allDrops[firstX, firstY] = table.allDrops[firstX - 1, firstY];
            table.allDrops[firstX, firstY].transform.position = new Vector2(firstX, firstY);
            table.allDrops[firstX - 1, firstY] = emptyDrop;
            table.allDrops[firstX - 1, firstY].transform.position = new Vector2(firstX - 1, firstY);
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

    bool Match(int x1, int y1, int x2, int y2)
    {
        int firstCountX = 1;
        int secondCountX = 1;
        int firstCountY1 = 0;
        int firstCountY2 = 0;
        int secondCountY1 = 0;
        int secondCountY2 = 0;

        int  i1, j1, i2, j2;
        if ((x2 - x1) == 1) //sag swipe
        {
            i1 = x1 + 2;
            while (i1 < table.width && table.allDrops[i1, y1] != null && table.allDrops[i1, y1].name == table.allDrops[x1, y1].name)
            {
                // birinci dropun yer degistikten sonraki konumunun sagini kontrol eder
                firstCountX++;
                i1++;
            }

            i2 = x2 - 2;
            while (0 <= i2 && table.allDrops[i2, y2] != null && table.allDrops[i2, y2].name == table.allDrops[x2, y2].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun solunu kontrol eder
                secondCountX++;
                i2--;
            }

            j1 = y1 + 1;
            while (j1 < table.height && table.allDrops[x2, j1] != null && table.allDrops[x1, y1].name == table.allDrops[x2, j1].name)
            {
                // birinci dropun yer degistikten sonraki konumunun ustunu kontrol eder
                firstCountY1++;
                j1++;
            }

            j1 = y1 - 1;
            while (0 <= j1 && table.allDrops[x2, j1] != null && table.allDrops[x1, y1].name == table.allDrops[x2, j1].name)
            {
                // birinci dropun yer degistikten sonraki konumunun altini kontrol eder
                firstCountY2++;
                j1--;
            }

            j2 = y2 + 1;
            while (j2 < table.height && table.allDrops[x1, j2] != null && table.allDrops[x2, y2].name == table.allDrops[x1, j2].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun ustunu kontrol eder
                secondCountY1++;
                j2++;
            }

            j2 = y2 - 1;
            while (0 <= j2 && table.allDrops[x1, j2] != null && table.allDrops[x2, y2].name == table.allDrops[x1, j2].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun altini kontrol eder
                secondCountY2++;
                j2--;
            }

            if (firstCountX >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                Debug.Log("if1");
                Destroy(table.allDrops[x1, y1]);
                table.allDrops[x1, y1] = null;
                for (int i = x1 + 2; i <= x1 + firstCountX; i++)
                {
                    Destroy(table.allDrops[i, y1]);
                    table.allDrops[i, y1] = null;
                }
            }

            if (secondCountX >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                Debug.Log("if2");
                Destroy(table.allDrops[x2, y2]);
                table.allDrops[x2, y2] = null;
                for (int i = x2 - 2; i >= x2 - secondCountX; i--)
                {
                    Destroy(table.allDrops[i, y2]);
                    table.allDrops[i, y2] = null;
                }
            }

            if (firstCountY1 + firstCountY2 + 1 >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (table.allDrops[x1, y1] != null)
                {
                    Destroy(table.allDrops[x1, y1]);
                    table.allDrops[x1, y1] = null;
                }
                for (int i = y2 + 1; i <= y2 + firstCountY1; i++)
                {
                    Destroy(table.allDrops[x2, i]);
                    table.allDrops[x2, i] = null;
                }
                for (int i = y2 - 1; i >= y2 - firstCountY2; i--)
                {
                    Destroy(table.allDrops[x2, i]);
                    table.allDrops[x2, i] = null;
                }

            }

            if (secondCountY1 + secondCountY2 + 1 >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (table.allDrops[x2, y2] != null)
                {
                    Destroy(table.allDrops[x2, y2]);
                    table.allDrops[x2, y2] = null;
                }
                for (int i = y2 + 1; i <= y2 + secondCountY1; i++)
                {
                    Destroy(table.allDrops[x1, i]);
                    table.allDrops[x1, i] = null;
                }
                for (int i = y2 - 1; i >= y2 - secondCountY2; i--)
                {
                    Destroy(table.allDrops[x1, i]);
                    table.allDrops[x1, i] = null;
                }
            }

            if (firstCountX >=3 || firstCountY1 + firstCountY2 + 1 >= 3 || secondCountX >= 3 || secondCountY1 + secondCountY2 + 1 >= 3)
            {
                return true;
            }
            else
            {
                Debug.Log("else1");
                return false;
            }
        }
        else
        {
            Debug.Log("else2");
            return false;
        }
        
    }
}
