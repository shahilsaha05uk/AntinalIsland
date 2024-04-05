using UnityEngine;

/// <summary>
/// Allows 3D camera movement based on mouse drag.
/// </summary>
public class CameraMouseDragPanning3D : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField][Condition("enableDragPanning", true)] private MouseButton dragMouseButton = MouseButton.Right;

    [Header("Drag Panning Settings")]
    [SerializeField] private bool enableDragPanning = true;
    [SerializeField][Condition("enableDragPanning", true)] private float dragPanSpeed = 10f;

    private Vector3 lastMousePosition;
    private bool isDragActive;

    private enum MouseButton
    {
        Left = 0,
        Middle = 2,
        Right = 1
    }

    /// <summary>
    /// Update is called once per frame, handles drag panning if enabled.
    /// </summary>
    private void Update()
    {
        if (enableDragPanning) HandleDragPanning();
    }

    /// <summary>
    /// Handles camera movement based on mouse dragging.
    /// </summary>
    private void HandleDragPanning()
    {
        if (Input.GetMouseButtonDown((int)dragMouseButton)) StartDragging();
        else if (Input.GetMouseButtonUp((int)dragMouseButton)) StopDragging();

        if (isDragActive)
        {
            Vector3 mouseMovementDelta = CalculateMouseMovementDelta();
            MoveCamera(mouseMovementDelta);
            UpdateLastMousePosition();
        }
    }

    /// <summary>
    /// Starts camera dragging when the specified mouse button is pressed.
    /// </summary>
    private void StartDragging()
    {
        isDragActive = true;
        lastMousePosition = Input.mousePosition;
    }

    /// <summary>
    /// Stops camera dragging when the specified mouse button is released.
    /// </summary>
    private void StopDragging()
    {
        isDragActive = false;
    }

    /// <summary>
    /// Calculates the mouse movement delta since the last frame.
    /// </summary>
    /// <returns>The mouse movement delta as a Vector3.</returns>
    private Vector3 CalculateMouseMovementDelta()
    {
        return (Vector3)Input.mousePosition - lastMousePosition;
    }

    /// <summary>
    /// Moves the camera based on the mouse movement delta.
    /// </summary>
    /// <param name="mouseMovementDelta">The mouse movement delta.</param>
    private void MoveCamera(Vector3 mouseMovementDelta)
    {
        Vector3 inputDir = new Vector3(-mouseMovementDelta.x, 0, -mouseMovementDelta.y) * dragPanSpeed;
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * Time.unscaledDeltaTime;
    }

    /// <summary>
    /// Updates the last mouse position for tracking movement delta.
    /// </summary>
    private void UpdateLastMousePosition()
    {
        lastMousePosition = Input.mousePosition;
    }
}
