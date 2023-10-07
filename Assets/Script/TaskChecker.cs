using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChecker : MonoBehaviour
{
    public GameObject[] tasks;

    public void activeTask()
    {
        int notDoJob = 0;
        for (int i = 0; i < tasks.Length; i++)
        {
            if (!tasks[i].GetComponent<Task>().taskComplete)
            {
                notDoJob = i;
            }
        }
        tasks[notDoJob].SetActive(true);
        tasks[notDoJob].GetComponent<Task>().taskActive = true;

    }

    public bool checkWin()
    {
        bool isWin = true;
        for(int i = 0; i < tasks.Length; i++)
        {
            if (!tasks[i].GetComponent<Task>().taskComplete)
            {
                isWin=false;
                return isWin;
            }

        }
        return isWin;
    }

    public void iamDONE(GameObject game)
    {
        KeyboardActive(true);

        if (checkWin())
        {
            GameObject.Find("Manager").GetComponent<GameManagerScript>().winGame();
        }
    }

    public void ResetMiniGames()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].GetComponent<Task>().taskComplete = false;
            GameObject.Find("UI MiniGame").GetComponent<Task>().closeTask();
        }

    }

    public void KeyboardActive(bool Active)
    {
        GameObject Player = GameObject.Find("Duck");
        Player.GetComponent<PlayerControl>().SetKeyboardActive(Active);

        GameObject Drone = GameObject.Find("Drone");
        Drone.GetComponent<Weapon>().SetkeyboardActive(Active);
    }
}
