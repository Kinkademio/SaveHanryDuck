using RoomInteriorGeneratorTag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Qwestion : MonoBehaviour
{
    public GameObject qwest;
    public GameObject buttonsBlock;
    public GameObject button;
    public GameObject horizontalBlock;

    void Awake()
    {
        // TODO: JSON

        GameObject lastHorizontalBlock;
        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                lastHorizontalBlock = Instantiate(horizontalBlock, buttonsBlock.transform);
            }

            //spawnButton(text, lastHorizontalBlock, WrongAnswer);
        }
    }

    void CheckAnswer(bool WrongAnswer)
    {
        if (WrongAnswer)
        {

        }
        else
        {

        }
    }

    void setQwestion(string text)
    {
        qwest.GetComponent<Text>().text = text;
    }

    void spawnButton(string text, GameObject Parent, bool WrongAnswer)
    {
        GameObject Button = Instantiate(button, Parent.transform);
        Button.GetComponentInChildren<Text>().text = text;
        Button.GetComponent<Button>().onClick.AddListener(delegate { CheckAnswer(WrongAnswer); }); ;
    }

    void Despawn()
    {

    }
}

