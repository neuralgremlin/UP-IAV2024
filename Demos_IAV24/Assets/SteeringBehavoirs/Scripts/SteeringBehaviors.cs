using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseEntity))]
public class SteeringBehaviors : MonoBehaviour
{
    private BaseEntity baseEntity = null;

    public bool seekEnabled = false;
    public bool arriveEnabled = false;
    public bool fleeEnabled = false;
    public bool avoidanceEnabled = false;
    public bool wanderEnabled = false;
    public bool flockingEnabled = false;

    public Transform target = null;


    // Start is called before the first frame update
    void Start()
    {
        baseEntity = GetComponent<BaseEntity>();
    }

    Vector3 Seek(Vector3 targetPos)
    {
        Vector3 vecToTarget = targetPos - new Vector3(transform.position.x,targetPos.y,transform.position.z);

        Vector3 desiredVel = vecToTarget.normalized * baseEntity.MaxSpeed;

        desiredVel -= baseEntity.CurrVel;

        return desiredVel;

    }
}
