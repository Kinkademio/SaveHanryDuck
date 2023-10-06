using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    bool Working = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (Input.GetKey(InputController.getInput("action")) && Working)
        {
            GameObject.Find("Manager").GetComponent<TaskChecker>().activeTask();
            //GameObject.Find("UI MiniGame").GetComponent<Task>().TaskOption();
            Working = false;
        }
    }
}
