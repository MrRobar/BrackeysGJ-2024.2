using UnityEngine;
using UnityEngine.Serialization;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] private AbstractItem heldAbstractItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Check();
        }
    }
    
    private void Check()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance, interactionLayerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (heldAbstractItem != null)
                {
                    interactable.InteractWith(heldAbstractItem);
                }
                else
                {
                    interactable.Interact();
                }
            }
        }
    }
}