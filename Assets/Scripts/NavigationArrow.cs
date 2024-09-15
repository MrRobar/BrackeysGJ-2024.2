using UnityEngine;

public class NavigationArrow : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform bedTransform;
    private Vector3 target;
    private Package nearestPackage;

    private void Update()
    {
        if (player.Inventory.CountItemsOfType<Package>() < Work.Orders.Length)
        {
            if (!nearestPackage || nearestPackage.InInventory)
            {
                var packages = FindObjectsOfType<Package>();
                nearestPackage = packages[0];
                var nearestDistance = Vector3.Distance(transform.position, nearestPackage.transform.position);
                foreach (var package in packages)
                {
                    if (package.enabled && Vector3.Distance(transform.position, package.transform.position) < nearestDistance)
                    {
                        nearestPackage = package;
                        nearestDistance = Vector3.Distance(transform.position, nearestPackage.transform.position);
                    }
                }
            }

            target = nearestPackage.transform.position;
        }
        else if (Work.Orders.Length > 0)
        {
            target = Work.Orders[0].DeliveryTarget.transform.position;
        }
        else
        {
            target = bedTransform.position;
        }
        
        transform.LookAt(target);
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0);
    }
}
