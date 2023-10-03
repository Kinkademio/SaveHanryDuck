using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OxygenTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public Text OxygenCounter, OxygenTimer;

    private int holdTime = 0, minHoldTime = 0;
    private readonly int maxHoldTime = 10;

    System.Random rnd = new();

    bool corutineWork = false;

    IEnumerator coroutine;
    public void OnPointerDown(PointerEventData eventData) { StartTask(); }
    public void OnPointerUp(PointerEventData eventData) { Stop(); }

    public void Stop()
    {
        StopCoroutine(coroutine);
        corutineWork = false;
        if (holdTime >= minHoldTime && holdTime <= maxHoldTime)
        {
            minHoldTime = 0;
            holdTime = 0;
            OxygenCounter.text = "";
            OxygenTimer.text = "Сила удара:" + holdTime;
            Completer();
        }
        else if (holdTime < minHoldTime) { OxygenCounter.text = "Слишком слабо"; }
        else { OxygenCounter.text = "По аккуратнее!!!"; }

    }

    public void StartTask()
    {
        if (minHoldTime == 0) { minHoldTimetRND(); }
        Debug.Log( "Мин:" + minHoldTime);
        holdTime = 0;
        if (!corutineWork)
        {
            coroutine = TestCoroutine();
            StartCoroutine(coroutine);
            corutineWork = true;
        }

    }
    private void minHoldTimetRND() { minHoldTime = rnd.Next(1, 6); }

    IEnumerator TestCoroutine()
    {
        while (true)
        {
            holdTime++;
            OxygenTimer.text = "Сила удара:" + holdTime;
            yield return new WaitForSeconds(1f);
        }
    }
}