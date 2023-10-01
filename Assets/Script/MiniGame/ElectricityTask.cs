using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElectricityTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Spark;
    public Text ElectricityCounter;

    private int Point = 0, PointNumber = 0;

    System.Random rnd = new();

    public void OnPointerDown(PointerEventData eventData) { Spark.SetActive(true); }
    public void OnPointerUp(PointerEventData eventData)
    {
        Spark.SetActive(false);
        Point++;
        ElectricityCounter.text = "Колличество ударов:" + Point;
        StartTask();
    }

    private void StartTask()
    {
        if (PointNumber == 0) { PointRND(); }
        Debug.Log(PointNumber);
        if (Point == PointNumber) 
        {
            PointNumber = 0;
            Point = 0;
            Completer();
        }
    }
    private void PointRND() { PointNumber = rnd.Next(10, 50);}
}