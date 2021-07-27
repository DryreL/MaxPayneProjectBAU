using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// AIBehavior representing an enemy that moves towards the player and attacks when within the desired attack distance.
/// </summary>
[RequireComponent(typeof(AttackController))]
public class BattleBehavior : AIBehavior
{          
    [SerializeField]
    private float desiredAttackDistance = 4;
    private AttackController attackController; 

    void Awake()
    {
        this.attackController = this.GetComponent<AttackController>();
    }
    
    protected override void TransitionEnter()
    {                
        base.TransitionEnter();  
                      
        if(this.movementController != null)
        {
            this.movementController.MovementState = MovementState.Walking;
            this.InvokeRepeating("CheckPosition", 0, .15f);
        }                    
    }  

    public void CheckPosition()
    {        
        // move towards the player if the attack distance isn't reached
        float distance = Vector3.Distance(this.transform.position, Player.Instance.transform.position);        
        if (distance > this.desiredAttackDistance)
        {            
            this.movementController.TargetPosition = Player.Instance.transform.position;            
        }
        else
        {
            this.movementController.TargetPosition = this.transform.position;
        } 
    }

    protected void Update()
    {
        // make sure enemy is always facing the player
        Vector3 fwd = Player.Instance.transform.position - this.transform.position;
        this.transform.forward = new Vector3(fwd.x, 0, fwd.z);

        this.attackController.TryAttack();
    }   
}
