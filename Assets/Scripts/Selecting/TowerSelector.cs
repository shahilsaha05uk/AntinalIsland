using UnityEngine;
using UnityEngine.EventSystems;

// Handles selection and deselection of towers in the game
public class TowerSelector : MonoBehaviour
{
    [SerializeField] private TowerPlacer towerPlacer; // Reference to the TowerPlacer component
    [SerializeField] private LayerMask towerLayer; // Layer mask to filter tower objects

    private ISelectable currentlySelectedObject; // Currently selected tower object

    private void Awake()
    {
        if (towerPlacer == null) Debug.LogError("TowerPlacer reference not set in TowerSelector.");
    }

    private void Update()
    {
        // Do not handle input if the pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Right mouse button deselects the currently selected tower
        if (Input.GetMouseButtonDown(1))
        {
            DeselectCurrentlySelectedObject();
            return;
        }

        // Left mouse button selects a tower if not currently placing one
        if (Input.GetMouseButtonDown(0) && !towerPlacer.IsPlacing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Raycast to detect tower objects.
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, towerLayer))
            {
                // Deselect the previously selected object, if any
                if (currentlySelectedObject != null)
                {
                    DeselectCurrentlySelectedObject();
                }

                // Select the clicked tower object if it implements ISelectable
                currentlySelectedObject = hit.collider.gameObject.GetComponent<ISelectable>();
                if (currentlySelectedObject != null)
                {
                    currentlySelectedObject.Select();
                }
            }
        }
    }

    // Deselects the currently selected tower object.
    private void DeselectCurrentlySelectedObject()
    {
        if (currentlySelectedObject == null)
        {
            return;
        }

        currentlySelectedObject.Deselect();
        currentlySelectedObject = null;
    }
}
