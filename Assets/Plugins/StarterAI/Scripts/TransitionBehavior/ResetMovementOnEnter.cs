using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Transition behavior that resets the movement controller on enter.
/// </summary>
public class ResetMovementOnEnter : AITransitionBehavior
{
    private ActorMovementController movementController;

    void Awake()
    {
        this.movementController = this.GetComponent<ActorMovementController>();
    }

    public override void TransitionEnter()
    {
        if (this.movementController != null)
            this.movementController.ResetMovement();
    }

    public override void TransitionExit()
    {        
    }
}