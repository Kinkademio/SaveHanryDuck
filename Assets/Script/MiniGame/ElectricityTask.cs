using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElectricityTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Spark, TaskComleted;
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
        TaskComleted.SetActive(false);
        if (PointNumber == 0) { PointRND(); }
        Debug.Log(PointNumber);
        if (Point == PointNumber) Stop();
    }
    void WaitScript()
    {
        PointNumber = 0;
        Point = 0;
        ElectricityCounter.text = "Колличество ударов:" + Point;
        Completer();
    }


    public void Stop()
    {
        TaskComleted.SetActive(true);
        Invoke("WaitScript", 0.5f);
    }

    private void PointRND() { PointNumber = rnd.Next(10, 50);}
}