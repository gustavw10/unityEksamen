using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controller;
    public Transform groundCheck;
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.8f;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity; 
    public Vector3 oldPosition;
    private float currentSpeed;
    private float groundSlopeAngle = 0f;  // angle
    private Vector3 groundSlopeDir;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        //MOVE
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        //STOPPING
        currentSpeed = Vector3.Distance(oldPosition, transform.position) * 100f;
        oldPosition = transform.position;
        //Debug.Log("Speed: " + currentSpeed.ToString("F2"));
        if(currentSpeed < 0.5){
            speed = 7f;
        }

        //SLOPES
        if(groundSlopeAngle > 10f && speed < 60 && (x != 0 || z != 0)){
            if(speed < 40){
            speed = speed + 5 * Time.deltaTime;
            }
            if(speed >= 40){
            speed = speed + 3 * Time.deltaTime;
            }
        }

        if(groundSlopeAngle < 10f && speed > 7){
            speed = speed - 5 * Time.deltaTime;
            if(speed < 7){
                speed = 7f;
            }
        }


        //JUMP
        if(Input.GetButtonDown("Jump") && isGrounded){
            gravity = -9.8f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //GRAV
        if(groundSlopeAngle > 5 && groundSlopeAngle < 30){
            gravity = -3000f;
        }
        else{
            gravity = -9.8f;
        }
    }

    void OnControllerColliderHit (ControllerColliderHit hit)
{
    Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
    groundSlopeDir = Vector3.Cross(temp, hit.normal);
    groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    //Debug.Log(groundSlopeAngle);
}
}
