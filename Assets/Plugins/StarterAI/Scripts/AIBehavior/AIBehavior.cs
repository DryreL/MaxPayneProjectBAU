using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Base abstract class for AIBehavior components. 
/// </summary>
public abstract class AIBehavior : MonoBehaviour
{
    [SerializeField]
    protected float runSpeed = 5;
    [SerializeField]
    protected float walkSpeed = 3;
    [SerializeField]
    protected float turningSpeed = 5;
    [SerializeField]
    private AITransitionBehavior[] transitionBehaviors;
            
    protected bool usesSensorData = false;
    protected ActorMovementController movementController;
  
    private void OnEnable()
    {
        this.TransitionEnter();
    }

    private void OnDisable()
    {
        this.TransitionExit();
    }   

    protected void SetupMovementController()
    {
        if (this.movementController == null)
            this.movementController = this.GetComponent<ActorMovementController>();
        if (this.movementController != null)
        {
            this.movementController.ResetMovement();
            NavMeshMovementController navController = this.movementController as NavMeshMovementController;
            if (navController != null)
            {
                navController.MovementSpeed = this.runSpeed;
                navController.TurningSpeed = this.turningSpeed;
            }
        }
    }

    protected virtual void TransitionEnter()
    {
        this.SetupMovementController();
        this.ExecuteTransitionEnterBehaviors();
    }

    protected virtual void TransitionExit()
    {
        this.CancelInvoke();
        this.StopAllCoroutines();        
        this.ExecuteTranistionExitBehaviors();
    }

    protected void ExecuteTranistionExitBehaviors()
    {
        if (this.transitionBehaviors != null)
            foreach (AITransitionBehavior b in this.transitionBehaviors)
                b.TransitionExit();
    }

    protected void ExecuteTransitionEnterBehaviors()
    {        
        if (this.transitionBehaviors != null)
            foreach (AITransitionBehavior b in this.transitionBehaviors)
                b.TransitionEnter();
    }    
}
