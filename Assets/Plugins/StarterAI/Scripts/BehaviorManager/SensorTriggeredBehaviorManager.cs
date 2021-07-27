using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Switches between two AIBehavior implementations when attached sensors are triggered or inactive for a period of time.
/// </summary>
public class SensorTriggeredBehaviorManager : MonoBehaviour
{
    private Sensor[] sensors;
    private bool inTriggeredState;

    [SerializeField]
    private AIBehavior normalBehavior;
    [SerializeField]
    private AIBehavior triggeredBehavior;    

    // once in the triggered AI state, can the actor revert back to normal.
    [SerializeField]
    private bool canRevertFromTriggered = true; 

    [SerializeField]
    private float minSecondsInNormalState = 3;
    [SerializeField]
    private float minSecondsInTriggeredState = 3;

    [SerializeField]
    private float checkTriggersActivatedInterval = .5f;

    [SerializeField]
    private float checkTriggersInactiveInterval = 2;    

    void Start()
    {
        this.FindAllSensors();

        if (this.triggeredBehavior == null)
            Debug.LogError("triggered behavior not attached");        

        this.BeginNormalState();
    }    

    void OnDestroy()
    {
        this.CancelInvoke();                
    }


    private void FindAllSensors()
    {
        this.sensors = this.GetComponentsInChildren<Sensor>();
    }

    // InvokeRepeat
    private void CheckSensorsTriggered()
    {
        if (this.sensors.Any(x => x.IsTriggered))
        {
            this.BeginTriggeredState();
            return;
        }
    }

    // InvokeRepeat
    private void CheckSensorsNormal()
    {
        if (this.sensors.All(x => !x.IsTriggered))
        {
            this.BeginNormalState();
        }
    }

    private void BeginTriggeredState()
    {        
        this.inTriggeredState = true;
        this.CancelInvoke("CheckSensorsTriggered");
        if (this.normalBehavior != null)
            this.normalBehavior.enabled = false;
        this.triggeredBehavior.enabled = true;

        if (this.canRevertFromTriggered)
        {            
            this.InvokeRepeating("CheckSensorsNormal", this.minSecondsInTriggeredState, this.checkTriggersInactiveInterval);
        }

    }

    private void BeginNormalState()
    {        
        this.inTriggeredState = false;
        this.CancelInvoke("CheckSensorsNormal");
        this.triggeredBehavior.enabled = false;
        if (this.normalBehavior != null)
            this.normalBehavior.enabled = true;
        this.InvokeRepeating("CheckSensorsTriggered", this.minSecondsInNormalState, this.checkTriggersActivatedInterval);
    }

    public bool InTriggeredState { get { return this.inTriggeredState; } }
}
