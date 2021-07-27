using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Behavior that walks between a series of waypoints. works in conjunction with a PatrolRoot and series of colliders
/// representing waypoints. 
/// </summary>
public class PatrolBehavior : AIBehavior
{
    private Transform rootTransform;
    private Transform curNode;
    private Transform nextNode;    

    protected override void TransitionEnter()
    {
        base.TransitionEnter();
        this.Invoke("SetTargetToClosestNode", .1f);        
    }

    public void NewRoot(Transform root, Transform startingNode = null)
    {
        this.rootTransform = root;
        this.curNode = startingNode;
        this.nextNode = null;
        if (this.curNode == null)
            this.SetTargetToClosestNode();
    }

    protected void SetTargetToClosestNode()
    {
        if (this.rootTransform == null)
            this.rootTransform = PatrolRoot.Instance.transform.FindNearestChild(this.transform.position);

        if (this.rootTransform == null)
            Debug.LogError("No root transform could be found for patrol: " + name);

        this.curNode = this.rootTransform.FindNearestChild(this.transform.position);
        if (this.curNode != null)
        {
            // if the enemy is already within the node, then move to the next node instead. 
            float radius = 3f;
            // this assumes that both the patrol point and current enemy are using SphereColliders. just be aware that if that's not the case
            // this might fail...
            float dist = Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z), 
                new Vector3(this.curNode.transform.position.x, 0, this.curNode.transform.position.z));
            SphereCollider sphereCol = this.curNode.GetComponent<SphereCollider>();
            SphereCollider mySphereCol = this.GetComponent<SphereCollider>();
            if(sphereCol != null && mySphereCol != null)            
                radius = sphereCol.radius + mySphereCol.radius;                
                                    
            if (dist <= radius)
            {
                this.DetermineNextTarget(this.curNode);
                this.SetNextTarget();
            }
            else
            {                
                this.movementController.TargetPosition = this.curNode.position; 
                this.movementController.MovementState = MovementState.Walking;              
            }
        }     
    }

    protected virtual void OnTriggerEnter(Collider col)
    {                
        if (curNode == col.transform)
        {
            if (!col.transform.IsChildOf(rootTransform))
                return;
            
            this.DetermineNextTarget(col.transform);
            this.SetNextTarget();            
        }
    }

    private void DetermineNextTarget(Transform curTarget)
    {
        int index = this.rootTransform.IndexOfChild(curTarget);

        if (index == -1)
            return;
                
        if (index >= this.rootTransform.childCount - 1)
            index = 0;
        else
            index++;
        this.nextNode = this.rootTransform.GetChild(index);
        
        this.curNode = null;        
    }

    private IEnumerator LookAround()
    {
        yield return StartCoroutine(this.movementController.LookAround(2));
        this.SetNextTarget();
    }

    private void SetNextTarget()
    {
        this.curNode = this.nextNode;
        this.nextNode = null;
        if (this.curNode != null)
        {
            this.movementController.TargetPosition = this.curNode.position;
            this.movementController.MovementState = MovementState.Walking;             
        }

    }   
}
