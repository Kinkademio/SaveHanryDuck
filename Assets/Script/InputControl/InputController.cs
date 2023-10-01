using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputController
{

    private static InputSCRObj defaultInputs = Resources.Load<InputSCRObj>("Input/InputSettings");
    private const string saveInputPrefsPrefix = "Input.";

    //Проверяет присудствует ли значение инпута в сохранениях у пользователя
    private static KeyCode checkPlayerPrefs(InputControlButton key)
    {
       int savedValue =  PlayerPrefs.GetInt(saveInputPrefsPrefix + key.inputName, -1);
       if(savedValue == -1){ //Значение не установлено пользователем
            return key.buttonCode;
       }
       else
       {
            return (KeyCode)savedValue;
       }
    }

    //Проверяем существует ли инпут с таким именем в списке настроек
    private static InputControlButton checkInputName(string inputName)
    {
        InputControlButton found = defaultInputs.getByName(inputName);
        if (found == null)
        { //Если не существует, то возвращаем ошибку
            throw new System.Exception("Ошибка! Пользовательский ввод с псевдонимом '" + inputName + "' не установлен! Выполните проверку: " + defaultInputs);
        }
        return found;

    }

    //Изменяет настройку управления сохраняя её в PlayerPrefs
    public static void changeInput(string inputName, KeyCode newInputKey)
    {
       //Проверяем существует ли инпут с таким именем в списке
       checkInputName(inputName);
       PlayerPrefs.SetInt(saveInputPrefsPrefix + inputName, (int)newInputKey);
    }

    //Получение кода по имени инпута
    public static KeyCode getInput(string inputName)
    {
       //Если существует, то провереям сохранения у пользователя и возвращаем код клавиши
       return checkPlayerPrefs(checkInputName(inputName));
    }





}
