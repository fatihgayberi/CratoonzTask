using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public int width = 8, height = 8; // tablonun yuksekligini ve genisligini tutar
    public GameObject[] drops; // temel prefablari icerir
    public GameObject[,] allDrops; // tum droplari tutar
    int[,] randArray; // randrom sayilari tutar
    // Start is called before the first frame update
    void Start()
    {
        allDrops = new GameObject[height, width]; //
        randArray = new int[height, width];
        CreateGame(height, width);
    }

    void CreateGame(int n, int m)
    {
        for (int i = 0; i < n; i++) //satir
        {
            for (int j = 0; j < m; j++) //sutun
            {
                randArray[i, j] = Random.Range(0, drops.Length);
            }
        }

        ColumnControl(n, m); // sutun control
        LineControl(n, m); // satir control

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                allDrops[i, j] = GameObject.Instantiate(drops[randArray[i, j]], new Vector2(i, j), Quaternion.identity);
            }
        }
    }

    void ColumnControl(int n, int m) // sutun control
    {
        for (int i = 0; i < n; i++) //satir
        {
            for (int j = 0; j < m - 2; j++) //sutun
            {
                while (randArray[i, j] == randArray[i, j + 1] && randArray[i, j + 1] == randArray[i, j + 2]) // sutunlari duzenler
                {
                    randArray[i, j + 2] = Random.Range(0, drops.Length);
                }
            }
        }
    }

    void LineControl(int n, int m) // satir control
    {
        for (int i = 0; i < n - 2; i += 2) //satir
        {
            for (int j = 0; j < m; j++) //sutun
            {
                while (randArray[i + 1, j] == randArray[i + 2, j]) // satirlari duzenler
                {
                    randArray[i + 2, j] = Random.Range(0, drops.Length);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}