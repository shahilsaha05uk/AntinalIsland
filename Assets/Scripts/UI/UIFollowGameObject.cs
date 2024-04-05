using UnityEngine;

/// <summary>
/// Follows a target GameObject's position on the screen and applies an offset.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIFollowGameObject : MonoBehaviour
{
    [SerializeField] private GameObject followTarget;
    [SerializeField] private Vector2 screenSpaceOffset = new Vector2(0f, 100f);

    private Camera mainCamera;
    private RectTransform canvasRectTransform;
    private RectTransform thisRectTransform;

    /// <summary>
    /// Initializes references to the main camera and RectTransforms.
    /// </summary>
    private void Awake()
    {
        InitializeReferences();
    }

    /// <summary>
    /// Initializes references to the main camera and RectTransforms.
    /// </summary>
    private void InitializeReferences()
    {
        mainCamera = Camera.main;
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        thisRectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Updates the position to follow the target GameObject.
    /// </summary>
    private void Update()
    {
        if (followTarget != null)
        {
            SetPositionToFollowTarget();
        }
    }

    /// <summary>
    /// Sets the position to follow the target GameObject.
    /// </summary>
    private void SetPositionToFollowTarget()
    {
        Vector2 screenPosition = GetScreenPositionOfObject(followTarget);
        Vector2 localPoint = ConvertToRectTransformLocalPoint(screenPosition);
        thisRectTransform.anchoredPosition = localPoint + screenSpaceOffset;
    }

    /// <summary>
    /// Converts the world position of the target GameObject to screen space.
    /// </summary>
    /// <param name="targetObject">The target GameObject to follow.</param>
    /// <returns>The screen position of the target GameObject.</returns>
    private Vector2 GetScreenPositionOfObject(GameObject targetObject)
    {
        Vector3 objectPosition = targetObject.transform.position;
        return mainCamera.WorldToScreenPoint(objectPosition);
    }

    /// <summary>
    /// Converts a screen position to a RectTransform's local point.
    /// </summary>
    /// <param name="screenPosition">The screen position to convert.</param>
    /// <returns>The local point within the canvas RectTransform.</returns>
    private Vector2 ConvertToRectTransformLocalPoint(Vector2 screenPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, null, out localPoint);
        return localPoint;
    }

    public void SetTarget(Transform target)
    {
        followTarget = target.gameObject;
    }
}
