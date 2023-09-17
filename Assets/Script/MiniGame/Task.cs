using UnityEngine;

public class Task : MonoBehaviour
{
    public GameObject parent;
    
    public void Completer()
    {
        GameObject.Find("Manager").GetComponent<TaskChecker>().iamDONE(gameObject);
        closeTask();
    }

    public void closeTask()
    {
        parent.SetActive(false);
    }

}
