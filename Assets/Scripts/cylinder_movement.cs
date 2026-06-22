using UnityEngine;
using UnityEngine.InputSystem;

public class cylinder_movement : MonoBehaviour
{
    Vector3 motion_dir = new Vector3(0f, 0f, 0f);
    Vector3 cam_dir = new Vector3(1f, 0f, 1f);
    float speed = 20.0f;
    public Transform targetCameraTransform;
    float verticalInput = 0.0f;
    float horizontalInput = 0.0f;
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
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) verticalInput = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) verticalInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1f;
        }
        if (horizontalInput == 1)
            motion_dir.x = transform.position.x + speed;
        horizontalInput = 0;
        verticalInput = 0;
    }
}
