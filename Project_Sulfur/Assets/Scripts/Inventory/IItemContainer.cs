
public interface IItemContainer
{
    int ItemCount(_Item item);
    bool ContainsItem(_Item item);
    bool RemoveItem(_Item item);
    bool AddItem(_Item item);
    bool IsFull();  
}
