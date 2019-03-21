using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TrayUI : MonoBehaviour {

    public InventoryTray tray;
    private TextMeshPro text;

	void Awake () {
        if (tray == null) GetComponentInParent<InventoryTray>();
        text = GetComponent<TextMeshPro>();
	}

    void Start() {
        text.text = tray.ItemTemplate.GetComponent<InventoryItem>().ItemName;
    }
}
