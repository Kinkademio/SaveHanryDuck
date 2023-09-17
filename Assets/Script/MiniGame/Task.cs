using UnityEngine;

public class Task : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject OxygenTask;
    public GameObject ElectricityTask;
    public GameObject SpaceshipTask;
    public GameObject Text;
    
    public bool OxygenTaskComplete = false;
    public bool ElectricityTaskComplete = false;
    public bool SpaceshipTaskComplete = false;

    void Start() { 
        //Canvas.SetActive(false);
    }
    public void Completer()
    {
        Text.SetActive(true);
    }

    public void TaskOption()
    {
        int taskNumber = Random.Range(1, 3);

        switch (taskNumber)
        {
            case 1:
                OxygenTask.SetActive(true);
                break;
            case 2:
                ElectricityTask.SetActive(true);
                break;
            case 3:
                SpaceshipTask.SetActive(true);
                break;
        }
    }
}
