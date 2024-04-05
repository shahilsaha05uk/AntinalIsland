using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GridManager gridManager; // Reference to the grid manager
    [SerializeField] private ResourceManager resourceManager; // Reference to the resource manager
    [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Material validPlacementMaterial; // Material to indicate valid placement
    [SerializeField] private Material invalidPlacementMaterial; // Material to indicate invalid placement
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground objects

    private bool isPlacing = false; // Flag to indicate if a tower is being placed
    private GameObject ghostTower; // Ghost tower object for visualization during placement
    private int selectedTowerIndex = 0;

    public bool IsPlacing => isPlacing;

    private void Awake()
    {
        if (gridManager == null) Debug.LogError("GridManager reference not set in TowerPlacer.");
        if (resourceManager == null) Debug.LogError("ResourceManager reference not set in TowerPlacer.");
    }

    private void Start()
    {
        InitializeGhostTower();
    }

    // Initialize the ghost tower object
    private void InitializeGhostTower()
    {
        ghostTower = Instantiate(towerPrefabs[selectedTowerIndex], Vector3.zero, Quaternion.identity);
        ghostTower.SetActive(false);
    }

    private void Update()
    {
        // Check if the pointer is over a UI element, hide ghost tower if true
        if (EventSystem.current.IsPointerOverGameObject())
        {
            ghostTower.SetActive(false);
            return;
        }

        // Check if the player has pressed the right mouse button to cancel tower placement
        if (Input.GetMouseButtonDown(1))
        {
            ghostTower.SetActive(false);
            isPlacing = false;
            return;
        }

        // Handle tower placement if placing mode is active
        if (isPlacing)
        {
            HandleTowerPlacement();
        }
    }

    // Handle tower placement logic
    private void HandleTowerPlacement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Raycast to find the ground position for tower placement
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector2 worldPosition = new Vector2(hit.point.x, hit.point.z);
            Vector2 gridPosition = gridManager.GetGridPosition(worldPosition);
            GridCell cell = gridManager.GetGridCell(gridPosition);

            // Check if a valid grid cell is found
            if (cell != null)
            {
                CheckCellPlacementValidity(cell);
            }
        }
        else
        {
            ghostTower.SetActive(false);
        }
    }

    // Check if the cell allows tower placement
    private void CheckCellPlacementValidity(GridCell cell)
    {
        if (!cell.isOccupied && cell.isValid)
        {
            Vector3 cellCenterPosition = new Vector3(cell.worldPosition.x + gridManager.CellSize * 0.5f, 0, cell.worldPosition.y + gridManager.CellSize * 0.5f);
            ghostTower.transform.position = cellCenterPosition;
            ghostTower.SetActive(true);
            SetTowerMaterial(validPlacementMaterial);

            // Place tower if left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower(cellCenterPosition, cell);
            }
        }
        else
        {
            ghostTower.SetActive(false);
        }
    }

    // Instantiate and place the tower at the given position
    private void PlaceTower(Vector3 position, GridCell cell)
    {
        // Fetch the buyCost from the Tower component of the selected prefab
        int towerBuyCost = towerPrefabs[selectedTowerIndex].GetComponent<Tower>().GetBuyCost();

        if (resourceManager.CanAffordPurchase(towerBuyCost))
        {
            GameObject newTower = Instantiate(towerPrefabs[selectedTowerIndex], position, Quaternion.identity);
            Tower newTowerComponent = newTower.GetComponent<Tower>();
            if (newTowerComponent != null)
            {
                newTowerComponent.SetOccupiedCell(cell);
                newTowerComponent.SetActive(true);
            }
            cell.isOccupied = true;
            resourceManager.ConsumeResources(towerBuyCost);
        }
        else
        {
            Debug.Log("Not enough resources to place the tower.");
        }
    }

    // Set material of the ghost tower to indicate placement validity
    private void SetTowerMaterial(Material material)
    {
        Renderer[] renderers = ghostTower.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.material = material;
        }
    }

    public void TogglePlacingMode(int towerIndex)
    {
        if (towerIndex >= 0 && towerIndex < towerPrefabs.Length)
        {
            selectedTowerIndex = towerIndex;
            isPlacing = !isPlacing;

            // When toggling, re-initialize the ghost tower to reflect the selected prefab
            if (ghostTower != null) Destroy(ghostTower);
            InitializeGhostTower();
            ghostTower.SetActive(isPlacing);
        }
        else
        {
            Debug.LogError($"Invalid tower index: {towerIndex}. Please use a value between 0 and {towerPrefabs.Length - 1}.");
        }
    }
}
