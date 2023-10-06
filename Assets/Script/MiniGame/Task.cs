using UnityEngine;

public class Task : MonoBehaviour
{
    public GameObject parent;

    public bool SpaceShipTaskCheck = false, OxygenTaskCheck = false, ElectricityTaskCheck = false;

    public bool SpaceShipTaskStatus = false, OxygenTaskStatus = false, ElectricityTaskStatus = false;

    public void Completer()
    {
        GameObject.Find("Manager").GetComponent<TaskChecker>().iamDONE(gameObject);
        closeTask();
    }

    public void closeTask() { parent.SetActive(false); }

}
