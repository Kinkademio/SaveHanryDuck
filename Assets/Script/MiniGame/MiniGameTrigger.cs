using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Duck")
        {
            if (false)
            {
                Working = true;


            }
            //GameObject.Find("UI MiniGame").GetComponent<Task>().TaskOption();
        }
    }
}
