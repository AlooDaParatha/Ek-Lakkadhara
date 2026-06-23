using UnityEngine;
using UnityEngine.InputSystem;

public class cylinder_movement : MonoBehaviour
{
    Vector3 motion_dir = new Vector3(11.0f, 0.0f, -1341.0f);
    Vector3 cam_dir = new Vector3(1f, 0f, 1f);
    float speed = 5.0f;
    public Transform targetCameraTransform;
    int verticalInput = 0;
    int horizontalInput = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(11.0f, 0.0f, -1341.0f);
    }

    // Update is called once per frame
    void Update()
    {
        cam_dir = targetCameraTransform.forward;
        cam_dir.x = 0 ;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) verticalInput = 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) verticalInput = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1;
        
            if (horizontalInput != 0)
            {
                motion_dir.x = transform.position.x + speed*horizontalInput;
                transform.position = motion_dir;
                horizontalInput = 0;
            }
            if (verticalInput != 0)
            {
                motion_dir.z = transform.position.z + speed*verticalInput;
                transform.position = motion_dir;
                verticalInput = 0;
            }
        }
        
    }
}