using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Behavior that moves in a random direction and periodically stops to look around.
/// </summary>
public class GrazingBehavior : AIBehavior
{
    [SerializeField]
    private RandomVarianceFloat timeMoving;
    [SerializeField]
    private RandomVarianceFloat timeStill;
    [SerializeField]
    private bool lookAround;
    [SerializeField]
    private RandomVarianceFloat distancePerMovement;
    private bool still = false;
    private DateTime startTime;
    private float timeInState;
    private Coroutine lookingCoroutine;    
    
    protected override void TransitionEnter()
    {
        base.TransitionEnter();
        this.Invoke("Transition", .1f);        
    }

    protected override void TransitionExit()
    {
        base.TransitionExit();                
        this.StopLookingAround();
    }

    private void Transition()
    {        
        this.still = !this.still;
        this.InitializeState();
        Invoke("Transition", this.timeInState);
    }

    private void InitializeState()
    {
        if (this.still)
        {
            this.timeInState = this.timeStill.NextValue();
            this.StopLookingAround();
            if (this.lookAround)
                this.lookingCoroutine = StartCoroutine(this.movementController.LookAround(2, this.timeInState));
        }
        else
        {
            this.StopLookingAround();
            this.timeInState = this.timeMoving.NextValue();
            this.movementController.TargetPosition = this.transform.position + this.GetDirection() * distancePerMovement.NextValue();                        
        }

        this.startTime = DateTime.Now;
    }

    private Vector3 GetDirection()
    {            
        return new Vector3(
            UnityEngine.Random.Range(-1f, 1f),
            0,
            UnityEngine.Random.Range(-1f, 1f));             
    }

    private void StopLookingAround()
    {
        if (this.lookingCoroutine != null)
        {
            this.StopCoroutine(this.lookingCoroutine);
            this.lookingCoroutine = null;
            this.movementController.ResetMovement();
        }
    }    
}
