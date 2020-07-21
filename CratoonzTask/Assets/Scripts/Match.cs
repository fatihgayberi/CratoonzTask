using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using TMPro;
using UnityEngine;

public class Match : MonoBehaviour
{
    public Table table;

    // Start is called before the first frame update
    void Start()
    {
        table = FindObjectOfType<Table>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string DropName(int x, int y)
    {
        return table.allDrops[x, y].name;
    }

    bool DropNull(int x, int y)
    {
        if (table.allDrops[x, y] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void DropRemove(int x, int y)
    {
        Destroy(table.allDrops[x, y]);
        table.allDrops[x, y] = null;
    }

    public bool MatchDrop(int x1, int y1, int x2, int y2)
    {
        int firstCountX1 = 0;
        int firstCountX2 = 0;
        int secondCountX1 = 0;
        int secondCountX2 = 0;
        int firstCountY1 = 0;
        int firstCountY2 = 0;
        int secondCountY1 = 0;
        int secondCountY2 = 0;
        int i1, j1, i2, j2;

        //sag/sol swipe
        if (Mathf.Abs(x2 - x1) == 1)
        {
            // birinci dropun yer degistikten sonraki konumunun ustunu kontrol eder
            j1 = y1 + 1;
            while (j1 < table.height && DropNull(x2, j1) && DropName(x1, y1) == DropName(x2, j1))
            {
                firstCountY1++;
                j1++;
            }

            // birinci dropun yer degistikten sonraki konumunun altini kontrol eder
            j1 = y1 - 1;
            while (0 <= j1 && DropNull(x2, j1) && DropName(x1, y1) == DropName(x2, j1))
            {
                firstCountY2++;
                j1--;
            }

            // ikinci dropun yer degistikten sonraki konumunun ustunu kontrol eder
            j2 = y2 + 1;
            while (j2 < table.height && DropNull(x1, j2) && DropName(x2, y2) == DropName(x1, j2))
            {
                secondCountY1++;
                j2++;
            }

            // ikinci dropun yer degistikten sonraki konumunun altini kontrol eder
            j2 = y2 - 1;
            while (0 <= j2 && DropNull(x1, j2) && DropName(x2, y2) == DropName(x1, j2))
            {
                secondCountY2++;
                j2--;
            }

            // sag
            if ((x2 - x1) == 1)
            {
                // birinci dropun yer degistikten sonraki konumunun sagini kontrol eder
                i1 = x2 + 1;
                while (i1 < table.width && DropNull(i1, y1) && DropName(i1, y1) == DropName(x1, y1))
                {
                    firstCountX1++;
                    i1++;
                }

                // ikinci dropun yer degistikten sonraki konumunun solunu kontrol eder
                i2 = x1 - 1;
                while (0 <= i2 && DropNull(i2, y2) && DropName(i2, y2) == DropName(x2, y2))
                {
                    secondCountX1++;
                    i2--;
                }

                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (firstCountX1 + 1 >= 3)
                {
                    DropRemove(x1, y1);
                    for (int i = x2 + 1; i <= x2 + firstCountX1; i++)
                    {
                        DropRemove(i, y1);
                    }
                }

                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (secondCountX1 + 1 >= 3)
                {
                    DropRemove(x2, y2);
                    for (int i = x1 - 1; i >= x1 - secondCountX1; i--)
                    {
                        DropRemove(i, y2);
                    }
                }
            }

            // sol
            if ((x2 - x1) == -1)
            {
                // birinci dropun yer degistikten sonraki konumunun solunu kontrol eder
                i1 = x2 - 1;
                while (0 <= i1 && DropNull(i1, y2) && DropName(x1, y1) == DropName(i1, y2))
                {
                    firstCountX1++;
                    i1--;
                }

                // ikinci dropun yer degistikten sonraki konumunun sagını kontrol eder
                i2 = x1 + 1;
                while (i1 < table.width && DropNull(i2, y1) && DropName(x2, y2) == DropName(i2, y1))
                {
                    secondCountX1++;
                    i2++;
                }

                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (firstCountX1 + 1 >= 3)
                {
                    DropRemove(x1, y1);
                    for (int i = x2 - 1; i < x2 - firstCountX1; i--)
                    {
                        DropRemove(i, y2);
                    }
                }

                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (secondCountX1 + 1 >= 3)
                {
                    DropRemove(x2, y2);
                    for (int i = x1 + 1; i < x1 + secondCountX1; i++)
                    {
                        DropRemove(i, y1);
                    }
                }
            }

            // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
            if (firstCountY1 + firstCountY2 + 1 >= 3)
            {
                if (DropNull(x1, y1))
                {
                    DropRemove(x1, y1);
                }
                for (int i = y2 + 1; i <= y2 + firstCountY1; i++)
                {
                    DropRemove(x2, i);
                }
                for (int i = y2 - 1; i >= y2 - firstCountY2; i--)
                {
                    DropRemove(x2, i);
                }

            }

            // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
            if (secondCountY1 + secondCountY2 + 1 >= 3)
            {
                if (DropNull(x2, y2))
                {
                    DropRemove(x2, y2);
                }
                for (int i = y2 + 1; i <= y2 + secondCountY1; i++)
                {
                    DropRemove(x1, i);
                }
                for (int i = y2 - 1; i >= y2 - secondCountY2; i--)
                {
                    DropRemove(x1, i);
                }
            }

            if (firstCountX1 + 1 >= 3 || secondCountX1 + 1 >= 3 || firstCountY1 + firstCountY2 + 1 >= 3 || secondCountY1 + secondCountY2 + 1 >= 3)
            {
                return true;
            }
            else
            {
                Debug.Log("else1");
                return false;
            }
        }

        // yukari/asagi swipe
        if (Mathf.Abs(y2 - y1) == 1)
        {
            i1 = x2 + 1;
            while (i1 < table.width && table.allDrops[i1, y2] != null && table.allDrops[x1, y1].name == table.allDrops[i1, y2].name)
            {
                // birinci dropun yer degistikten sonraki konumunun sagini kontrol eder
                firstCountX1++;
                i1++;
            }

            i1 = x2 - 1;
            while (0 <= i1 && table.allDrops[i1, y2] != null && table.allDrops[x1, y1].name == table.allDrops[i1, y2].name)
            {
                // birinci dropun yer degistikten sonraki konumunun solunu kontrol eder
                firstCountX2++;
                i1--;
            }

            i2 = x1 + 1;
            while (i2 < table.width && table.allDrops[i2, y1] != null && table.allDrops[x2, y2].name == table.allDrops[i2, y1].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun sagini kontrol eder
                secondCountX1++;
                i2++;
            }

            i2 = x1 - 1;
            while (0 <= i2 && table.allDrops[i2, y1] != null && table.allDrops[x2, y2].name == table.allDrops[i2, y1].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun solunu kontrol eder
                secondCountX2++;
                i2--;
            }

            // yukariya
            if ((y2 - y1) == 1)
            {
                j1 = y2 + 1;
                while (j1 < table.height && table.allDrops[x1, j1] && table.allDrops[x1, y1].name == table.allDrops[x1, j1].name)
                {
                    // birinci dropun yer degistikten sonraki konumunun yukarisini kontrol eder
                    firstCountY1++;
                    j1++;
                }

                j2 = y1 - 1;
                while (0 <= j2 && table.allDrops[x2, j2] != null && table.allDrops[x2, y2].name == table.allDrops[x2, j2].name)
                {
                    // ikinci dropun yer degistikten sonraki konumunun asagisini kontrol eder
                    secondCountY2++;
                    j2--;
                }

                if (firstCountY1 + 1 >= 3)
                {
                    // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
                    DropRemove(x1, y1);
                    for (int i = y2 + 1; i <= y2 + firstCountY1; i++)
                    {
                        DropRemove(x1, i);
                    }
                }

                if (secondCountY2 + 1 >= 3)
                {
                    // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
                    DropRemove(x2, y2);
                    for (int i = y1 - 1; i >= y1 - secondCountY2; i--)
                    {
                        DropRemove(x2, i);
                    }
                }
            }
            
            // asagiya
            if ((y2 - y1) == -1)
            {
                // birinci dropun yer degistikten sonraki konumunun altını kontrol eder
                j1 = y2 - 1;
                while (0 <= j1 && DropNull(x2, j1) && DropName(x1, y1) == DropName(x2, j1))
                {
                    firstCountY1++;
                    j1--;
                }

                // ikinci dropun yer degistikten sonraki konumunun yukarısını kontrol eder
                j2 = y1 + 1;
                while (j2 < table.height && DropNull(x1, j2) && DropName(x2, y2) == DropName(x1, j2))
                {
                    secondCountY2++;
                    j2++;
                }

                // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (firstCountY1 + 1 >= 3)
                {
                    DropRemove(x1, y1);
                    for (int i = y2 - 1; i >= y2 - firstCountY1; i--)
                    {
                        DropRemove(x1, i);
                    }
                }

                // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (secondCountY2 + 1 >= 3)
                {
                    DropRemove(x2, y2);
                    for (int i = y1 + 1; i <= y1 + secondCountY2; i++)
                    {
                        DropRemove(x1, i);
                    }
                }
            }


            if (firstCountX1 + firstCountX2 + 1 >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (DropNull(x1, y1))
                {
                    DropRemove(x1, y1);
                }
                for (int i = x2 + 1; i <= x2 + firstCountX1; i++)
                {
                    DropRemove(i, y2);
                }
                for (int i = x2 - 1; i >= x2 - firstCountX2; i--)
                {
                    DropRemove(i, y2);
                }
            }

            if (secondCountX1 + secondCountX2 + 1 >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (DropNull(x2, y2))
                {
                    DropRemove(x2, y2);
                }
                for (int i = x1 + 1; i <= x1 + secondCountX1; i++)
                {
                    DropRemove(i, y1);
                }
                for (int i = x1 - 1; i >= x1 - secondCountX2; i--)
                {
                    DropRemove(i, y1);
                }
            }

            if (firstCountX1 + firstCountX2 + 1 >= 3 || secondCountX1 + secondCountX2 + 1 >= 3 || firstCountY1 + 1 >= 3 || secondCountY2 + 1 >= 3)
            {
                return true;
            }
            else
            {
                Debug.Log("else1");
                return false;
            }
        }

        // hatali hareket
        else
        {
            Debug.Log("else2");
            return false;
        }
    }
}
