using UnityEngine;

public class Task : MonoBehaviour
{
    public GameObject Parent;

    public bool taskActive = false;
    public bool taskComplete = false;

    public void Completer()
    {
        GameObject.Find("Manager").GetComponent<TaskChecker>().iamDONE(gameObject);
        closeTask();
    }

    public void closeTask()
    {
        Parent.SetActive(false);
    }

}
