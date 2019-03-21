using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public OVRInput.Controller controller;

    private GrabbableItem grabbableItem;
    public GrabbableItem grabbedItem;

    [SerializeField] private float grabBegin = 0.55f;
    [SerializeField] private float grabEnd = 0.35f;
    private float prevTigger;

    //Events
    public delegate void GrabbedItem(GrabbableItem item);
    public event GrabbedItem OnGrabbedItem;

    public delegate void DroppedItem(GrabbableItem item);
    public event DroppedItem OnDroppedItem;

    public delegate void TriggerPull();
    public event TriggerPull OnTriggerPull;

    void Start()
    {
        OnTriggerPull += triggerPull;  
    }

    void Update()
    {
        float triggerInput = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        //Check for TriggerPull
        if(prevTigger < grabBegin && triggerInput > grabBegin)
        {
            if (OnTriggerPull != null) OnTriggerPull.Invoke();
        }

        if (grabbedItem != null)
        {
            //if trigger is released call DropItem
            if (triggerInput < grabEnd) DropItem();
        }

        prevTigger = triggerInput;
    }

    void OnTriggerEnter(Collider other)
    {
        //Check to see if 'other' is a GrabbableItem
        GrabbableItem otherItem = other.GetComponent<GrabbableItem>();
        if(otherItem != null)
        {
            grabbableItem = otherItem;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (grabbableItem != null) grabbableItem = null;
    }

    public void GrabItem(GrabbableItem item)
    {
        if (item != null)
        {
            grabbedItem = item;
            grabbedItem.transform.position = this.transform.position;
            grabbedItem.transform.parent = this.transform;

            if (OnGrabbedItem != null) OnGrabbedItem.Invoke(item);
            grabbedItem.Grab();
        }
    }
    private void triggerPull()
    {
        if (grabbableItem != null)
        {
            GrabItem(grabbableItem);
        }
    }

    public void DropItem()
    {
        grabbedItem.transform.parent = null;

        if (OnDroppedItem != null) OnDroppedItem.Invoke(grabbedItem);
        grabbedItem.Drop();
        grabbedItem = null;
    }
}
