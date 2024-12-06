using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BinControl : MonoBehaviour
{
    public UnityEvent onRecycle = new UnityEvent();


     public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered with: {other.gameObject.name}");
        if(other.gameObject.GetComponent<Rigidbody>()) {

            onRecycle.Invoke();
        } 
        else {
            Debug.Log("Object has no Rigidbody component");
        }
    }
}