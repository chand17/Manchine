using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrayQueue : MonoBehaviour
{
    [SerializeField]
    private Vector3 itemOffset;

    public List<GameObject> QueuedItems;

    private void Awake()
    {
        QueuedItems = new List<GameObject>();
    }

    public void AddToQueue(GameObject newItem)
    {
        newItem.transform.parent = this.transform;
        newItem.transform.position = this.transform.position;
        newItem.transform.localPosition += itemOffset * QueuedItems.Count;

        QueuedItems.Add(newItem);
    }

    private void resetPositions()
    {
        for (int i = 0; i < QueuedItems.Count; i++)
        {
            QueuedItems[i].transform.position = transform.position;
            QueuedItems[i].transform.localPosition += itemOffset * i;
        }
    }

    public void GrabFromQueue(GameObject grabbedItem)
    {
        QueuedItems.Remove(grabbedItem);
        resetPositions();
    }
}
