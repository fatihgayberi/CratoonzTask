using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrops : MonoBehaviour
{
    public GameObject [] Drops; 
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                CreateTable(x, y);
            }
        }
    }

    int CreateRandDropNum()
    {
        int rand = Random.Range(0, Drops.Length);

        return rand;
    }

    void CreateTable(int x, int y)
    {
        GameObject drop = GameObject.Instantiate(Drops[CreateRandDropNum()], new Vector2(x, y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
