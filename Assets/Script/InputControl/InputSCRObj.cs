using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDefaultControls", menuName = "ScriptableObjects/GameDefaultControls", order = 1)]
public class InputSCRObj : ScriptableObject
{
   public InputControlButton[] inputButtons;

    public InputControlButton getByName(string name)
    {
        return this.inputButtons.FirstOrDefault(obj => obj.inputName == name);
    }

}
