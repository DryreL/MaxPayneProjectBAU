using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Example of a movement controller using root motion animations. 
/// </summary>
public class RootMotionMovementController : ActorMovementController
{
    private Animator anim;

    private float forwardSpeed;
    private float lateralSpeed;

    void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.PropertyChanged += RootMotionMovementController_PropertyChanged;
    }

    protected override void OnDestroy()
    {
        this.PropertyChanged -= this.RootMotionMovementController_PropertyChanged;
        base.OnDestroy();
    }

    public override void ResetMovement()
    {        
        base.ResetMovement();
        this.TargetDirection = Vector3.zero;                 
    }

    private void RootMotionMovementController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "MovementSpeed":             
                break;
            case "TurningSpeed":                
                break;
            case "TargetPosition":
                if (this.targetPosition != Vector3.zero && this.targetPosition != this.transform.position)
                {
                    this.TargetDirection = this.targetPosition - this.transform.position;
                }
                else
                {
                    this.TargetDirection = Vector3.zero;
                }

                break;

            case "TargetDirection":
                this.UpdateTargetDirection();
                break;
            case "CanMove":

                if (!this.CanMove)
                {
                    this.anim.SetFloat("Input X", 0);
                    this.anim.SetFloat("Input Z", 0);
                    this.anim.SetBool("Moving", false);
                }
                else
                {
                    this.UpdateTargetDirection();
                }
                break;
            case "CanTurn":           
                break;

            case "MovementState":
                if (this.MovementState == MovementState.None)
                {
                    this.anim.SetBool("Moving", false);
                    this.anim.SetBool("Running", false);
                    this.ForwardSpeed = 0;
                    this.LateralSpeed = 0;
                }
                else if (this.MovementState == MovementState.Walking)
                {
                    this.anim.SetBool("Moving", true);
                    this.anim.SetBool("Running", false);
                    this.ForwardSpeed = .5f;
                    this.LateralSpeed = 0;
                }
                else if (this.MovementState == MovementState.Running)
                {
                    this.anim.SetBool("Moving", true);
                    this.anim.SetBool("Running", true);
                    this.ForwardSpeed = .9f;
                    this.LateralSpeed = 0;
                }
                else if (this.MovementState == MovementState.Stealth)
                {
                }
                break;
            case "ForwardSpeed":
                this.anim.SetFloat("Input Z", this.forwardSpeed);
                break;

            case "LateralSpeed":
                this.anim.SetFloat("Input X", this.lateralSpeed);
                break;
        }
    }

    private void UpdateTargetDirection()
    {
        if (this.targetDirection != Vector3.zero)
        {
            this.targetDirection.y = 0;
            this.targetDirection.Normalize();
            this.transform.forward = this.targetDirection;         
            
            this.anim.SetFloat("Input X", this.lateralSpeed);
            this.anim.SetFloat("Input Z", this.forwardSpeed);
            this.anim.SetBool("Moving", true);
        }
        else
        {            
            this.anim.SetFloat("Input X", 0);
            this.anim.SetFloat("Input Z", 0);
            this.anim.SetBool("Moving", false);
        }
    }
    
    public float ForwardSpeed
    {
        get { return this.forwardSpeed; }
        set { if (this.forwardSpeed != value) { this.forwardSpeed = value; OnPropertyChanged("ForwardSpeed"); } }
    }
    
    public float LateralSpeed
    {
        get { return this.lateralSpeed; }
        set { if (this.lateralSpeed != value) { this.lateralSpeed = value; OnPropertyChanged("LateralSpeed"); } }
    }

}