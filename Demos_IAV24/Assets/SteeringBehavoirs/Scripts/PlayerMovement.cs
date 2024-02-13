using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [Header("Movement")]
    public float movSpeed;
    public float floorDrag;
    public Transform direction;

    [Header("Ground Check")]
    public Collider playerObject;
    public LayerMask isFloor;
    bool touchingFloor;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMovMult;
    bool jumpReady;
    public KeyCode jumpKey = KeyCode.Space;

    float horizontalInput;
    float verticalInput;
    Vector3 movDirection;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpReady = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        float playerHeight = playerObject.bounds.size.y;
        touchingFloor = Physics.Raycast(transform.position,Vector3.down, playerHeight*0.5f + 0.2f,isFloor);
        PlayerInput();
        SpeedLimit();

        if (touchingFloor)
            rb.drag = floorDrag;
        else
            rb.drag = 0;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && jumpReady && touchingFloor)
        {
            jumpReady = false;

            Jump();

            Invoke(nameof(JumpReset),jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        movDirection = direction.forward*verticalInput + direction.right*horizontalInput;

            if(touchingFloor)
                rb.AddForce(movDirection.normalized*movSpeed*10f,ForceMode.Force);
            else if (!touchingFloor)
                rb.AddForce(movDirection.normalized*movSpeed*10f*airMovMult,ForceMode.Force);
    }


    private void SpeedLimit()
    {
        Vector3 currSpeed = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        if(currSpeed.magnitude > movSpeed)
        {
             Vector3 cappedSpeed = currSpeed.normalized * movSpeed;
             rb.velocity = new Vector3 (cappedSpeed.x,rb.velocity.y,cappedSpeed.z);

        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up*jumpForce,ForceMode.Impulse);
    }
    private void JumpReset()
    {
        jumpReady = true;
    }
}
