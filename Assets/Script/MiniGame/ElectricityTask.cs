using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElectricityTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Spark;
    private int Point = 0;
    private readonly int PointNumber = 50;

    public void OnPointerDown(PointerEventData eventData) {
        Spark.SetActive(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Spark.SetActive(false);
        Point++;
        StartTask();
    }

    private void StartTask()
    {
        if (Point == PointNumber) 
        {
            ElectricityTaskComplete = true;
            Completer();
        }
    }

}