﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    const int width = 8, height = 8; // tablonun yuksekligini ve genisligini tutar
    public List<GameObject> drops = new List<GameObject>(); // temel prefablari icerir
    GameObject[,] allDrops; // tum droplari tutar
    int[,] randArray; // randrom sayilari tutar

    void Start()
    {
        allDrops = new GameObject[height, width];
        randArray = new int[height, width];
        CreateGame(height, width);
    }

    // cagirilan dropu return eder
    public GameObject getAllDrops(int x, int y)
    {
        return allDrops[x, y];
    }

    // dropu oyundan siler
    public void DestroyDrop(int x, int y) {
        Destroy(allDrops[x, y]);
        allDrops[x, y] = null;
    }

    // oyun tahtasinin genisligini return eder
    public int getWidth()
    {
        return width;
    }

    // oyun tahtasinin uzunlugunu return eder
    public int getHeight()
    {
        return height;
    }

    // iki dropu swap islemi yapar
    public void SwapDrop(int x1, int y1, int x2, int y2)
    {
        GameObject emptyDrop = allDrops[x1, y1];
        allDrops[x1, y1] = allDrops[x2, y2];
        allDrops[x2, y2] = emptyDrop;
    }

    // rastgele drop olusturur ve konumlandirir
    void CreateGame(int n, int m)
    {
        // rastgele drop olusturur
        for (int i = 0; i < n; i++)
        {
            // random table olusturur
            for (int j = 0; j < m; j++) 
            {
                randArray[i, j] = Random.Range(0, drops.Count);
            }
        }

        CloumnControl(n, m); // sutunlari duzenler
        LineControl(n, m); //satirlari duzenler

        // droplari konumlandirir
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                allDrops[i, j] = GameObject.Instantiate(drops[randArray[i, j]], new Vector2(i, j), Quaternion.identity);
            }
        }
    }

    // sutunda baslangic sirasinda eslesme olmamasini engeller
    void CloumnControl(int n, int m)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m - 2; j++)
            {
                while (randArray[i, j] == randArray[i, j + 1] && randArray[i, j + 1] == randArray[i, j + 2]) // sutunlari duzenler
                {
                    randArray[i, j + 2] = Random.Range(0, drops.Count);
                }
            }
        }
    }

    // sutunlarin olusum esnasinda olsuturdugu hatalari giderir
    void LineControl(int n, int m)
    {
        for (int i = 0; i < n; i++)
        {
            if (i > 1 && i < n - 2)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j < m - 2)
                    {
                        while ((randArray[i - 2, j] == randArray[i, j] && randArray[i, j] == randArray[i - 1, j])
                            || (randArray[i - 1, j] == randArray[i, j] && randArray[i, j] == randArray[i + 1, j])
                            || (randArray[i + 1, j] == randArray[i, j] && randArray[i, j] == randArray[i + 2, j])
                            || (randArray[i, j + 1] == randArray[i, j] && randArray[i, j] == randArray[i, j + 2]))
                        {
                            randArray[i, j] = Random.Range(0, drops.Count);
                        }
                    }
                    else
                    {
                        while ((randArray[i - 2, j] == randArray[i, j] && randArray[i, j] == randArray[i - 1, j]
                            || randArray[i - 1, j] == randArray[i, j] && randArray[i, j] == randArray[i + 1, j])
                            || randArray[i + 1, j] == randArray[i, j] && randArray[i, j] == randArray[i + 2, j]
                            || randArray[i, j - 1] == randArray[i, j] && randArray[i, j] == randArray[i, j - 2])
                        {
                            randArray[i, j] = Random.Range(0, drops.Count);
                        }
                    }
                }
            }
        }
    }
}