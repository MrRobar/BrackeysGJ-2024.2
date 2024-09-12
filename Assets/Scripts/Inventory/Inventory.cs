using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AbstractItem[] items = new AbstractItem[5];
    [SerializeField] private int currentItemId = 0;

    public event Action<int> OnSelectedSlot; 
    public event Action<AbstractItem[]> OnInventoryUpdated;

    public void AddItem(AbstractItem item)
    {
        if (items[currentItemId] != null)
        {
            DropCurrentItem();
        }

        items[currentItemId] = item;
        OnInventoryUpdated?.Invoke(items);
    }

    public void RemoveCurrentItem()
    {
        items[currentItemId] = null;
        OnInventoryUpdated?.Invoke(items);
    }

    public void DropCurrentItem()
    {
        items[currentItemId].Drop();
    }

    public void SelectSlot(int slotId)
    {
        currentItemId = slotId;
        OnSelectedSlot?.Invoke(currentItemId);
    }
}