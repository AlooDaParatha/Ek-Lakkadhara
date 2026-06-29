using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // ─── Inspector Settings ───────────────────────────────────────────
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 125f;        // was 5f, scaled 25x

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 50f;        // was 2f, scaled 25x
    [SerializeField] private float gravity = -500f;         // was -20f, scaled 25x

    [Header("Mouse Look")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;

    // ─── Private State ────────────────────────────────────────────────
    private CharacterController _controller;
    private float _verticalVelocity = 0f;
    private float _cameraPitch = 0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player body left/right (yaw) — this drives movement direction
        transform.Rotate(Vector3.up * mouseX);

        // Rotate ONLY the camera up/down (pitch) — player body stays upright
        _cameraPitch -= mouseY;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -85f, 85f);

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
    }

    void HandleMovement()
    {
        float horizontal = 0f;
        float vertical   = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)  horizontal += 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)   horizontal -= 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)     vertical   += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)   vertical   -= 1f;
        }

        // Use transform.right and transform.forward — these are HORIZONTAL only
        // because the player body never tilts on X. Camera pitches separately above,
        // so looking up/down does NOT pull movement into the air.
        Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 flatRight   = new Vector3(transform.right.x,   0f, transform.right.z).normalized;

        Vector3 moveDirection = (flatRight * horizontal) + (flatForward * vertical);

        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();

        // ── Grounded + Jump ──
        if (_controller.isGrounded)
        {
            if (_verticalVelocity < 0f)
                _verticalVelocity = -2f;

            bool jumpPressed = (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
                             || Input.GetButtonDown("Jump");

            if (jumpPressed)
                _verticalVelocity = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
        }

        _verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = _verticalVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
}