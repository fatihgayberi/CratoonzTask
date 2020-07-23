using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    float tangent; // tanjant degerini tutar
    string status; // hatali kaydırmayi tutar
    int firstX, firstY; // ilk dropun x ve y indexlerini tutar
    Vector2 firstTouchPosition; // ilk dokunulan noktanin positionlarını tutar
    Vector2 finalTouchPosition; // ikinci dokunulan noktanin positionlarını tutar
    Match match;
    Table table;
    Animator directionAnim;

    void Start()
    {
        match = FindObjectOfType<Match>();
        table = FindObjectOfType<Table>();
    }

    void Update()
    {
        Touch();
    }

    // status degiskenini return eder
    public string getStatus()
    {
        return status;
    }

    // dokunma islemini gercekler
    void Touch()
    {
        if (Input.touchCount == 1)
        {
            // ilk dokunulan dropu isler
            if (Input.GetTouch(0).phase == TouchPhase.Began) 
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                firstTouchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);

                // dokunulan yerin drop olup olmadigini anlar ve x-y positionlarını tutar
                if (hit.collider != null) 
                {
                    firstX = (int)Mathf.Round(hit.collider.transform.position.x); 
                    firstY = (int)Mathf.Round(hit.collider.transform.position.y);
                }
            }

            // ikinci dokunulan dropu isler
            if (Input.GetTouch(0).phase == TouchPhase.Ended) 
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                finalTouchPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);

                // dokunulan yerin drop olup olmadigini anlar
                if (hit.collider != null) 
                {
                    TangentCalculator();
                    SwipeDrop();
                }
            }
        }
    }

    // ilk ve son dokunulan noktalarin arasindaki tanjant acisini hesaplar
    void TangentCalculator() 
    {
        tangent = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    }

    // dropun bos olup olmadigini kontrol eder
    bool DropNull(int x, int y) 
    {
        if (table.getAllDrops(x, y) != null)
            return true;

        else
            return false;
    }

    // eslesme olursa gerceklesmesi gereken animasyon islemlerini yapar
    void AnimationDirection(string direction, int x1, int y1, int x2, int y2) 
    {
        directionAnim = table.getAllDrops(x1, y1).GetComponent<Animator>();
        directionAnim.SetBool(direction, true);
        table.DropTrans(x2, y2, x1, y1);
    }

    // swipe islemlerini gercekler
    void SwipeDrop()
    {
        // saga dogru yapilan swipe islemleri
        if (tangent < 45f && tangent > -45f && firstX < table.getWidth() - 1)
        {
            status = "Right";
            match.MatchDrop(firstX, firstY, firstX + 1, firstY);
        }

        // yukari dogru yapilan swipe islemleri
        else if (tangent > 45f && tangent < 135f && firstY < table.getHeight() - 1)
        {
            status = "Up";
            match.MatchDrop(firstX, firstY, firstX, firstY + 1);
        }

        // sola dogru yapilan swipe islemleri
        else if ((tangent > 135f || tangent < -135f) && firstX > 0)
        {
            status = "Left";
            match.MatchDrop(firstX, firstY, firstX - 1, firstY);
        }

        // asagi dogru yapilan swipe islemleri
        else if (tangent < -45f && tangent > -135f && firstY > 0)
        {
            status = "Down";
            match.MatchDrop(firstX, firstY, firstX, firstY - 1);
        }
    }
}
