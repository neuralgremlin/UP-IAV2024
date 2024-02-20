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

    public float maxForce = 10.0f;

    public float wanderDistance = 10.0f;
    public float wanderRadius = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        baseEntity = GetComponent<BaseEntity>();
    }

    public Vector3 CombinedForce()
    {
        float remainingForce = maxForce;
        Vector3 accumForce = Vector3.zero;

        if(seekEnabled)
        {
            Vector3 seekForce = Seek(target.position);

            if(seekForce.magnitude > remainingForce)
            {
                accumForce += seekForce.normalized * remainingForce;
                return accumForce;
            }
            accumForce += seekForce;
            remainingForce = maxForce-accumForce.magnitude;

        }
        if(arriveEnabled)
        {
            Vector3 arriveForce = Arrive(target.position);

            if(arriveForce.magnitude > remainingForce)
            {
                accumForce += arriveForce.normalized * remainingForce;
                return accumForce;
            }
            accumForce += arriveForce;
            remainingForce = maxForce-accumForce.magnitude;
        }
        if(fleeEnabled)
        {
            Vector3 fleeForce = Flee(target.position);

            if(fleeForce.magnitude > remainingForce)
            {
                accumForce += fleeForce.normalized * remainingForce;
                return accumForce;
            }
            accumForce += fleeForce;
            remainingForce = maxForce-accumForce.magnitude;
        }
        if(wanderEnabled)
        {
            Vector3 wanderForce = Wander();

            if(wanderForce.magnitude > remainingForce)
            {
                accumForce += wanderForce.normalized * remainingForce;
                return accumForce;
            }
            accumForce += wanderForce;
            remainingForce = maxForce-accumForce.magnitude;
        }

        return accumForce;
    }

    Vector3 Seek(Vector3 targetPos)
    {
        // Calcular Dirección hacia el objetivo
        Vector3 vecToTarget = targetPos - new Vector3(transform.position.x,targetPos.y,transform.position.z);

        Vector3 desiredVel = vecToTarget.normalized * baseEntity.MaxSpeed;

        desiredVel -= baseEntity.CurrVel;

        return desiredVel;

    }

    Vector3 Flee(Vector3 targetPos)
    {
        // Calcular Dirección desde el objetivo
        Vector3 vecToTarget = new Vector3(transform.position.x,targetPos.y,transform.position.z) - targetPos;

        // Normalizamos dirección y la escalamos
        Vector3 desiredVel = vecToTarget.normalized * baseEntity.MaxSpeed;

        // Restar velocidad actual paraa obtener la fuerza que necesitamos
        desiredVel -= baseEntity.CurrVel;

        return desiredVel;

    }

    Vector3 Arrive(Vector3 targetPos)
    {

        const float deceleration = 0.2f;

        // Calcular Dirección hacia el objetivo
        Vector3 vecToTarget = targetPos - new Vector3(transform.position.x,targetPos.y,transform.position.z);

        float distToTarget = vecToTarget.magnitude;

        if (distToTarget > 0 )
        {
            // Ajustar velocidaD
            float decelSpeed = distToTarget * deceleration;

            decelSpeed = Mathf.Min(baseEntity.MaxSpeed,decelSpeed);

            Vector3 desiredVel = vecToTarget.normalized * decelSpeed;

            
            desiredVel -= baseEntity.CurrVel;
            
            return desiredVel;

        }
    
    return Vector3.zero;
    }

    Vector3 Wander()
    {
        float randomPoint = Random.Range(-1.0f,1.0f);

        float randomDegrees = Mathf.Acos(randomPoint);

        if(Random.Range(0,100) > 50)
        {
            randomDegrees += Mathf.PI;
        }

        Vector3 wanderPosition = new Vector3(transform.position.x,0.0f,transform.position.z);
        //Vector3 wanderPosition = new Vector3(baseEntity.transform.position.x,0.0f,baseEntity.transform.position.z);

        wanderPosition += baseEntity.FacingVec * wanderDistance;


        Vector3 wanderDirection = Vector3.zero;
        wanderDirection.x = Mathf.Cos(randomDegrees) - Mathf.Sin(randomDegrees);
        wanderDirection.y = Mathf.Cos(randomDegrees) + Mathf.Sin(randomDegrees);

        wanderDirection *= wanderRadius;
        wanderPosition += wanderDirection;

        Vector3 rayPosition  = new Vector3(transform.position.x,0.0f,transform.position.z) + baseEntity.FacingVec * wanderDistance;
        Debug.DrawRay(rayPosition, wanderDirection, Color.green,0.0f,true);

        return Seek(wanderPosition);

    }
}
