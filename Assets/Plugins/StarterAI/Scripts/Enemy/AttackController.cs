using UnityEngine;
using System.Collections;

/// <summary>
/// Contains logic for an enemies ability to attack. 
/// </summary>
public class AttackController : MonoBehaviour
{
    [SerializeField]
    private float minAttackDistance;
    [SerializeField]
    private LayerMask allowedTargets;    

    public void TryAttack()
    {                
        float dist = Vector3.Distance(this.transform.position, Player.Instance.transform.position);

        if (dist <= this.minAttackDistance)
        {
            RaycastHit hitInfo;
            // position is based at the feet of the enemy, we need it to be a bit higher.                         
            Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, this.minAttackDistance * 2, this.allowedTargets.value);
            
            if (hitInfo.collider != null)
            {                
                this.Attack(hitInfo.collider.gameObject);
            }
        }        
    }

    public void Attack(GameObject target)
    {
        Debug.LogFormat("{0} attacked", target.name);      
    }
}
