using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserControl : MonoBehaviour
{
    [SerializeField] private Transform dispenseLocation;
    [SerializeField] private float dispenseInterval = 1f;

    private GameObject dispenseObject;
    private bool dispensing;
    private int dispensesLeft;
    private float dTime;

    void Awake()
    {
        dispensing = false;
    }

    void Update()
    {
        if (dispensing)
        {
            if (dTime > dispenseInterval)
            {
                dispense();
                dTime = 0;
            }
            else
            {
                dTime += Time.deltaTime;
            }

            if (dispensesLeft <= 0) dispensing = false;
        }
    }

    private void dispense()
    {
        GameObject.Instantiate(dispenseObject, dispenseLocation.transform.position, dispenseLocation.transform.rotation);
        dispensesLeft--;
    }

    public void DispenseObject(GameObject prefabObject, int count)
    {
        dispenseObject = prefabObject;
        dispensesLeft = count;
        dispensing = true;
        dTime = 0;
    }
}
