using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Image[] slotImages;

    private void OnEnable()
    {
        inventory.OnInventoryUpdated += UpdateUI;
        inventory.OnSelectedSlot += HighlightSelectedSlot;
    }

    private void OnDisable()
    {
        inventory.OnInventoryUpdated -= UpdateUI;
        inventory.OnSelectedSlot -= HighlightSelectedSlot;
    }

    private void UpdateUI(AbstractItem[] items)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (items[i] != null)
            {
                slotImages[i].sprite = items[i].GetIcon();
            }
            else
            {
                slotImages[i].sprite = null;
            }
        }
    }

    private void HighlightSelectedSlot(int selectedSlot)
    {
        // Логика для подсвечивания выбранного слота
    }
}