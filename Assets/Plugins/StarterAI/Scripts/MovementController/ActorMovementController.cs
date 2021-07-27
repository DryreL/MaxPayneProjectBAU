using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Base class for controlling the movement of an AI. 
/// </summary>
public abstract class ActorMovementController : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public event Action StartMoving;
    public event Action StopMoving;

    protected Vector3 targetDirection;
    protected Vector3 targetPosition;
    protected bool canMove = true;
    protected bool canTurn = true;
    protected bool isMoving = false;

    protected Vector3 previousPosition;
    protected float speed; // m/s
    protected bool speedVerification = false;
    private MovementState movementState;    

    protected virtual void Awake()
    {        
    }    

    protected virtual void FixedUpdate()
    {
        this.SpeedCheck();
    }

    protected virtual void OnDestroy()
    {
        this.CancelInvoke();        
    }    

    private void SpeedCheck()
    {
        Vector3 curPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        this.speed = Vector3.Distance(this.previousPosition, curPos) / Time.deltaTime;
        this.previousPosition = curPos;

        if (speed > 0)
        {
            this.IsMoving = true;
            if (this.speedVerification)
            {
                this.CancelInvoke("VerifyMovementStopped");
                this.speedVerification = false;
            }
        }
        else if (!this.speedVerification)
        {
            this.Invoke("VerifyMovementStopped", .25f);
            this.speedVerification = true;
        }
    }

    public virtual void ResetMovement()
    {
        this.TargetPosition = this.transform.position;        
        this.CanMove = true;
        this.CanTurn = true; 
    }

    // Invoked after the speed is detected to be 0. If the enemy is still stopped after a delay then change the flag to trigger an animation change.
    private void VerifyMovementStopped()
    {
        if (this.speed == 0)
            this.IsMoving = false;
        else this.IsMoving = true;
        this.speedVerification = false;
    }

    public virtual IEnumerator LookAround(float secondsBetweenLooks, float totalSeconds = 0)
    {
        return null;
    }

    protected void OnStartMoving()
    {
        Action tmp = this.StartMoving;
        if (tmp != null)
            tmp();
    }

    protected void OnStopMoving()
    {
        Action tmp = this.StopMoving;
        if (tmp != null)
            tmp();
    }


    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler h = this.PropertyChanged;
        if (h != null)
            h(this, new PropertyChangedEventArgs(name));
    }

    public Vector3 TargetPosition
    {
        get
        {
            return this.targetPosition;
        }
        set
        {
            this.targetPosition = value;
            OnPropertyChanged("TargetPosition");
        }
    }

    public Vector3 TargetDirection
    {
        get
        {
            return this.targetDirection;
        }
        set
        {
            this.targetDirection = value;
            OnPropertyChanged("TargetDirection");
        }
    }

    public bool IsMoving
    {
        get { return this.isMoving; }
        protected set
        {
            if (this.isMoving != value)
            {
                this.isMoving = value;
                if (!this.isMoving)
                    this.OnStopMoving();
                else
                    this.OnStartMoving();
            }
        }
    }

    public bool CanMove
    {
        get
        {
            return this.canMove;
        }
        set
        {
            this.canMove = value;
            OnPropertyChanged("CanMove");
        }
    }

    public bool CanTurn
    {
        get
        {
            return this.canTurn;
        }
        set
        {
            this.canTurn = value;
            OnPropertyChanged("CanTurn");
        }
    }    

    public MovementState MovementState
    {
        get { return this.movementState; }
        set { if (this.movementState != value) { this.movementState = value; OnPropertyChanged("MovementState"); } }
    }

}
