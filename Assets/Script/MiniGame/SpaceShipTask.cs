using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShipTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SpaceShip, Star1, Star2, Star3, TaskComleted;
    public Text SpaceCounter;

    Vector2 point;

    public int TakedStar = 0;
    public readonly int RequiredStar = 3;

    public void OnPointerDown(PointerEventData eventData)
    {  }

    public void OnPointerUp(PointerEventData eventData) {  StartTask(); }

    public void CheckTask(Vector2 point)
    {
        TaskComleted.SetActive(true);
        Invoke("WaitScript", 0.5f);
    }

    void WaitScript() 
    {
        TakedStar = 0;
        Star1.SetActive(true);
        Star2.SetActive(true);
        Star3.SetActive(true);
        SpaceShip.transform.position = point;
        SpaceCounter.text = "Колличество звёзд:" + TakedStar;
        Completer();
    }

    public void StartTask()
    {
        TaskComleted.SetActive(false);
        switch (TakedStar)
        {
            case 0:
                point = SpaceShip.transform.position;
                PickUpStar(Star1);
                break;
            case 1:
                PickUpStar(Star2);
                break;
            case 2:
                PickUpStar(Star3);
                break;
        }

        if (TakedStar == RequiredStar) CheckTask(point);
    }

    public void PickUpStar(GameObject Star)
    {
        SpaceShip.transform.position = Star.transform.position;
        TakedStar ++;
        SpaceCounter.text = "Колличество звёзд:" + TakedStar;
        Star.SetActive(false);
    }

}