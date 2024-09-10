using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private LayerMask interactionLayerMask;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Check();
        }
    }
    
    private void Check()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayerMask))
        {
            Debug.Log(hit.collider.name);
            hit.collider.GetComponent<IInteractable>().Interact();
        }
    }
}