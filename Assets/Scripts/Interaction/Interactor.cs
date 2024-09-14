using UnityEngine;

[RequireComponent(typeof(Player))]
public class Interactor : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private LayerMask interactionLayerMask;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }
    
    private void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance, interactionLayerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                var heldAbstractItem = player.Inventory.CurrentItem;
                
                if (heldAbstractItem != null)
                {
                    interactable.InteractWith(heldAbstractItem, player);
                }
                else
                {
                    interactable.Interact(player);
                }
            }
        }
    }
}