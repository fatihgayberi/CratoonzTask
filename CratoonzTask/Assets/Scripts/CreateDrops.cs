using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrops : MonoBehaviour
{
    public static int width = 8, height = 8; // tablonun yuksekligini ve genisligini tutar
    public GameObject[] drops;
    public GameObject[,] allDrops = new GameObject[height, width]; //
    int[,] dropArray = new int[height, width];
    // Start is called before the first frame update
    void Start()
    {
        CreateGame(height, width);
    }

    void CreateGame(int n, int m)
    {
        for (int i = 0; i < n; i++) //satir
        {
            for (int j = 0; j < m; j++) //sutun
            {
                dropArray[i, j] = Random.Range(0, drops.Length);
            }
        }

        ColumnControl(n, m); // sutun control
        LineControl(n, m); // satir control

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                allDrops[i, j] = GameObject.Instantiate(drops[dropArray[i, j]], new Vector2(i, j), Quaternion.identity);
            }
        }
    }

    void ColumnControl(int n, int m) // sutun control
    {
        int randNum;

        for (int i = 0; i < n; i++) //satir
        {
            for (int j = 0; j < m - 2; j++) //sutun
            {
                while (dropArray[i, j] == dropArray[i, j + 1] && dropArray[i, j + 1] == dropArray[i, j + 2]) // sutunlari duzenler
                {
                    randNum = Random.Range(0, 3);
                    dropArray[i, j + 2] = Random.Range(0, drops.Length);
                }
            }
        }
    }

    void LineControl(int n, int m) // satir control
    {
        int randNum;

        for (int i = 0; i < n - 2; i += 2) //satir
        {
            for (int j = 0; j < m; j++) //sutun
            {
                while (dropArray[i + 1, j] == dropArray[i + 2, j]) // satirlari duzenler
                {
                    randNum = Random.Range(0, 3);
                    dropArray[i + 2, j] = Random.Range(0, drops.Length);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}