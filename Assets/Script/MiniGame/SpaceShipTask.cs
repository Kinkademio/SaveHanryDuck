using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShipTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SpaceShip, Star1, Star2, Star3;

    public readonly int RequiredStar = 3;

    Vector2 point;

    public void Start()
    {
        point = SpaceShip.transform.position;
    }
    public void Awake()
    {
        SpaceShip.transform.position = point;
    }
    public void OnPointerDown(PointerEventData eventData) {  }

    public void OnPointerUp(PointerEventData eventData) {  StartTask(); }

    public void CheckTask(Vector2 point)
    {
        TaskComleted.SetActive(true);  
        taskComplete = true;
        Invoke("WaitScript", 0.5f);
    }

    public new void closeTask() 
    {
        Star1.SetActive(true);
        Star2.SetActive(true);
        Star3.SetActive(true);
        SpaceShip.transform.position = point;
    }
    public void WaitScript() { Completer(); }

    public void StartTask()
    {
        switch (Reqwest)
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

        if (Reqwest == RequiredStar) CheckTask(point);
    }

    public void PickUpStar(GameObject Star)
    {
        SpaceShip.transform.position = Star.transform.position;
        Reqwest++;
        TaskCounter.text = "����������� ����:" + Reqwest;
        Star.SetActive(false);
    }

}