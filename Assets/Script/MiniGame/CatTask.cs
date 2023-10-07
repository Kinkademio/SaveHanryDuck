using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public Text TaskTimer;
    public void OnPointerDown(PointerEventData eventData) { StartTask(); }
    public void OnPointerUp(PointerEventData eventData) { Stop(); }
    public void Stop()
    {
        StopCoroutine();
        if (Reqwest >= minHoldTime && Reqwest <= maxHoldTime)
        {
            TaskComleted.SetActive(true);
            taskComplete = true;
            Invoke("WaitScript", 0.5f);
        }
        else if (Reqwest < minHoldTime) { TaskCounter.text = "Мало :<"; }
        else { TaskCounter.text = "Долго >:0"; }

    }

    public new void closeTask()
    {
        TaskTimer.text = "" + Reqwest;
    }

    public void WaitScript() { Completer(); }

    public void StartTask()
    {
        if (minHoldTime == 0) { MinHoldTimetRND(); }
        Debug.Log("Мин:" + minHoldTime);
        Reqwest = 0;
        if (!corutineWork)
        {
            coroutine = TestCoroutine();
            StartCoroutine(coroutine);
            corutineWork = true;
        }

    }
    private void MinHoldTimetRND() { minHoldTime = rnd.Next(1, 6); }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            Reqwest++;
            TaskTimer.text = "Время:" + Reqwest;
            yield return new WaitForSeconds(1f);
        }
    }
}
