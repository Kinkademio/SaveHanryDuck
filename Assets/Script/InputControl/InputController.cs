using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputController
{

    private static InputSCRObj defaultInputs = Resources.Load<InputSCRObj>("Input/InputSettings");
    private const string saveInputPrefsPrefix = "Input.";

    //��������� ������������ �� �������� ������ � ����������� � ������������
    private static KeyCode checkPlayerPrefs(InputControlButton key)
    {
       int savedValue =  PlayerPrefs.GetInt(saveInputPrefsPrefix + key.inputName, -1);
       if(savedValue == -1){ //�������� �� ����������� �������������
            return key.buttonCode;
       }
       else
       {
            return (KeyCode)savedValue;
       }
    }

    //��������� ���������� �� ����� � ����� ������ � ������ ��������
    private static InputControlButton checkInputName(string inputName)
    {
        InputControlButton found = defaultInputs.getByName(inputName);
        if (found == null)
        { //���� �� ����������, �� ���������� ������
            throw new System.Exception("������! ���������������� ���� � ����������� '" + inputName + "' �� ����������! ��������� ��������: " + defaultInputs);
        }
        return found;

    }

    //�������� ��������� ���������� �������� � � PlayerPrefs
    public static void changeInput(string inputName, KeyCode newInputKey)
    {
       //��������� ���������� �� ����� � ����� ������ � ������
       checkInputName(inputName);
       PlayerPrefs.SetInt(saveInputPrefsPrefix + inputName, (int)newInputKey);
    }

    //��������� ���� �� ����� ������
    public static KeyCode getInput(string inputName)
    {
       //���� ����������, �� ��������� ���������� � ������������ � ���������� ��� �������
       return checkPlayerPrefs(checkInputName(inputName));
    }





}
