using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Patrol behavior that pursues the enemy when a sensor is triggered. 
/// </summary>
public class EnemyPatrolBehavior : PatrolBehavior
{
    private Sensor[] sensors;
    private bool inPursuit;

    void Start()
    {
        this.FindAllSensors();        
    }

    protected override void TransitionEnter()
    {
        this.SetupMovementController();
        this.ExecuteTransitionEnterBehaviors();        

        if(this.inPursuit)
        {
            this.inPursuit = false;    
            this.Invoke("BeginPursuit", 1f);
        }
        else
            this.Invoke("BeginPatrol", 1f);
    }    

    protected override void TransitionExit()
    {
        base.TransitionExit();
        this.movementController.TargetPosition = this.transform.position;                       
    }

    protected void Update()
    {
        if(this.inPursuit)
        {
            this.movementController.TargetPosition = Player.Instance.transform.position;                            
        }                
    }
    
    private void BeginPursuit()
    {
        this.inPursuit = true;         
        this.movementController.TargetPosition = Player.Instance.transform.position; 
        this.movementController.MovementState = MovementState.Running; 
        this.InvokeRepeating("CheckPursuitTimeout", 8, 2);
        this.CancelInvoke("CheckSensors");
    }
    
    private void BeginPatrol()
    {
        this.inPursuit = false;        
        this.movementController.MovementState = MovementState.Walking; 
        this.SetTargetToClosestNode();
        this.InvokeRepeating("CheckSensors", 3, .5f);
        this.CancelInvoke("CheckPursuitTimeout");
    }

    private void FindAllSensors()
    {
        this.sensors = this.GetComponentsInChildren<Sensor>();
    }

    // InvokeRepeat
    private void CheckSensors()
    {
        if (this.sensors.Any(x => x.IsTriggered))
        {
            this.BeginPursuit();
            return;
        }
        this.inPursuit = false;
    }
    
    // InvokeRepeat
    private void CheckPursuitTimeout()
    {
        if (this.sensors.All(x => !x.IsTriggered))
        {
            this.BeginPatrol();
        }
    }

    protected override void OnTriggerEnter(Collider col)
    {
        if (!this.inPursuit)
            base.OnTriggerEnter(col);
    }
}
