using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElectricityTask : Task, IPointerDownHandler, IPointerUpHandler
{
    private int PointNumber = 0;

    public void OnPointerDown(PointerEventData eventData) { Spark.SetActive(true); }
    public void OnPointerUp(PointerEventData eventData)
    {
        Spark.SetActive(false);
        Reqwest++;
        TaskCounter.text = "Колличество ударов:" + Reqwest;
        StartTask();
    }

    private void StartTask()
    {
        if (PointNumber == 0) { PointRND(); }
        Debug.Log(PointNumber);
        if (Reqwest == PointNumber) Stop();
    }
    public void WaitScript() { Completer(); }
    public void Stop()
    {
        TaskComleted.SetActive(true);
        
        taskComplete = true;
        Invoke("WaitScript", 0.5f);
    }

    private void PointRND() { PointNumber = rnd.Next(5, 30);}
}