using SpaceBattles.Core.Domain.GameSystems;

namespace SpaceBattles.Tests.Domain.GameSystems;

public class ItemGridTests
{
    sealed class TestItem : IGridItem
    {
        public bool CanBeRemoved => true;
    }
    
    [Fact]
    public void ItemCount()
    {
        // Arrange
        const int width = 8;
        const int height = 6;
        
        // Act
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        
        Assert.Equal(width * height, grid.GetItems().Count());
    }
    
    [Fact]
    public void GridDimensions()
    {
        // Arrange
        const int width = 8;
        const int height = 6;
        
        // Act
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        
        // Assert
        Assert.Equal(height, grid.Height);
        Assert.Equal(width, grid.Width);
    }
    
    [Fact]
    public void CreatedEmpty()
    {
        // Arrange
        const int width = 8;
        const int height = 6;
        
        // Act
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        
        // Assert
        Assert.True(grid.GetItems().All(item => item.IsEmpty));
    }
    
    [Fact]
    public void Insert()
    {
        // Arrange
        const int width = 8;
        const int height = 6;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);

        // Act
        bool inserted = grid.Insert(new TestItem(), 0, 0);
        
        // Assert
        Assert.True(inserted);
        Assert.Equal(1, grid.GetItems().Count(item => !item.IsEmpty));
    }
    
    [Fact]
    public void CannotInsertIfCellIsNotEmpty()
    {
        // Arrange
        const int width = 8;
        const int height = 6;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);

        // Act
        grid.Insert(new TestItem(), 0, 0);
        bool inserted = grid.Insert(new TestItem(), 0, 0);
        
        // Assert
        Assert.False(inserted);
        Assert.Equal(1, grid.GetItems().Count(item => !item.IsEmpty));
    }
    
    [Fact]
    public void Clear()
    {
        // Arrange
        const int width = 8;
        const int height = 6;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid.Insert(new TestItem(), i, j);
            }
        }
        
        Assert.Equal(8 * 6, grid.GetItems().Count(item => !item.IsEmpty));
        
        // Act
        grid.Clear();
        
        // Assert
        Assert.Equal(8 * 6, grid.GetItems().Count(item => item.IsEmpty));
    }
    
    [Fact]
    public void ClearCell()
    {
        // Arrange
        const int width = 1;
        const int height = 1;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);

        grid.Insert(new TestItem(), 0, 0);
        
        Assert.Equal(1, grid.GetItems().Count(item => !item.IsEmpty));
        
        // Act
        grid.ClearCell(0, 0);
        
        // Assert
        Assert.Equal(1, grid.GetItems().Count(item => item.IsEmpty));
    }

    [Theory]
    [InlineData(0, 0, 2)]
    [InlineData(2, 2, 2)]
    [InlineData(1, 1, 4)]
    [InlineData(2, 1, 3)]
    public void GetAdjacentCells_Coordinates(int x, int y, int expectedCount)
    {
        // Arrange
        const int width = 3;
        const int height = 3;
        
        // Act
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        
        // Assert
        Assert.Equal(expectedCount, grid.GetAdjacentCells(x, y).Count());
        Assert.Equal(expectedCount, grid.GetAdjacentCells(y, x).Count());
    }
    
    [Fact]
    public void GetAdjacentCells_Object_Found()
    {
        // Arrange
        const int width = 3;
        const int height = 3;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        TestItem cell = new TestItem();
        
        // Act
        grid.Insert(cell, 2, 2);
        
        // Assert
        Assert.Equal(2, grid.GetAdjacentCells(cell).Count());
    }
    
    [Fact]
    public void GetAdjacentCells_Object_NotFound()
    {
        // Arrange
        const int width = 3;
        const int height = 3;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        TestItem cell = new TestItem();
        
        // Act
        grid.Insert(cell, 0, 0);
        
        // Assert
        Assert.Empty(grid.GetAdjacentCells(new TestItem()));
    }
    
    [Fact]
    public void GetAdjacentCells_Cell_Found()
    {
        // Arrange
        const int width = 3;
        const int height = 3;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        TestItem item = new TestItem();
        
        // Act
        grid.Insert(item, 0, 0);

        GridCell<TestItem> cell = grid[0, 0];
        
        // Assert
        Assert.Equal(2, grid.GetAdjacentCells(cell).Count());
    }
    
    [Fact]
    public void GetAdjacentCells_Cell_NotFound()
    {
        // Arrange
        const int width = 3;
        const int height = 3;
        
        ItemGrid<TestItem> grid = new ItemGrid<TestItem>(width, height);
        TestItem item = new TestItem();
        
        // Act
        grid.Insert(item, 0, 0);
        
        // Assert
        Assert.Empty(grid.GetAdjacentCells(new GridCell<TestItem>()));
    }
}