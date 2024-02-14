using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : BaseEntity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, FacingVec*10.0f, Color.red,0.0f,true);
        Debug.DrawRay(transform.position, RightVec*5.0f, Color.blue,0.0f,true);
        
    }
}
