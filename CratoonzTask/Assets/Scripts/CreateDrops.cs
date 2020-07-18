using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrops : MonoBehaviour
{
    static int height = 8, width = 8; // tablonun yuksekligini ve genisligini tutar
    public GameObject[] Drops;
    int[,] dropArray = new int[height, width];
    // Start is called before the first frame update
    void Start()
    {
        CreateGame(height, width);
    }

    void CreateGame(int n, int m)
    {
        for (int i = 0; i < n; i += 1) // satir
        {
            for (int j = 0; j < m; j++) // sutun
            {
                dropArray[i, j] = Random.Range(0, Drops.Length);
            }
        }
               

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                GameObject.Instantiate(Drops[dropArray[i, j]], new Vector2(i, j), Quaternion.identity);
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {

    }
}