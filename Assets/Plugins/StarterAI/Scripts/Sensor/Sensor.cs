using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for sensors that are used to notify AI behaviors and behavior managers of information perceived by the actor. 
/// </summary>
public abstract class Sensor: MonoBehaviour
{
    // invoked when isTriggered switches from false to true.
    public event EventHandler Triggered;
    private bool isTriggered;
    [SerializeField]
    private int priority = 1;

    public Sensor()
    {        
    }

    protected void OnTriggered()
    {
        EventHandler temp = this.Triggered;
        if (temp != null)
            temp(this, null);
    }

    public int Priority { get { return this.priority; } }

    public bool IsTriggered
    {
        get { return this.isTriggered; }
        set
        {
            if (this.isTriggered != value)
            {
                this.isTriggered = value; 
                if(this.isTriggered)
                    this.OnTriggered();
            }
        }
    }    
}
