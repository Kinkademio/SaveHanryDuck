using UnityEngine;

public class TaskChecker : MonoBehaviour
{
    public GameObject[] tasks;
    public bool[] ready;

    private void Start()
    {
        ready = new bool[tasks.Length];

        for(int i=0; i<tasks.Length; i++)
        {
            ready[i] = false;
        }
    }

    public void activeTask()
    {
        int notDoJob = 0;
        for (int i=0; i < ready.Length; i++)
        {
            if (!ready[i])
            {
                notDoJob = i;
            }
        }
        tasks[notDoJob].SetActive(true);

    }

    public bool checkWin()
    {
        bool isWin = true;
        for(int i = 0; i<ready.Length; i++)
        {
            if (!ready[i])
            {
                isWin=false;
                return isWin;
            }

        }
        return isWin;
    }
    public void iamDONE(GameObject game)
    {
        for(int i=0; i< tasks.Length; i++)
        {
            if (tasks[i] == game)
            {
                ready[i] = true;
            }
        }

        if (checkWin())
        {
            for(int i =0; i < ready.Length; i++)
            {
                ready[i] = false;   
            }
            GameObject.Find("Manager").GetComponent<GameManagerScript>().winGame();
        }
    }
}
