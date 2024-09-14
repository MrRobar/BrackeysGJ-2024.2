using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] private ObjectDetector objectDetector;
    [SerializeField] private PathFollow pathFollow;
    [SerializeField] private NavMeshAgent agent;
    private float originSpeed;

    private void OnEnable()
    {
        objectDetector.OnObjectDetected += Stop;
        objectDetector.OnObjectLost += ContinueRoute;
    }

    private void OnDisable()
    {
        objectDetector.OnObjectDetected -= Stop;
        objectDetector.OnObjectLost -= ContinueRoute;
    }

    private void Awake()
    {
        originSpeed = agent.speed;
    }

    private void Stop()
    {
        agent.speed = 0f;
    }

    private void ContinueRoute()
    {
        agent.speed = originSpeed;
    }
}
