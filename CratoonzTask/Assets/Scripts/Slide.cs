using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    Table table;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        table = FindObjectOfType<Table>();
    }

    // Update is called once per frame
    void Update()
    {
        //DropNullHorizontalFind(); // bu classin calismasi icin fonksiyon yorumdan cikarilmalidir
    }

    // dropun null olup olmadigini return eder
    bool DropNull(int x, int y)
    {
        if (table.getAllDrops(x, y) == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // null drop bulur
    public void DropNullHorizontalFind()
    {
        for (int i = 0; i < table.getHeight(); i++)
        {
            for (int j = 0; j < table.getWidth(); j++)
            {
                if (DropNull(i, j))
                {
                    SlideAfterHorizontalMatch(i, j);
                }
            }
        }
    }

    // dusme animasyonlarini oynatir
    void SlideAnimation(string slipeType, int x, int y)
    {
        anim = table.getAllDrops(x, y).GetComponent<Animator>();
        anim.SetBool(slipeType, true);
    }

    // yatay konumdaki null karelere slide islemi yapar
    public void SlideAfterHorizontalMatch(int x, int y)
    {        
        int j1;
        int j2;
        int counter = 0;

        j1 = y + 1;
        j2 = y;

        // null karonun ustunde kac adet drop oldugunu bulur
        while (j1 < table.getHeight() && !DropNull(x, j1))
        {
            j1++;
            counter++;
        }

        for (int i = y + 1; i <= y + counter; i++)
        {
            SlideAnimation("Slide1", x, i);
            table.SwapDrop(x, j2, x, i);
            j2++;
        }        
    }
}
