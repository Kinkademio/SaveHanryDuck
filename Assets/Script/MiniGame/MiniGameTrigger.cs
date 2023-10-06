using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    bool Working = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(InputController.getInput("action")) && Working && (collision.name == "Duck"))
        {
            Working = false;
            GameObject.Find("Manager").GetComponent<TaskChecker>().activeTask();


            collision.GetComponent<PlayerControl>().SetKeyboardActive(false);
            GameObject Drone = GameObject.Find("Drone");
            Drone.GetComponent<Weapon>().SetkeyboardActive(false);

            //GameObject.Find("UI MiniGame").GetComponent<Task>().TaskOption();
        }
    }
}
