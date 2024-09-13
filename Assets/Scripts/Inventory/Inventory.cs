using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AbstractItem[] items = new AbstractItem[5];
    [SerializeField] private int currentItemId = 0;

    public event Action<int> OnSelectedSlot;
    public event Action<AbstractItem[]> OnInventoryUpdated;

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
        if (scroll > 0f)
        {
            SelectNextSlot();
        }
        else if (scroll < 0f)
        {
            SelectPreviousSlot();
        }
    }

    private void SelectSlot(int slotId)
    {
        if (slotId == currentItemId)
        {
            return;
        }

        if (slotId >= 0 && slotId < items.Length)
        {
            currentItemId = slotId;
            OnSelectedSlot?.Invoke(currentItemId);
            // Debug.Log($"Selected slot: {currentItemId}");
        }
    }

    private void SelectNextSlot()
    {
        currentItemId = (currentItemId + 1) % items.Length;
        OnSelectedSlot?.Invoke(currentItemId);
        // Debug.Log($"Selected next slot: {currentItemId}");
    }

    private void SelectPreviousSlot()
    {
        currentItemId = (currentItemId - 1 + items.Length) % items.Length;
        OnSelectedSlot?.Invoke(currentItemId);
        //  Debug.Log($"Selected previous slot: {currentItemId}");
    }

    public void AddItem(AbstractItem item)
    {
        if (items[currentItemId] != null)
        {
            DropCurrentItem();
        }

        items[currentItemId] = item;
        OnInventoryUpdated?.Invoke(items);
    }

    public void DropCurrentItem()
    {
        items[currentItemId].Drop();
        items[currentItemId] = null;
        OnInventoryUpdated?.Invoke(items);
    }
}