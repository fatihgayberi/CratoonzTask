using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using TMPro;
using UnityEngine;

public class Match : MonoBehaviour
{
    public Table table;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        table = FindObjectOfType<Table>();
    }

    // dropun ismini return eder
    string DropName(int x, int y)
    {
        return table.getAllDrops(x, y).name;
    }

    // dropun null olup olmadıgını return eder
    bool DropNull(int x, int y)
    {
        if (table.getAllDrops(x, y) == null)
            return false;
        else
            return true;
    }

    // eslesme olunca olmasi gereken animasyonlari oynatir
    void DropAnimation(string direction, int x, int y)
    {
        anim = table.getAllDrops(x, y).GetComponent<Animator>();
        anim.SetBool(direction, true);
    }

    // eslesme olup olmadigini return eder
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

        //sag-sol swipe
        if (Mathf.Abs(x2 - x1) == 1)
        {
            // birinci dropun yer degistikten sonraki konumunun ustunu kontrol eder
            j1 = y1 + 1;
            while (j1 < table.getHeight() && DropNull(x2, j1) && DropName(x1, y1) == DropName(x2, j1))
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
            while (j2 < table.getHeight() && DropNull(x1, j2) && DropName(x2, y2) == DropName(x1, j2))
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
                while (i1 < table.getWidth() && DropNull(i1, y1) && DropName(i1, y1) == DropName(x1, y1))
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
                    DropAnimation("Right", x1, y1);
                    table.DestroyDrop(x1, y1);
                    
                    for (int i = x2 + 1; i <= x2 + firstCountX1; i++)
                    {
                        table.DestroyDrop(i, y1);
                    }
                }

                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (secondCountX1 + 1 >= 3)
                {
                    DropAnimation("Left", x2, y2);
                    table.DestroyDrop(x2, y2);

                    for (int i = x1 - 1; i >= x1 - secondCountX1; i--)
                    {
                        table.DestroyDrop(i, y2);
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
                while (i2 < table.getWidth() && DropNull(i2, y1) && DropName(x2, y2) == DropName(i2, y1))
                {
                    secondCountX1++;
                    i2++;
                }

                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (firstCountX1 + 1 >= 3)
                {
                    DropAnimation("Left", x1, y1);

                    table.DestroyDrop(x1, y1);
                    for (int i = x2 - 1; i <= x2 - firstCountX1; i--)
                        table.DestroyDrop(i, y2);
                }

                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (secondCountX1 + 1 >= 3)
                {
                    DropAnimation("Right", x2, y2);

                    table.DestroyDrop(x2, y2);

                    for (int i = x1 + 1; i <= x1 + secondCountX1; i++)
                    {
                        table.DestroyDrop(i, y1);
                    }
                    
                }
            }

            // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
            if (firstCountY1 + firstCountY2 + 1 >= 3)
            {
                if (DropNull(x1, y1))
                {
                    if ((x2 - x1) == 1) // sag
                    {
                        DropAnimation("Right", x1, y1);
                    }

                    if ((x2 - x1) == -1) // sol
                    {
                        DropAnimation("Left", x1, y1);
                    }
                    table.DestroyDrop(x1, y1);
                }

                for (int i = y2 + 1; i <= y2 + firstCountY1; i++)
                {
                    table.DestroyDrop(x2, i);
                }
                for (int i = y2 - 1; i >= y2 - firstCountY2; i--)
                {
                    table.DestroyDrop(x2, i);
                }

            }

            // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
            if (secondCountY1 + secondCountY2 + 1 >= 3)
            {
                if (DropNull(x2, y2))
                {
                    if ((x2 - x1) == 1) // sag
                    {
                        DropAnimation("Left", x2, y2);
                    }


                    if ((x2 - x1) == -1) // sol
                    {
                        DropAnimation("Right", x2, y2);


                    }

                    //while (!AnimationFinish("Left") || AnimationFinish("Right"))
                    table.DestroyDrop(x2, y2);
                }
                for (int i = y2 + 1; i <= y2 + secondCountY1; i++)
                {
                    table.DestroyDrop(x1, i);
                }
                for (int i = y2 - 1; i >= y2 - secondCountY2; i--)
                {
                    table.DestroyDrop(x1, i);
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

        // yukari-asagi swipe
        if (Mathf.Abs(y2 - y1) == 1)
        {
            // birinci dropun yer degistikten sonraki konumunun sagini kontrol eder
            i1 = x2 + 1;
            while (i1 < table.getWidth() && DropNull(i1, y2) && DropName(x1, y1) == DropName(i1, y2))
            {
                firstCountX1++;
                i1++;
            }

            // birinci dropun yer degistikten sonraki konumunun solunu kontrol eder
            i1 = x2 - 1;
            while (0 <= i1 && DropNull(i1, y2) && DropName(x1, y1) == DropName(i1, y2))
            {
                firstCountX2++;
                i1--;
            }

            // ikinci dropun yer degistikten sonraki konumunun sagini kontrol eder
            i2 = x1 + 1;
            while (i2 < table.getWidth() && DropNull(i2, y1) && DropName(x2, y2) == DropName(i2, y1))
            {
                secondCountX1++;
                i2++;
            }

            // ikinci dropun yer degistikten sonraki konumunun solunu kontrol eder
            i2 = x1 - 1;
            while (0 <= i2 && DropNull(i2, y1) && DropName(x2, y2) == DropName(i2, y1))
            {
                secondCountX2++;
                i2--;
            }

            // yukariya
            if ((y2 - y1) == 1)
            {
                // birinci dropun yer degistikten sonraki konumunun yukarisini kontrol eder
                j1 = y2 + 1;
                while (j1 < table.getHeight() && DropNull(x1, j1) && DropName(x1, y1) == DropName(x1, j1))
                {
                    firstCountY1++;
                    j1++;
                }

                // ikinci dropun yer degistikten sonraki konumunun asagisini kontrol eder
                j2 = y1 - 1;
                while (0 <= j2 && DropNull(x2, j2) && DropName(x2, y2) == DropName(x2, j2))
                {
                    secondCountY2++;
                    j2--;
                }

                // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (firstCountY1 + 1 >= 3)
                {
                    table.DestroyDrop(x1, y1);
                    for (int i = y2 + 1; i <= y2 + firstCountY1; i++)
                    {
                        table.DestroyDrop(x1, i);
                    }
                }

                // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (secondCountY2 + 1 >= 3)
                {
                    table.DestroyDrop(x2, y2);
                    for (int i = y1 - 1; i >= y1 - secondCountY2; i--)
                    {
                        table.DestroyDrop(x2, i);
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
                while (j2 < table.getHeight() && DropNull(x1, j2) && DropName(x2, y2) == DropName(x1, j2))
                {
                    secondCountY2++;
                    j2++;
                }

                // birinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (firstCountY1 + 1 >= 3)
                {
                    table.DestroyDrop(x1, y1);
                    for (int i = y2 - 1; i >= y2 - firstCountY1; i--)
                    {
                        table.DestroyDrop(x1, i);
                    }
                }

                // ikinci dropun yer degistikten sonraki konumunun y eksenini temizler
                if (secondCountY2 + 1 >= 3)
                {
                    table.DestroyDrop(x2, y2);
                    for (int i = y1 + 1; i <= y1 + secondCountY2; i++)
                    {
                        table.DestroyDrop(x1, i);
                    }
                }
            }


            if (firstCountX1 + firstCountX2 + 1 >= 3)
            {
                // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (DropNull(x1, y1))
                {
                    table.DestroyDrop(x1, y1);
                }
                for (int i = x2 + 1; i <= x2 + firstCountX1; i++)
                {
                    table.DestroyDrop(i, y2);
                }
                for (int i = x2 - 1; i >= x2 - firstCountX2; i--)
                {
                    table.DestroyDrop(i, y2);
                }
            }

            if (secondCountX1 + secondCountX2 + 1 >= 3)
            {
                // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                if (DropNull(x2, y2))
                {
                    table.DestroyDrop(x2, y2);
                }
                for (int i = x1 + 1; i <= x1 + secondCountX1; i++)
                {
                    table.DestroyDrop(i, y1);
                }
                for (int i = x1 - 1; i >= x1 - secondCountX2; i--)
                {
                    table.DestroyDrop(i, y1);
                }
            }

            if (firstCountX1 + firstCountX2 + 1 >= 3 || secondCountX1 + secondCountX2 + 1 >= 3 || firstCountY1 + 1 >= 3 || secondCountY2 + 1 >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // hatali hareket
        else
        {
            return false;
        }
    }
}
