using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    // Start is called before the first frame update
    private string mouseXInputName, mouseYInputName;
    private float mouseSensitivity;
    
    void Awake()
    {
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LockCursor(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CamerRotation(){
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;
    }
}
