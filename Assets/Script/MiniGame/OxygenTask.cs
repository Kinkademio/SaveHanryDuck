using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class OxygenTask : Task, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Slash;
    public readonly int minHoldTime = 5;
    public readonly int maxHoldTime = 15;
    public int holdTime = 0;

    bool corutineWork = false;
    IEnumerator coroutine;
    public void OnPointerDown(PointerEventData eventData)
    {
        StartTask();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Stop();
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
        corutineWork = false;
        if (holdTime >= minHoldTime && holdTime <= maxHoldTime)
        {
            holdTime = 0;  
            Completer();
        }
    }

    public void StartTask()
    {
        holdTime = 0;
        if (!corutineWork)
        {
            coroutine = TestCoroutine();
            StartCoroutine(coroutine);
            corutineWork = true;
        }

    }
    IEnumerator TestCoroutine()
    {
        while (true)
        {
            holdTime++;
            yield return new WaitForSeconds(1f);
            Debug.Log(holdTime);
        }
    }
}