using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElectricityTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Spark;
    public Text ElectricityCounter;
    private int Point = 0;
    private readonly int PointNumber = 20;

    public void OnPointerDown(PointerEventData eventData) {
        Spark.SetActive(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Spark.SetActive(false);
        Point++;
        ElectricityCounter.text = "Колличество ударов:" + Point;
        StartTask();
    }

    private void StartTask()
    {
        if (Point == PointNumber) 
        {
            Point = 0;
            Completer();
        }
    }
}