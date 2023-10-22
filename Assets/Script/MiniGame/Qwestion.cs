using RoomInteriorGeneratorTag;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEditor.Progress;

public class Qwestion : Task
{
    public UnityEngine.UI.Text qwest;
    public GameObject buttonsBlock;
    public GameObject button;
    public GameObject horizontalBlock;
    public GameManagerScript manager;

    private Tasks currentTask;

    void OnEnable()
    {
        currentTask = manager.test.tasks[0];
        setQwestion(currentTask.text);

        GameObject lastHorizontalBlock = null;
        int childCounter = buttonsBlock.transform.childCount;
        for (int i = 0; i < childCounter; i++)
        {
            Destroy(buttonsBlock.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < currentTask.ansvers.Length; i++)
        {
            if (i % 2 == 0)
            {
                lastHorizontalBlock = Instantiate(horizontalBlock, buttonsBlock.transform);
            }

            spawnButton(currentTask.ansvers[i].text, lastHorizontalBlock, currentTask.ansvers[i].right);
        }
    }
    public void Stop()
    {
        taskComplete = true;
        Invoke("WaitScript", 0.5f);
    }
    public void WaitScript() { Completer(); }
    void CheckAnswer(bool ansver)
    {
        if (ansver)
        {
            Tasks[] array = null;
            if (manager.test.tasks.Length <= 1){
                array = new Tasks[0];
            }
            else
            {
                array = new Tasks[manager.test.tasks.Length - 1];
                for (int i = 1; i < manager.test.tasks.Length; i++)
                {
                    array[i - 1] = manager.test.tasks[i];
                }

            }
            manager.test.tasks = array;

            Stop();
        }
        else
        {
            //Что-то про не верный ответ
        }
    }

    void setQwestion(string text)
    {
        qwest.text = text;
    }

    void spawnButton(string text, GameObject Parent, bool ansver)
    {
        GameObject Button = Instantiate(button, Parent.transform);
        Button.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        Button.GetComponent<Button>().onClick.AddListener(delegate { CheckAnswer(ansver); }); ;
    }

    void Despawn()
    {

    }
}

