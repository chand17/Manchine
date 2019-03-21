using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour {

    //Assigned in Editor
    public List<InventoryTray> Trays;

	void Awake() {
        if (Trays == null) Trays = new List<InventoryTray>();
	}
}
