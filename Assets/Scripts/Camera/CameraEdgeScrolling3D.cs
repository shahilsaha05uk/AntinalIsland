using UnityEngine;

/// <summary>
/// Handles camera movement based on mouse position near the screen edges.
/// </summary>
public class CameraEdgeScrolling3D : MonoBehaviour
{
    [Header("Edge Scrolling Settings")]
    [SerializeField] private bool enableEdgeScrolling = true;
    [SerializeField][Condition("enableEdgeScrolling", true)] private bool scrollOnlyWhenMouseOnScreen = false;
    [SerializeField][Condition("enableEdgeScrolling", true)] private float panSpeed = 10f;
    [SerializeField][Condition("enableEdgeScrolling", true)] private int edgeScrollSize = 20;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        if (enableEdgeScrolling && (!scrollOnlyWhenMouseOnScreen || IsMouseOnScreen()))
        {
            HandleEdgeScrolling();
        }
    }

    /// <summary>
    /// Handles camera movement when the mouse is near the edges of the screen.
    /// </summary>
    private void HandleEdgeScrolling()
    {
        Vector3 inputDir = CalculateInputDirection();
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * panSpeed * Time.unscaledDeltaTime;
    }

    /// <summary>
    /// Calculates the input direction based on the mouse position.
    /// </summary>
    /// <returns>The input direction vector.</returns>
    private Vector3 CalculateInputDirection()
    {
        Vector3 inputDir = Vector3.zero;
        Vector2 mousePosition = Input.mousePosition;

        if (mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
        }
        if (mousePosition.y < edgeScrollSize)
        {
            inputDir.z = -1f;
        }
        if (mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDir.x = 1f;
        }
        if (mousePosition.y > Screen.height - edgeScrollSize)
        {
            inputDir.z = 1f;
        }

        return inputDir;
    }

    /// <summary>
    /// Checks if the mouse cursor is on the screen.
    /// </summary>
    /// <returns>True if the mouse is on the screen, false otherwise.</returns>
    private bool IsMouseOnScreen()
    {
        Vector2 mousePosition = Input.mousePosition;
        return mousePosition.x >= 0 && mousePosition.x <= Screen.width
            && mousePosition.y >= 0 && mousePosition.y <= Screen.height;
    }
}
