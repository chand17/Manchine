using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayKey : MonoBehaviour
{
    //Set in editor
    public string Message;

    //Events
    public delegate void Display();
    public event Display OnDisplay;
    public event Display OffDisplay;

    internal void ToggleDisplay(bool toggle)
    {
        if (toggle){
            if (OnDisplay != null) OnDisplay.Invoke();
        }
        else{
            if (OffDisplay != null) OffDisplay.Invoke();
        }
    }
}
