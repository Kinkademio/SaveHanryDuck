using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShipTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SpaceShip;
    public Text SpaceCounter;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public int TakedStar = 0;
    public readonly int RequiredStar = 3;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Мышь нажата");
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Мышь не нажата");
        StartTask();
    }

    public void CheckTask()
    {
        if (TakedStar == RequiredStar) {
            Completer();
        }
    }
    public void StartTask()
    {
        switch (TakedStar)
        {
            case 0:
                PickUpStar(Star1);
                break;
            case 1:
                PickUpStar(Star2);
                break;
            case 2:
                PickUpStar(Star3);
                break;
        }
    CheckTask();
}

    public void PickUpStar(GameObject Star)
    {
        SpaceShip.transform.position = Star.transform.position;
        TakedStar ++;
        SpaceCounter.text = "Колличество звёзд:" + TakedStar;
        Star.SetActive(false);
    }

}