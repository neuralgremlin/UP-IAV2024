using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    public float MaxSpeed {get; protected set;} = 500.0f;
    protected float mass = 1.0f;

    public Vector3 CurrVel {get; protected set;} = Vector3.zero;
    public Vector3 FacingVec {get; protected set;} = Vector3.forward;
    public Vector3 RightVec {get; protected set;} = Vector3.right;

    protected float currDeg = 0.0f;
    protected Quaternion newRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {   
    }

    protected void UpdateDirection()
    {
        if(CurrVel.magnitude > 0.0f)
        {
            float angle = Vector3.Angle(FacingVec,CurrVel);

            if(Vector3.Dot(RightVec,CurrVel) < 0.0f)
            {
                angle *= -1.0f;
            }

            currDeg += angle;

            newRotation.eulerAngles = new Vector3(0.0f,currDeg,0.0f);

            transform.rotation = newRotation;
        }
    }
}
