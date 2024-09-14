using System;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public event Action<string> OnObjectDetected;
    public event Action OnObjectLost;

    private void OnTriggerEnter(Collider other)
    {
        OnObjectDetected?.Invoke(other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        OnObjectLost?.Invoke();
    }
}