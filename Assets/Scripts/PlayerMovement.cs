using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 18.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 100.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    //TODELETE
    public Transform groundCheck;
    private float groundSlopeAngle = 0f;  // angle
    private Vector3 groundSlopeDir;

    //END

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : runningSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : runningSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //SLOPES
        if(isRunning){
        if(groundSlopeAngle > 10f && runningSpeed < 60 && characterController.isGrounded){
            if(runningSpeed < 40){
            runningSpeed = runningSpeed + 12 * Time.deltaTime;
            }
            if(runningSpeed >= 40){
            runningSpeed = runningSpeed + 3 * Time.deltaTime;
            }
        }
        }
        if((groundSlopeAngle < 10f && runningSpeed > 11.5f) || !isRunning){
            if(runningSpeed > 35){
            runningSpeed = runningSpeed - 8 * Time.deltaTime;
            }
            else
            {
            runningSpeed = runningSpeed - 5 * Time.deltaTime;
            }
            if(runningSpeed < 11.5f){
                runningSpeed = 11.5f;
            }
        }

        //JUMP
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit -20, lookXLimit + 20);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

      void OnControllerColliderHit (ControllerColliderHit hit)
{
    Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
    groundSlopeDir = Vector3.Cross(temp, hit.normal);
    groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    }
}