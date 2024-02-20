using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SteeringBehaviors))]
public class EnemyEntity : BaseEntity
{

    private SteeringBehaviors steering = null;
    // Start is called before the first frame update
    void Start()
    {
        steering = GetComponent<SteeringBehaviors>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 force = steering.CombinedForce();

        Vector3 acceleration = force / mass;

        CurrVel += acceleration * Time.deltaTime;

        CurrVel = Vector3.ClampMagnitude(CurrVel, MaxSpeed);

        transform.position += CurrVel * Time.deltaTime;

        UpdateDirection();


        Debug.DrawRay(transform.position, FacingVec*10.0f, Color.red,0.0f,true);
        Debug.DrawRay(transform.position, RightVec*5.0f, Color.blue,0.0f,true);
        
    }
}
