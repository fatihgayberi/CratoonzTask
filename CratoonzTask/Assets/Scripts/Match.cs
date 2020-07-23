using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Match : MonoBehaviour
{
    Table table;
    Swipe swipe;
    Animator anim1, anim2, fallAnim1, fallAnim2;
    int firstCountX1;
    int firstCountX2;
    int secondCountX1;
    int secondCountX2;
    int firstCountY1;
    int firstCountY2;
    int secondCountY1;
    int secondCountY2;
    int i1, j1, i2, j2;
    bool next = false;

    // Start is called before the first frame update
    void Start()
    {
        table = FindObjectOfType<Table>();
        swipe = FindObjectOfType<Swipe>();
    }

    public bool getNext()
    {
        return next;
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

    void AnimationFall(string fall1, string fall2, int x1, int y1, int x2, int y2)
    {
        fallAnim1 = table.getAllDrops(x1, y1).GetComponent<Animator>();
        fallAnim1.SetBool(fall1, true);
        fallAnim2 = table.getAllDrops(x2, y2).GetComponent<Animator>();
        fallAnim2.SetBool(fall2, true);
    }

    // eslesme olunca olmasi gereken animasyonlari oynatir
    void DropAnimation(string direction1, string direction2, int x1, int y1, int x2, int y2)
    {
        anim1 = table.getAllDrops(x1, y1).GetComponent<Animator>();
        anim1.SetBool(direction1, true);
        anim2 = table.getAllDrops(x2, y2).GetComponent<Animator>();
        anim2.SetBool(direction2, true);
    }

    // degiskenlere 0 atar
    void ValueReset()
    {
        firstCountX1 = 0;
        firstCountX2 = 0;
        secondCountX1 = 0;
        secondCountX2 = 0;
        firstCountY1 = 0;
        firstCountY2 = 0;
        secondCountY1 = 0;
        secondCountY2 = 0;
        i1 = 0;
        i2 = 0;
        j1 = 0;
        j2 = 0;
    }

    // dropun animasyonlarini oynatir ve eksenlerdeki droplar destroy eder 
    IEnumerator MatchingAnimation(int x1, int y1, int x2, int y2)
    {
        // sag-sol
        if (Mathf.Abs(x2 - x1) == 1)
        {
            // eslesme oldu ise animasyonları gercekler ve dropları destroy eder
            if (firstCountX1 + 1 >= 3 || secondCountX1 + 1 >= 3 || firstCountY1 + firstCountY2 + 1 >= 3 || secondCountY1 + secondCountY2 + 1 >= 3)
            {
                next = true;

                // sag
                if ((x2 - x1) == 1)
                {
                    DropAnimation("Right", "Left", x1, y1, x2, y2);
                }

                // sol
                if ((x2 - x1) == -1)
                {
                    DropAnimation("Left", "Right", x1, y1, x2, y2);
                }

                yield return new WaitForSeconds(1.2f);

                // sag
                if ((x2 - x1) == 1)
                {
                    if (firstCountX1 + 1 >= 3)
                    {
                        table.DestroyDrop(x1, y1);

                        for (int i = x2 + 1; i <= x2 + firstCountX1; i++)
                        {
                            table.DestroyDrop(i, y1);
                        }
                    }

                    // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                    if (secondCountX1 + 1 >= 3)
                    {
                        table.DestroyDrop(x2, y2);

                        for (int i = x1 - 1; i >= x1 - secondCountX1; i--)
                        {
                            table.DestroyDrop(i, y2);
                        }
                    }
                }

                //sol
                if ((x2 - x1) == -1)
                {
                    // birinci dropun yer degistikten sonraki konumunun x eksenini temizler
                    if (firstCountX1 + 1 >= 3)
                    {
                        table.DestroyDrop(x1, y1);

                        for (int i = x2 - 1; i <= x2 - firstCountX1; i--)
                        {
                            table.DestroyDrop(i, y2);
                        }
                    }

                    // ikinci dropun yer degistikten sonraki konumunun x eksenini temizler
                    if (secondCountX1 + 1 >= 3)
                    {
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

            }
        }

        // yukari-asagi
        if (Mathf.Abs(y2 - y1) == 1)
        {
            // eslesme oldu ise animasyonları gercekler ve dropları destroy eder
            if (firstCountX1 + firstCountX2 + 1 >= 3 || secondCountX1 + secondCountX2 + 1 >= 3 || firstCountY1 + 1 >= 3 || secondCountY2 + 1 >= 3)
            {
                next = true;

                // yukari
                if ((y2 - y1) == 1)
                {
                    DropAnimation("Up", "Down", x1, y1, x2, y2);
                }

                // asagi
                if ((y2 - y1) == -1)
                {
                    DropAnimation("Down", "Up", x1, y1, x2, y2);
                }

                yield return new WaitForSeconds(1.2f);

                // yukari
                if ((y2 - y1) == 1)
                {
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

                // asagi
                if ((y2 - y1) == -1)
                {
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

            }
        }

        //
        if (!next)
        {
            switch (swipe.getStatus())
            {
                case "Right":
                    AnimationFall("RightReturn", "LeftReturn", x1, y1, x2, y2);
                    break;
                case "Up":
                    AnimationFall("UpReturn", "DownReturn", x1, y1, x2, y2);
                    break;
                case "Left":
                    AnimationFall("LeftReturn", "RightReturn", x1, y1, x2, y2);
                    break;
                case "Down":
                    AnimationFall("DownReturn", "UpReturn", x1, y1, x2, y2);
                    break;
                default:
                    break;
            }
        }
    }

    // eslesme olup olmadigini return eder
    public void MatchDrop(int x1, int y1, int x2, int y2)
    {
        ValueReset();

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
            }

            StartCoroutine(MatchingAnimation(x1, y1, x2, y2));

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
            }

            StartCoroutine(MatchingAnimation(x1, y1, x2, y2));
        }

    }
}
