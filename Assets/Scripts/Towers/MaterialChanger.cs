using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    private Color _originalColor;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on the GameObject.");
        }
    }

    /// <summary>
    /// Sets the color of the material.
    /// </summary>
    /// <param name="newColor">The new color to set.</param>
    public void SetMaterialColor(Color newColor)
    {
        if (_renderer != null)
        {
            _renderer.material.color = newColor;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on the GameObject. Cannot set material color.");
        }
    }

    /// <summary>
    /// Resets the color of the material to its original color.
    /// </summary>
    public void ResetMaterialColor()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on the GameObject. Cannot reset material color.");
        }
    }

    public void SetMaterialColorToYellow()
    {
        SetMaterialColor(Color.yellow);
    }
}
