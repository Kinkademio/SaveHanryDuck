using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public GameObject SpaceShip, Star1, Star2, Star3, Parent, Spark, TaskComleted;
    public Text TaskCounter, TaskTimer;

    public System.Random rnd = new();

    public int Reqwest = 0, maxHoldTime = 10, minHoldTime = 0, qwest;

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

    public void OffGameObject() 
    {
        Star1.SetActive(true);
        Star2.SetActive(true);
        Star3.SetActive(true);
        SpaceShip.transform.position = point;
    }

    public void closeTask()
    {
        Parent.SetActive(false);
        if (Spark.activeInHierarchy) Spark.SetActive(false);
        if (!Star1.activeInHierarchy || !Star2.activeInHierarchy || !Star3.activeInHierarchy) OffGameObject();
        if (corutineWork) StopCoroutine();
        taskActive = false;
        minHoldTime = 0;
        Reqwest = 0;
        TaskCounter.text = "";
        TaskTimer.text = "" + Reqwest;
        TaskComleted.SetActive(false);
    }

}
