using UnityEngine;
using UnityEngine.UI;

public class InventoryCellUI : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Image outlineImage;

    public void UpdateCellItem(Sprite sprite)
    {
        slotImage.sprite = sprite;
    }

    public void SelectCell()
    {
        outlineImage.enabled = true;
    }
    
    public void DeselectCell()
    {
        outlineImage.enabled = false;
    }
}