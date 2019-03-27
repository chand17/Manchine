using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SwitchControl : MonoBehaviour
{
    public bool SwitchToggle;
    
    [SerializeField] private GameObject switchObject;
    [SerializeField] private Vector3 onRotation;
    [SerializeField] private Vector3 offRotation; 

    //Events
    public delegate void SwitchAction();
    public event SwitchAction OnSwitch;

    public delegate void SwitchOn();
    public event SwitchOn OnSwitchOn;

    public delegate void SwitchOff();
    public event SwitchOff OnswitchOff;

    void Start()
    {
        if(switchObject == null)
        {
            switchObject = this.gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Grabber grabber = other.GetComponent<Grabber>();
        if(grabber != null) grabber.OnTriggerPull += Switch;
    }
    void OnTriggerExit(Collider other)
    {
        Grabber grabber = other.GetComponent<Grabber>();
        if(grabber != null) grabber.OnTriggerPull -= Switch;
    }

    public void Switch()
    {
        Switch(!SwitchToggle);
    }
    public void Switch(bool switchOn)
    {
        SwitchToggle = switchOn;
        SwitchPosition(switchOn);

        //Switch Event
        if(OnSwitch != null) OnSwitch.Invoke();

        //Switch On and Switch Off events
        if(switchOn){
            if(OnSwitchOn != null) OnSwitchOn.Invoke();
        }
        else{
            if(OnswitchOff != null) OnswitchOff.Invoke();
        }
    }  

    public void SwitchPosition(bool On)
    {
        if(On){
            switchObject.transform.localRotation = Quaternion.Euler(onRotation);
            }
        else{
            switchObject.transform.rotation = Quaternion.Euler(offRotation);
        }

    }
}
