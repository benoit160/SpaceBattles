namespace SpaceBattles.Core.Domain.GameSystems;

public class ItemGrid<T>
    where T : class, IGridItem
{
    private readonly GridCell<T>[,] _cells;

    public ItemGrid(int width, int height)
    {
        _cells = new GridCell<T>[width, height];
        Width = width;
        Height = height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _cells[i, j] = new GridCell<T>();
            }
        }
    }

    public int Width { get; init; }

    public int Height { get; init; }

    public GridCell<T> this[int x, int y] => _cells[x, y];

    public bool Insert(T item, int x, int y)
    {
        return _cells[x, y].Insert(item);
    }

    public void Clear()
    {
        foreach (GridCell<T> cell in GetItems())
        {
            cell.Clear();
        }
    }

    public void ClearCell(int x, int y)
    {
        _cells[x, y].Clear();
    }

    public IEnumerable<GridCell<T>> GetItems()
    {
        int rows = _cells.GetLength(0);
        int cols = _cells.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                yield return _cells[i, j];
            }
        }
    }

    /// <summary>
    /// Returns the top, bottom, left and right cells if applicable.
    /// </summary>
    public IEnumerable<GridCell<T>> GetAdjacentCells(int x, int y)
    {
        int numRows = _cells.GetLength(0);
        int numCols = _cells.GetLength(1);

        // Check the cell to the left
        if (x > 0)
            yield return _cells[x - 1, y];

        // Check the cell to the right
        if (x < numCols - 1)
            yield return _cells[x + 1, y];

        // Check the cell above
        if (y > 0)
            yield return _cells[x, y - 1];

        // Check the cell below
        if (y < numRows - 1)
            yield return _cells[x, y + 1];
    }

    /// <summary>
    /// Returns the top, bottom, left and right cells if applicable.
    /// </summary>
    public IEnumerable<GridCell<T>> GetAdjacentCells(GridCell<T> cell)
    {
        int x = -1;
        int y = -1;

        int numRows = _cells.GetLength(0);
        int numCols = _cells.GetLength(1);

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (_cells[i, j] == cell)
                {
                    x = i;
                    y = j;
                }
            }
        }

        if (x == -1 && y == -1) yield break;

        // Check the cell to the left
        if (x > 0)
            yield return _cells[x - 1, y];

        // Check the cell to the right
        if (x < numCols - 1)
            yield return _cells[x + 1, y];

        // Check the cell above
        if (y > 0)
            yield return _cells[x, y - 1];

        // Check the cell below
        if (y < numRows - 1)
            yield return _cells[x, y + 1];
    }

    /// <summary>
    /// Returns the top, bottom, left and right cells if applicable.
    /// </summary>
    public IEnumerable<GridCell<T>> GetAdjacentCells(T item)
    {
        int x = -1;
        int y = -1;

        int numRows = _cells.GetLength(0);
        int numCols = _cells.GetLength(1);

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (_cells[i, j].Item == item)
                {
                    x = i;
                    y = j;
                }
            }
        }

        if (x == -1 && y == -1) yield break;

        // Check the cell to the left
        if (x > 0)
            yield return _cells[x - 1, y];

        // Check the cell to the right
        if (x < numCols - 1)
            yield return _cells[x + 1, y];

        // Check the cell above
        if (y > 0)
            yield return _cells[x, y - 1];

        // Check the cell below
        if (y < numRows - 1)
            yield return _cells[x, y + 1];
    }
}