using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    bool Working = true;

    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
       if(Input.GetKeyDown(KeyCode.E) && Working)
       {
            GameObject.Find("UI MiniGame").GetComponent<Task>().TaskOption();
            Working = false;
       }
    }
}
