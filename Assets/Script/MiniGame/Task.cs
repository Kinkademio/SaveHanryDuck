using UnityEngine;

public class Task : MonoBehaviour
{
    public GameObject Parent;

    public bool OxygenTaskStatus = false, SpaceShipTaskStatus = false, ElectricityTaskStatus = false;
    public bool OxygenTaskCheck = false, SpaceShipTaskCheck = false, ElectricityTaskCheck = false;

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
