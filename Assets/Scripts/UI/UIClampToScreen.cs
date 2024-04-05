using UnityEngine;

/// <summary>
/// Ensures that the attached RectTransform stays within the screen boundaries of its parent Canvas.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIClampToScreen : MonoBehaviour
{
    private RectTransform _canvasRectTransform;
    private RectTransform _thisRectTransform;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        _canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        _thisRectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// LateUpdate is called after all Update functions have been called. It clamps the RectTransform to the screen boundaries.
    /// </summary>
    private void LateUpdate()
    {
        ClampToScreen();
    }

    /// <summary>
    /// Clamps the RectTransform to the screen boundaries within the Canvas.
    /// </summary>
    private void ClampToScreen()
    {
        float clampX = CalculateClamp(_canvasRectTransform.rect.width, _thisRectTransform.rect.width);
        float clampY = CalculateClamp(_canvasRectTransform.rect.height, _thisRectTransform.rect.height);

        Vector2 clampedPosition = CalculateClampedPosition(clampX, clampY);
        _thisRectTransform.anchoredPosition = clampedPosition;
    }

    /// <summary>
    /// Calculates the maximum clamp value for a RectTransform's position based on its size and the canvas size.
    /// </summary>
    /// <param name="canvasSize">The size of the parent Canvas.</param>
    /// <param name="thisSize">The size of the RectTransform.</param>
    /// <returns>The maximum clamp value.</returns>
    private float CalculateClamp(float canvasSize, float thisSize)
    {
        return canvasSize * 0.5f - thisSize * 0.5f;
    }

    /// <summary>
    /// Calculates the clamped position for the RectTransform based on the provided clamp values.
    /// </summary>
    /// <param name="clampX">The maximum allowed clamp value in the X-axis.</param>
    /// <param name="clampY">The maximum allowed clamp value in the Y-axis.</param>
    /// <returns>The clamped position.</returns>
    private Vector2 CalculateClampedPosition(float clampX, float clampY)
    {
        Vector2 clampedPosition = _thisRectTransform.anchoredPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -clampX, clampX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -clampY, clampY);
        return clampedPosition;
    }
}
