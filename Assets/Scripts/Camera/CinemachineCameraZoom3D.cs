using Cinemachine;
using UnityEngine;

/// <summary>
/// Allows controlling the zoom of a Cinemachine camera using keyboard and scroll wheel input.
/// </summary>
public class CinemachineCameraZoom3D : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private bool enableKeyboardZoom = true;
    [SerializeField] private bool enableScrollWheelZoom = true;
    [SerializeField][Condition("enableKeyboardZoom", true)] private KeyCode zoomInKey = KeyCode.PageUp;
    [SerializeField][Condition("enableKeyboardZoom", true)] private KeyCode zoomOutKey = KeyCode.PageDown;
    [SerializeField][Condition("enableScrollWheelZoom", true)] private string mouseInputAxis = "Mouse ScrollWheel";
    [SerializeField][Condition("enableScrollWheelZoom", true)] private bool invertScrollWheelZoom = false;

    [Header("Zoom Settings")]
    [SerializeField] private float followOffsetMinY = 10f;
    [SerializeField] private float followOffsetMaxY = 50f;
    [SerializeField][Condition("enableKeyboardZoom", true)] private float keyboardZoomSpeed = 0.05f;
    [SerializeField][Condition("enableScrollWheelZoom", true)] private float scrollZoomSpeed = 10f;
    [SerializeField] private float zoomSmoothing = 15f;

    private CinemachineTransposer cinemachineTransposer;
    private Vector3 followOffset;
    private float currentZoomValue;
    private float targetZoomValue;

    /// <summary>
    /// Finds the CinemachineTransposer component in the children of the parent transform.
    /// </summary>
    private void Awake()
    {
        FindCinemachineTransposerInChildren(transform.parent);
        InitializeZoom();
    }

    /// <summary>
    /// Finds and assigns the CinemachineTransposer component.
    /// </summary>
    private void FindCinemachineTransposerInChildren(Transform parent)
    {
        cinemachineTransposer = parent.GetComponentInChildren<CinemachineTransposer>();

        if (cinemachineTransposer == null)
        {
            Debug.LogError("CinemachineTransposer not found in children of parent!");
        }
    }

    /// <summary>
    /// Initializes the camera zoom settings.
    /// </summary>
    private void InitializeZoom()
    {
        followOffset = cinemachineTransposer.m_FollowOffset;
        currentZoomValue = targetZoomValue = followOffset.y;
    }

    /// <summary>
    /// Update is called once per frame, handles camera zoom.
    /// </summary>
    private void Update()
    {
        HandleCameraZoom();
    }

    /// <summary>
    /// Handles the camera zoom operations.
    /// </summary>
    private void HandleCameraZoom()
    {
        if (enableKeyboardZoom) HandleKeyboardZoom();
        if (enableScrollWheelZoom) HandleScrollWheelZoom();
        ClampZoomValue();
        SmoothZooming();
    }

    /// <summary>
    /// Handles zooming using the keyboard input.
    /// </summary>
    private void HandleKeyboardZoom()
    {
        float zoomInput = Input.GetKey(zoomInKey) ? -1f : (Input.GetKey(zoomOutKey) ? 1f : 0f);
        targetZoomValue += zoomInput * keyboardZoomSpeed;
    }

    /// <summary>
    /// Handles zooming using the scroll wheel input.
    /// </summary>
    private void HandleScrollWheelZoom()
    {
        float scroll = Input.GetAxis(mouseInputAxis);
        if (invertScrollWheelZoom) scroll *= -1f;
        targetZoomValue -= scroll * scrollZoomSpeed;
    }

    /// <summary>
    /// Clamps the target zoom value within specified limits.
    /// </summary>
    private void ClampZoomValue()
    {
        targetZoomValue = Mathf.Clamp(targetZoomValue, followOffsetMinY, followOffsetMaxY);
    }

    /// <summary>
    /// Smoothly adjusts the camera zoom based on the target zoom value.
    /// </summary>
    private void SmoothZooming()
    {
        currentZoomValue = Mathf.Lerp(currentZoomValue, targetZoomValue, Time.deltaTime * zoomSmoothing);
        followOffset.y = currentZoomValue;
        cinemachineTransposer.m_FollowOffset = followOffset;
    }
}
