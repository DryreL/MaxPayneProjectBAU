using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine; 

/// <summary>
/// Base class for decorators that are attached to each AI state. Enter and Exit methods will be executed when the AI
/// behavior state is entered and exited. 
/// </summary>
public abstract class AITransitionBehavior : MonoBehaviour
{
    public abstract void TransitionEnter();
    public abstract void TransitionExit();
}
