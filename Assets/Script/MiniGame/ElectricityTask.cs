using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElectricityTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Spark;
    private int PointNumber = 0;

    public void OnPointerDown(PointerEventData eventData) { Spark.SetActive(true); }
    public void OnPointerUp(PointerEventData eventData)
    {
        Spark.SetActive(false);
        Reqwest++;
        TaskCounter.text = "Колличество ударов:" + Reqwest;
        StartTask();
    }

    public override void closeTask()
    {
        Spark.SetActive(false);
        base.closeTask();
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
        int fixedObjects = int.Parse(ScoreController.getCurrentScoreByName("fixed_objects").scoreValue);
        fixedObjects++;
        ScoreController.setCurrentScoreNewValue("fixed_objects", fixedObjects.ToString());
        taskComplete = true;
        Invoke("WaitScript", 0.5f);
    }

    private void PointRND() { PointNumber = rnd.Next(5, 30);}
}