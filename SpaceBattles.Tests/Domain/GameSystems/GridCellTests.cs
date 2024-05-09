using SpaceBattles.Core.Domain.GameSystems;

namespace SpaceBattles.Tests.Domain.GameSystems;

public class GridCellTests
{
    sealed class TestItem : IGridItem
    {
        public TestItem(bool removable = true)
        {
            CanBeRemoved = removable;
        }
        
        public bool CanBeRemoved { get; set; }
    }
    
    [Fact]
    public void GridCell()
    {
        // Arrange
        GridCell<TestItem> cell = new GridCell<TestItem>();

        // Act

        // Assert
        Assert.True(cell.IsEmpty);
    }
    
    [Fact]
    public void Insert()
    {
        // Arrange
        GridCell<TestItem> cell = new GridCell<TestItem>();

        // Act
        bool result = cell.Insert(new TestItem());

        // Assert
        Assert.True(result);
        Assert.False(cell.IsEmpty);
    }
    
    [Fact]
    public void Clear()
    {
        // Arrange
        GridCell<TestItem> cell = new GridCell<TestItem>();
        cell.Insert(new TestItem());
        
        // Act
        cell.Clear();
        
        // Assert
        Assert.True(cell.IsEmpty);
    }
    
    [Fact]
    public void TryRemove_CellIsEmpty()
    {
        // Arrange
        GridCell<TestItem> cell = new GridCell<TestItem>();
        
        // Act
        bool success = cell.TryRemove(out TestItem? item);
        
        // Assert
        Assert.False(success);
        Assert.Null(item);
    }
    
    [Fact]
    public void TryRemove_CellWithItem()
    {
        // Arrange
        GridCell<TestItem> cell = new GridCell<TestItem>();
        cell.Insert(new TestItem());

        // Act
        bool success = cell.TryRemove(out TestItem? item);
        
        // Assert
        Assert.True(success);
        Assert.True(cell.IsEmpty);
        Assert.NotNull(item);
    }
    
    [Fact]
    public void TryRemove_CellWithUnremovableItem()
    {
        // Arrange
        GridCell<TestItem> cell = new GridCell<TestItem>();
        cell.Insert(new TestItem(false));

        // Act
        bool success = cell.TryRemove(out TestItem? item);
        
        // Assert
        Assert.False(success);
        Assert.False(cell.IsEmpty);
        Assert.Null(item);
    }
}