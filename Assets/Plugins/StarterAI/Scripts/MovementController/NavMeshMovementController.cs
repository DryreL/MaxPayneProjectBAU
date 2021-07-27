using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;

/// <summary>
/// Movement controller utilizing a nav mesh agent. 
/// </summary>
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NavMeshMovementController : ActorMovementController
{    
    public event Action StartLookInPlace; 
    public event Action StopLookInPlace;
    
    private UnityEngine.AI.NavMeshAgent navAgent;    
    private Vector3 curMove = Vector3.zero;    
    private float movementSpeed = 10;
    private float turningSpeed = 2;    
           
    protected override void Awake()
    {        
        this.navAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();               
        this.PropertyChanged += EnemyMovementController_PropertyChanged;                            
    }
    
    protected override void OnDestroy()
    {
        this.PropertyChanged -= EnemyMovementController_PropertyChanged;                
        base.OnDestroy();
    }  

    void EnemyMovementController_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "MovementSpeed":
                if (this.navAgent != null && this.navAgent.enabled)
                    this.navAgent.speed = this.movementSpeed;
                break;
            case "TurningSpeed":
                if (this.navAgent != null && this.navAgent.enabled)
                    this.navAgent.angularSpeed = this.turningSpeed * Mathf.Rad2Deg;
                break;
            case "TargetPosition":
                if (this.navAgent != null && this.navAgent.enabled && this.gameObject.activeInHierarchy)
                {
                    if (this.navAgent.destination != this.targetPosition)
                    {
                        this.navAgent.SetDestination(this.targetPosition);                        
                    }                                  
                }
                break;
            case "CanMove":
                if(this.navAgent != null && this.navAgent.enabled)
                {                 
                    this.navAgent.updatePosition = this.CanMove;                          
                }
                break;
            case "CanTurn":
                if(this.navAgent != null && this.navAgent.enabled)
                    this.navAgent.updateRotation = this.CanTurn; 
                break;        
        }
    }

    private void LookInPlace(Vector3 direction)
    {
        if(this.navAgent != null && this.navAgent.enabled)
        {
            this.CanMove = false; 
            this.CanTurn = true;            
            this.navAgent.SetDestination(direction);                     
        }
    }

    public override IEnumerator LookAround(float secondsBetweenLooks, float totalSeconds = 0)
    {        
        this.OnStartLookInPlace();                 
        DateTime start = DateTime.Now;
        int ms = (int)totalSeconds * 1000; 
        do
        {
            Vector3 curLeft = -this.transform.right;
            Vector3 curRight = this.transform.right;

            bool canRight = !Physics.Raycast(this.transform.position, curRight, 5);
            bool canLeft = !Physics.Raycast(this.transform.position, curLeft, 5);

            if (canRight)
            {                
                this.LookInPlace(this.transform.position + curRight);
                yield return new WaitForSeconds(secondsBetweenLooks);
            }
            if (canLeft)
            {                
                this.LookInPlace(this.transform.position + curLeft);
                yield return new WaitForSeconds(secondsBetweenLooks);
            }
            else yield return new WaitForSeconds(secondsBetweenLooks); 

        } while ((DateTime.Now - start).TotalMilliseconds < ms); 

        this.OnStopLookInPlace();
        this.ResetMovement();        
    }


    // re-align the nav agent and restore movement
    public override void ResetMovement()
    {
        if(this.navAgent == null)
            this.navAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>(); 
        this.navAgent.Warp(this.transform.position);

        base.ResetMovement();      
    }            

    protected void OnStartLookInPlace()
    {
        Action act = this.StartLookInPlace; 
        if(act != null)
            act(); 
    }

    protected void OnStopLookInPlace()
    {
        Action act = this.StopLookInPlace; 
        if(act != null)
            act();
    }
    
    public float MovementSpeed { get { return this.movementSpeed; } set { this.movementSpeed = value; OnPropertyChanged("MovementSpeed"); } }
    public float TurningSpeed { get { return this.turningSpeed; } set { this.turningSpeed = value; OnPropertyChanged("TurningSpeed"); } }        
}
