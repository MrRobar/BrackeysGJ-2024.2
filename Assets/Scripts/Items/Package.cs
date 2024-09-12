using UnityEngine;

public class Package : AbstractItem
{
    public override void Collect()
    {
        base.Collect();
        Debug.Log("Picked up package...");
    }
}
