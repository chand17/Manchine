using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayKeypadController : MonoBehaviour
{
    //Keys Assinged in Editor
    public List<DisplayKey> DisplayKeys;

    private void Start()
    {
        //if no key was assigned
        if(DisplayKeys == null) DisplayKeys = new List<DisplayKey>();
    }

    public void SetDisplay(string Display)
    {
        DisplayKey key = DisplayKeys.Find(x => x.Message.Equals(Display));

        if(key != null)
        {
            key.ToggleDisplay(true);
        }
        else
        {
            Debug.Log("Can't display " + Display + " no matching key.");
        }
    }
    //Varient to disable all other Displaykeys before Displaying the new one
    public void SetDisplay(string Display, bool singleDisplay)
    {
        if(singleDisplay)
        {
            foreach (DisplayKey key in DisplayKeys)
                key.ToggleDisplay(false);
        }

        SetDisplay(Display);
    }
}
