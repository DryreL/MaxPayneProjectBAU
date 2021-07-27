using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Sensor that detects when the player is within the vision cone of the actor. 
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class VisionConeSensor : Sensor
{
    [SerializeField] private float fieldOfView = 110f;
    [SerializeField]
    private string targetTag = "Player"; 
    private SphereCollider sphereCollider;
    private Vector3 dirToPlayer;
    private float angleToPlayer;
    private RaycastHit hitInfo; 

    private void Awake()
    {
        this.sphereCollider = this.GetComponent<SphereCollider>(); 
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == this.targetTag)
        {                        
            this.dirToPlayer = (col.transform.position - this.transform.position).normalized;                       
            this.angleToPlayer = Vector3.Angle(dirToPlayer, this.transform.parent.forward);
            // is the angle between the player and actor within the FOV
            if (this.angleToPlayer < this.fieldOfView / 2)
            {
                // ensure that the sensor actually has a line of sight - an obstacle could be between the actor and player but the trigger has fired because we're in range.                 
                if (Physics.Raycast(this.transform.parent.position, dirToPlayer, out hitInfo, this.sphereCollider.radius, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
                {                                  
                    if (hitInfo.collider != null && hitInfo.collider.tag == this.targetTag)
                    {                                                                              
                        this.IsTriggered = true;
                    }
                    else
                    {                        
                        this.IsTriggered = false;                        
                    }
                }
                else
                {
                    this.IsTriggered = false;                    
                }
            }
            else
            {                
                this.IsTriggered = false;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == this.targetTag)
        {            
            this.IsTriggered = false;
        }
    }
}
