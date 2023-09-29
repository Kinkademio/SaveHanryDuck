using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShipTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SpaceShip;
    //public GameObject Star1;
    //public GameObject Star2;
    //public GameObject Star3;

    public int TakedStar = 0;
    public readonly int RequiredStar = 3;

    bool corutineWork = false;

    IEnumerator coroutine;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Мышь нажата");
        StartTask();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Мышь не нажата");
        Stop();
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
        corutineWork = false;

    }
    public void CheckTask()
    {
        if (TakedStar == RequiredStar) {
            Stop();
            Completer();
        }
    }

    public void StartTask()
    {
        if (!corutineWork)
        {
            coroutine = TestCoroutine();
            StartCoroutine(coroutine);
            corutineWork = true;
        }
    }
    IEnumerator TestCoroutine()
    {
        while (true)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpaceShip.transform.position = point;
            Debug.Log("Корабль" + " " + point);

            //Vector2 St1 = Star1.transform.position;
            //Vector2 St2 = Star2.transform.position;
            //Vector2 St3 = Star3.transform.position;
            //Debug.Log("Звезда 1" + " " + St1);
            //Debug.Log("Звезда 2" + " " + St2);
            //Debug.Log("Звезда 3" + " " + St3);

            //if (point == St1)
            //{
            //    Debug.Log("Звёздочка 1");
            //    PickUpStar(Star1);
            //}
            //if (point == St2)
            //{
            //    Debug.Log("Звёздочка 2");
            //    PickUpStar(Star2);
            //}
            //if (point == St3)
            //{
            //    Debug.Log("Звёздочка взята");
            //    PickUpStar(Star3); 
            //}
            
            CheckTask();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PickUpStar(GameObject Star)
    {
        TakedStar ++;
        Star.SetActive(false);
        Debug.Log("Звёздочка взята");
    }

}