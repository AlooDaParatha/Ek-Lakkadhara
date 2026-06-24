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
    public cam_rotation cam_turn;

    public Vector2 turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(11.0f, 0.0f, -1341.0f);
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0f);
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) verticalInput = 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) verticalInput = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1;
            
            if (horizontalInput != 0)
            {
                motion_dir.x = transform.position.x + speed*Mathf.Cos(turn.y * 3.14f / 90.0f) * horizontalInput;
                motion_dir.z = transform.position.z + speed*Mathf.Sin(turn.y * 3.14f / 90.0f) * horizontalInput;
                transform.position = motion_dir;
                horizontalInput = 0;
            }
            if (verticalInput != 0)
            {
                motion_dir.x = transform.position.x + speed*Mathf.Cos(turn.y * 3.14f / 90.0f) * verticalInput;
                motion_dir.z = transform.position.z + speed*Mathf.Sin(turn.y * 3.14f / 90.0f) * verticalInput;
                transform.position = motion_dir;
                verticalInput = 0;
            }
        }
    }
}