using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropboxControl : MonoBehaviour
{
    //Events
    public delegate void ItemDrop(GrabbableItem item);
    public event ItemDrop OnItemDrop;
        
    //Triggers
    void OnTriggerEnter(Collider other)
    {
        GrabbableItem item = other.GetComponent<GrabbableItem>();
        if (item != null)
        {
            item.OnDrop += ItemDropped;
        }
    }
    void OnTriggerExit(Collider other)
    {
        GrabbableItem item = other.GetComponent<GrabbableItem>();
        if (item != null)
        {
            item.OnDrop -= ItemDropped;
        }
    }

    public void ItemDropped(GrabbableItem item)
    {
        if (OnItemDrop != null) OnItemDrop.Invoke(item);

        GameObject.Destroy(item.gameObject);
    }
}
