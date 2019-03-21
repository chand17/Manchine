using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class KeyUI : MonoBehaviour {

    private DisplayKey key;
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        text.color = Color.grey;

        key = transform.parent.GetComponent<DisplayKey>();
        if (key != null)
        {
            key.OnDisplay += Display;
            key.OffDisplay += TurnOffDisplay;
        }

        text.text = key.Message;
    }

    public void Display()
    {
        text.color = Color.red;
    }
    public void TurnOffDisplay()
    {
        text.color = Color.gray;
    }
}
