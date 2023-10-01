using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShipTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SpaceShip, Star1, Star2, Star3;
    public Text SpaceCounter;

    public int TakedStar = 0;
    public readonly int RequiredStar = 3;

    public void OnPointerDown(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData) { StartTask(); }

    public void CheckTask()
    {
        if (TakedStar == RequiredStar) { Completer(); }
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
        SpaceCounter.text = "����������� ����:" + TakedStar;
        Star.SetActive(false);
    }

}