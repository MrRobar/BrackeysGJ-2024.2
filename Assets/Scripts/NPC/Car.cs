using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private ObjectDetector objectDetector;
    [SerializeField] private float speed;
    private Vector3 originPosition;
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
        originSpeed = speed;
        originPosition = transform.position;
    }

    private void Stop(string tag)
    {
        if (tag == "Edge")
        {
            transform.position = originPosition;
            return;
        }

        if (tag != "Detector")
        {
            speed = 0f;
        }
    }

    private void ContinueRoute()
    {
        speed = originSpeed;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime), Space.Self);
    }
}