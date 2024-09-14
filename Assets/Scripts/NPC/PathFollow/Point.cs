using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Point nextPoint;
    public Point NextPoint => nextPoint;
}