using UnityEngine;

// Manages the grid and provides functionality to interact with it
public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int gridWidth = 100; // Width of the grid in cells
    [SerializeField] private int gridHeight = 100; // Height of the grid in cells
    [SerializeField] private float cellSize = 1f; // Size of each grid cell
    [SerializeField] private bool showGrid = true; // Toggle to display the grid in editor

    private GridCell[,] gridCells; // 2D array to store grid cells

    public float CellSize => cellSize; // Property to access the cell size

    private void Start()
    {
        CreateGrid();
    }

    // Creates the grid by instantiating grid cells
    private void CreateGrid()
    {
        gridCells = new GridCell[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 cellPosition = new Vector2(x, y);
                Vector2 worldPosition = new Vector2(x * cellSize, y * cellSize);
                gridCells[x, y] = new GridCell(cellPosition, worldPosition);
            }
        }
    }

    // Returns the GridCell object at the given grid position
    public GridCell GetGridCell(Vector2 position)
    {
        if (position.x >= 0 && position.x < gridWidth && position.y >= 0 && position.y < gridHeight)
        {
            return gridCells[(int)position.x, (int)position.y];
        }
        else
        {
            Debug.LogError("Requested position is out of grid bounds.");
            return null;
        }
    }

    // Converts a world position to grid coordinates
    public Vector2 GetGridPosition(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2(x, y);
    }

    // Converts grid coordinates to a world position
    public Vector2 GetWorldPosition(Vector2 gridPosition)
    {
        float x = gridPosition.x * cellSize + cellSize / 2;
        float y = gridPosition.y * cellSize + cellSize / 2;
        return new Vector2(x, y);
    }

    // Draws the grid lines and highlights occupied cells in the editor
    private void OnDrawGizmos()
    {
        if (!showGrid) return;

        if (gridCells == null || gridCells.GetLength(0) != gridWidth || gridCells.GetLength(1) != gridHeight)
        {
            CreateGrid();
        }

        Gizmos.color = Color.gray;

        // Draw the vertical grid lines
        for (int x = 0; x <= gridWidth; x++)
        {
            Gizmos.DrawLine(new Vector3(x * cellSize, 0, 0), new Vector3(x * cellSize, 0, gridHeight * cellSize));
        }

        // Draw the horizontal grid lines
        for (int z = 0; z <= gridHeight; z++)
        {
            Gizmos.DrawLine(new Vector3(0, 0, z * cellSize), new Vector3(gridWidth * cellSize, 0, z * cellSize));
        }

        // Highlight occupied cells in red
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                if (gridCells[x, z].isOccupied)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(new Vector3(x * cellSize + cellSize / 2, 0, z * cellSize + cellSize / 2), new Vector3(cellSize, 0.01f, cellSize));
                }
                else
                {
                    // Reset Gizmos color to gray for next cell
                    Gizmos.color = Color.gray;
                }
            }
        }
    }
}

// Represents a single cell in the grid
public class GridCell
{
    public Vector2 cellPosition; // Position of the cell in grid coordinates
    public Vector2 worldPosition; // Position of the cell in world coordinates
    public bool isOccupied; // Flag indicating if the cell is occupied
    public bool isValid; // Flag indicating if the cell is valid for placement

    // Constructor to initialize cell properties
    public GridCell(Vector2 cellPosition, Vector2 worldPosition)
    {
        this.cellPosition = cellPosition;
        this.worldPosition = worldPosition;
        isOccupied = false;
        isValid = true;
    }

    // Sets the occupied status of the cell
    public void SetOccupied(bool value)
    {
        isOccupied = value;
    }

    // Sets the validity status of the cell
    public void SetValid(bool value)
    {
        isValid = value;
    }
}
