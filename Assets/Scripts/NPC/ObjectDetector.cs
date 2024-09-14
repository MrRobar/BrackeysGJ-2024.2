using System;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public event Action OnObjectDetected;
    public event Action OnObjectLost;

    private void OnTriggerEnter(Collider other)
    {
        OnObjectDetected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnObjectLost?.Invoke();
    }
}