using UnityEngine;

/// <summary>
/// Allows 3D camera rotation using both keyboard and mouse input.
/// </summary>
public class CameraRotation3D : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private bool enableKeyboardRotation = true;
    [SerializeField] private bool enableMouseRotation = true;
    [SerializeField][Condition("enableKeyboardRotation", true)] private KeyCode rotateLeftKey = KeyCode.E;
    [SerializeField][Condition("enableKeyboardRotation", true)] private KeyCode rotateRightKey = KeyCode.Q;
    [SerializeField][Condition("enableMouseRotation", true)] private MouseButton rotateMouseButton = MouseButton.Middle;
    [SerializeField][Condition("enableMouseRotation", true)] private bool invertMouseRotation = false;

    [Header("Rotation Settings")]
    [SerializeField][Condition("enableKeyboardRotation", true)] private float keyboardRotationSpeed = 250f;
    [SerializeField][Condition("enableMouseRotation", true)] private float mouseRotationSpeed = 2500f;

    private Vector2 mouseRotateVector;
    private bool mouseRotateActive;

    private enum MouseButton
    {
        Left = 0,
        Middle = 2,
        Right = 1
    }

    /// <summary>
    /// Update is called once per frame. It handles camera rotation.
    /// </summary>
    private void Update()
    {
        HandleCameraRotation();
    }

    /// <summary>
    /// Handles both keyboard and mouse-based camera rotation.
    /// </summary>
    private void HandleCameraRotation()
    {
        HandleKeyboardRotation();
        HandleMouseRotation();
    }

    /// <summary>
    /// Handles keyboard-based camera rotation if enabled.
    /// </summary>
    private void HandleKeyboardRotation()
    {
        float keyboardRotation = 0f;

        if (enableKeyboardRotation)
        {
            if (Input.GetKey(rotateLeftKey)) keyboardRotation = -1f;
            else if (Input.GetKey(rotateRightKey)) keyboardRotation = 1f;
        }

        RotateWithKeyboard(keyboardRotation);
    }

    /// <summary>
    /// Rotates the camera based on keyboard input.
    /// </summary>
    /// <param name="rotationInput">Input value for rotation.</param>
    private void RotateWithKeyboard(float rotationInput)
    {
        float rotationAmount = rotationInput * keyboardRotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationAmount);
    }

    /// <summary>
    /// Handles mouse-based camera rotation if enabled.
    /// </summary>
    private void HandleMouseRotation()
    {
        if (enableMouseRotation)
        {
            if (Input.GetMouseButton((int)rotateMouseButton))
            {
                mouseRotateActive = true;
                Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                mouseRotateVector = NormalizeMouseDelta(mouseDelta);

                if (invertMouseRotation) mouseRotateVector *= -1;
            }
            else mouseRotateActive = false;

            if (mouseRotateActive) RotateWithMouse();
        }
    }

    /// <summary>
    /// Normalizes mouse delta based on screen size and mouse rotation speed.
    /// </summary>
    /// <param name="mouseDelta">Mouse delta values.</param>
    /// <returns>Normalized mouse delta values.</returns>
    private Vector2 NormalizeMouseDelta(Vector2 mouseDelta)
    {
        return new Vector2(mouseDelta.x / Screen.width, mouseDelta.y / Screen.height) * mouseRotationSpeed;
    }

    /// <summary>
    /// Rotates the camera based on mouse input.
    /// </summary>
    private void RotateWithMouse()
    {
        float mouseX = mouseRotateVector.x;
        transform.Rotate(Vector3.up * mouseX);
    }
}
