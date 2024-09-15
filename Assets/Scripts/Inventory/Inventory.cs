using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AbstractItem[] items = new AbstractItem[5];
    [SerializeField] private int currentItemId = 0;
    [SerializeField] private Transform hand;
    public event Action<int> OnSelectedSlot;
    public event Action<AbstractItem[]> OnInventoryUpdated;
    
    public AbstractItem CurrentItem => items[currentItemId];

    private void Update()
    {
        HandleSlotSelection();
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropCurrentItem();
        }
    }

    private void HandleSlotSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0f)
        {
            SelectNextSlot();
        }
        else if (scroll > 0f)
        {
            SelectPreviousSlot();
        }
    }

    public int CountItemsOfType<T>()
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item is T)
            {
                count++;
            }
        }

        return count;
    }
    private void SelectSlot(int slotId)
    {
        if (slotId == currentItemId)
        {
            return;
        }

        if (slotId >= 0 && slotId < items.Length)
        {
            if (items[currentItemId])
            {
                items[currentItemId].gameObject.SetActive(false);
            }
            currentItemId = slotId;
            if (items[currentItemId])
            {
                items[currentItemId].gameObject.SetActive(true);
            }
            
            OnSelectedSlot?.Invoke(currentItemId);
        }
    }

    private void SelectNextSlot()
    {
        SelectSlot((currentItemId + 1) % items.Length);
    }

    private void SelectPreviousSlot()
    {
        SelectSlot((currentItemId - 1) % items.Length);
    }

    public void AddItem(AbstractItem item)
    {
        if (items[currentItemId] != null)
        {
            DropCurrentItem();
        }

        items[currentItemId] = item;
        item.transform.parent = hand;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        OnInventoryUpdated?.Invoke(items);
    }

    public void DropCurrentItem()
    {
        items[currentItemId].Drop();
        items[currentItemId].transform.parent = null;
        items[currentItemId] = null;
        OnInventoryUpdated?.Invoke(items);
    }
    
    public void RemoveItem(AbstractItem itemToRemove)
    {
        foreach(var item in items)
        {
            if (item == itemToRemove)
            {
                items[currentItemId] = null;
                OnInventoryUpdated?.Invoke(items);
                return;
            }
        }
    }
}