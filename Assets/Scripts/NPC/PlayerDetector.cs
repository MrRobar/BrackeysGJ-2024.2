using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerDetector : MonoBehaviour
{
    public event Action<Player> OnPlayerDetected;
    public event Action OnPlayerLost;
    
    private SphereCollider collider;
    
    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
    }
    
    public void SetRadius(float radius) => collider.radius = radius;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            OnPlayerDetected?.Invoke(player);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            OnPlayerLost?.Invoke();
        }
    }
}
