using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public GameObject Parent, TaskComleted;
    public Text TaskCounter;

    public System.Random rnd = new();

    public int Reqwest = 0, maxHoldTime = 10, minHoldTime = 0;

    public bool taskActive = false, taskComplete = false, corutineWork = false;

    public IEnumerator coroutine;

    public Vector2 point;

    public void Completer()
    {
        GameObject.Find("Manager").GetComponent<TaskChecker>().iamDONE(gameObject);
        closeTask();
    }
    public void StopCoroutine()
    {
        StopCoroutine(coroutine);
        corutineWork = false;
    }

    public void closeTask()
    {
        if(corutineWork) StopCoroutine();
        taskActive = false;
        minHoldTime = 0;
        Reqwest = 0;
        TaskCounter.text = "";
        TaskComleted.SetActive(false);
        Parent.SetActive(false);
    }

}
