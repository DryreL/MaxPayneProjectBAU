using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Sensor that notifies when the player is within range of a trigger collider on the actor. 
/// </summary>
public class ProximitySensor : Sensor
{
    [SerializeField]
    private string targetTag = "Player"; 

    public void OnTriggerEnter(Collider col)
    {        
        if (!this.IsTriggered)
        {
            if (col.gameObject.tag == this.targetTag)
            {                
                this.CancelInvoke("ResetTrigger");                
                this.IsTriggered = true;
            }
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (this.IsTriggered)
        {
            this.Invoke("ResetTrigger", 3);
        }
    }

    private void ResetTrigger()
    {
        this.IsTriggered = false;
    }            
}
