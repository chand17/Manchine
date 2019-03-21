using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableItem : MonoBehaviour
{
    //Events
    public delegate void GrabAction();
    public event GrabAction OnGrab;

    public delegate void DropAction(GrabbableItem item);
    public event DropAction OnDrop;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Grab()
    {
        if (OnGrab != null) OnGrab.Invoke();

        rb.isKinematic = true;
        rb.useGravity = false;
    }
    public void Drop()
    {
        if (OnDrop != null) OnDrop.Invoke(this);

        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
