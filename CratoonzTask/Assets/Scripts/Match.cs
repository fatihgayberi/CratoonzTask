using System.Collections;
using System.Collections.Generic;
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
        
        //sag swipe
        if ((x2 - x1) == 1) 
        {
            firstCountX1 = 1;
            secondCountX1 = 1;

            i1 = x1 + 2;
            while (i1 < table.width && table.allDrops[i1, y1] != null && table.allDrops[i1, y1].name == table.allDrops[x1, y1].name)
            {
                // birinci dropun yer degistikten sonraki konumunun sagini kontrol eder
                firstCountX1++;
                i1++;
            }

            i2 = x2 - 2;
            while (0 <= i2 && table.allDrops[i2, y2] != null && table.allDrops[i2, y2].name == table.allDrops[x2, y2].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun solunu kontrol eder
                secondCountX1++;
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

            if (firstCountX1 >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                Debug.Log("if1");
                Destroy(table.allDrops[x1, y1]);
                table.allDrops[x1, y1] = null;
                for (int i = x1 + 2; i <= x1 + firstCountX1; i++)
                {
                    Destroy(table.allDrops[i, y1]);
                    table.allDrops[i, y1] = null;
                }
            }

            if (secondCountX1 >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                Debug.Log("if2");
                Destroy(table.allDrops[x2, y2]);
                table.allDrops[x2, y2] = null;
                for (int i = x2 - 2; i >= x2 - secondCountX1; i--)
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

            if (firstCountX1 >= 3 || firstCountY1 + firstCountY2 + 1 >= 3 || secondCountX1 >= 3 || secondCountY1 + secondCountY2 + 1 >= 3)
            {
                return true;
            }
            else
            {
                Debug.Log("else1");
                return false;
            }
        }
        
        // yukari swipe
        if ((y2 - y1) == 1) 
        {
            firstCountY1 = 1;
            secondCountY1 = 1;

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
                i2++;
            }

            j1 = y1 + 2;
            while (j1 < table.height && table.allDrops[x1, j1] != null && table.allDrops[x1, y1].name == table.allDrops[x1, j1].name)
            {
                // birinci dropun yer degistikten sonraki konumunun yukarisini kontrol eder
                firstCountY1++;
                j1++;
            }

            j2 = y2 - 2;
            while (0 <= j2 && table.allDrops[x2, j2] != null && table.allDrops[x2, y2].name == table.allDrops[x2, j2].name)
            {
                // ikinci dropun yer degistikten sonraki konumunun altını kontrol eder
                secondCountY1++;
                j2--;
            }

            if (firstCountX1 + firstCountX2 + 1 >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                Destroy(table.allDrops[x1, y1]);
                table.allDrops[x1, y1] = null;

                for (int i = x2 + 1; i <= x2 + firstCountX1; i++)
                {
                    Destroy(table.allDrops[i, y2]);
                    table.allDrops[i, y2] = null;
                }

                for (int i = x2 - 1; i >= x2 - firstCountX2; i--)
                {
                    Destroy(table.allDrops[i, y2]);
                    table.allDrops[i, y2] = null;
                }
            }

            if (secondCountX1 + secondCountX2 + 1 >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (table.allDrops[x2, y2] != null)
                {
                    Destroy(table.allDrops[x2, y2]);
                    table.allDrops[x2, y2] = null;
                }

                for (int i = x1 + 1; i <= x1 + secondCountX1; i++)
                {
                    Destroy(table.allDrops[i, y1]);
                    table.allDrops[i, y1] = null;
                }

                for (int i = x1 - 1; i >= x1 - secondCountX2; i--)
                {
                    Destroy(table.allDrops[i, y1]);
                    table.allDrops[i, y1] = null;
                }

            }

            if (firstCountY1 >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (table.allDrops[x1, y1] != null)
                {
                    Destroy(table.allDrops[x1, y1]);
                    table.allDrops[x1, y1] = null;
                }
                for (int i = y1 + 2; i <= y1 + firstCountY1; i++)
                {
                    Destroy(table.allDrops[x1, i]);
                    table.allDrops[x1, i] = null;
                }
            }

            if (secondCountY1 >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (table.allDrops[x2, y2] != null)
                {
                    Destroy(table.allDrops[x2, y2]);
                    table.allDrops[x2, y2] = null;
                }
                for (int i = y2 - 2; i >= y2 - secondCountY1; i--)
                {
                    Destroy(table.allDrops[x1, i]);
                    table.allDrops[x1, i] = null;
                }
            }

            if (firstCountX1 + firstCountX2 + 1 >= 3 || secondCountX1 + secondCountX2 + 1 >= 3 || secondCountY1 >= 3 || firstCountY1 >= 3)
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
