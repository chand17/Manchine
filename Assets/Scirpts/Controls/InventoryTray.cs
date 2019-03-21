using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTray : MonoBehaviour {

    public InventoryTrayQueue InventoryQueue;
    public GameObject ItemTemplate;

    private Queue<InventoryItem> inventory;
    public Grabber hoveredGrabber;
    
    //Events
    public delegate void ItemGrabbed(InventoryItem item);
    public event ItemGrabbed OnItemGrabbed; 

	void Awake () {
        inventory = new Queue<InventoryItem>();
	}

    void Start() {
        fillTray();
    }

    void OnTriggerEnter(Collider other)
    {
        Grabber grabber = other.GetComponent<Grabber>();
        if(grabber != null)
        {
            hoveredGrabber = grabber;
            hoveredGrabber.OnTriggerPull += TriggerPulledOverTray;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Grabber grabber = other.GetComponent<Grabber>();
        if (grabber != null)
        {
            grabber.OnTriggerPull -= TriggerPulledOverTray;
            hoveredGrabber = null;
        }
    }

    public void TriggerPulledOverTray()
    {
        InventoryItem item = GrabFromQueue();
        hoveredGrabber.GrabItem(item.gameObject.AddComponent<GrabbableItem>());
    }
    
    private void fillTray()
    {
        if (ItemTemplate != null)
        {
            for (int i = 0; i < 20; i++)
            {
                InventoryItem newItem = GameObject.Instantiate(ItemTemplate).GetComponent<InventoryItem>();
                newItem.gameObject.SetActive(true);
                AddInventoryItem(newItem);
            }
        }
    }

    public void AddInventoryItem(InventoryItem item)
    {
        inventory.Enqueue(item);
        InventoryQueue.AddToQueue(item.gameObject);
    }

    public InventoryItem GrabFromQueue()
    {
        if (inventory.Count > 0)
        {
            InventoryItem item = inventory.Dequeue();
            InventoryQueue.GrabFromQueue(item.gameObject);

            if (OnItemGrabbed != null) OnItemGrabbed.Invoke(item);

            return item;
        }

        return null;
    }
}
