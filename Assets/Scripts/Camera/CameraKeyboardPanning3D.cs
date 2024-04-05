using UnityEngine;

/// <summary>
/// Allows 3D camera panning based on user input.
/// </summary>
public class CameraKeyboardPanning3D : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private bool enableKeyboardPanning = true;
    [SerializeField][Condition("enableKeyboardPanning", true)] private bool useRawInput = true;
    [SerializeField][Condition("enableKeyboardPanning", true)] private string horizontalInputAxis = "Horizontal";
    [SerializeField][Condition("enableKeyboardPanning", true)] private string verticalInputAxis = "Vertical";

    [Header("Panning Settings")]
    [SerializeField][Condition("enableKeyboardPanning", true)] private float panSpeed = 5f;

    private Vector2 moveInput;

    /// <summary>
    /// Called every frame to handle camera panning.
    /// </summary>
    private void Update()
    {
        GetInput();
        PanCamera();
    }

    /// <summary>
    /// Retrieves user input for camera movement.
    /// </summary>
    private void GetInput()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (enableKeyboardPanning)
        {
            horizontalInput = useRawInput ? Input.GetAxisRaw(horizontalInputAxis) : Input.GetAxis(horizontalInputAxis);
            verticalInput = useRawInput ? Input.GetAxisRaw(verticalInputAxis) : Input.GetAxis(verticalInputAxis);
        }

        moveInput = new Vector2(horizontalInput, verticalInput).normalized;
    }

    /// <summary>
    /// Pans the camera based on user input.
    /// </summary>
    private void PanCamera()
    {
        Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        transform.position += moveDir * panSpeed * Time.unscaledDeltaTime;
    }
}
