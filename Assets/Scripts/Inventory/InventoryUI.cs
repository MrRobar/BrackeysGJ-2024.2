using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private InventoryCellUI[] cells;
    private int lastSelected = -1;

    private void Start()
    {
        HighlightSelectedSlot(0);
    }

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
        for (int i = 0; i < cells.Length; i++)
        {
            if (items[i] != null)
            {
                cells[i].UpdateCellItem(items[i].Icon);
            }
            else
            {
                cells[i].UpdateCellItem(emptySprite);
            }
        }
    }

    private void HighlightSelectedSlot(int selectedID)
    {
        cells[selectedID].SelectCell();
        if (lastSelected >= 0)
        {
            cells[lastSelected].DeselectCell();
        }

        lastSelected = selectedID;
    }
}