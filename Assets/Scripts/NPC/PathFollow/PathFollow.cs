using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PathFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform routes;
    [SerializeField] private int distanceBetweenPoints = 5;
    [SerializeField] private List<Transform> pathPoints = new List<Transform>(); // Список промежуточных точек
    private Transform currentPoint;
    private int currentPointIndex = -1; // Индекс текущей точки, к которой идем
    private Vector3 lastPointPosition;

    [ContextMenu("GeneratePath")]
    public void GeneratePath()
    {
        pathPoints.Clear(); // Очищаем старые точки
        var pathHolder = new GameObject(transform.name);
        pathHolder.transform.SetParent(routes);
        var startPos = transform.position;
        var endPos = endPoint.transform.position;
        var distance = Vector3.Distance(startPos, endPos);
        var amount = Mathf.RoundToInt(distance / distanceBetweenPoints); // Количество точек на пути

        for (int i = 1; i < amount; i++)
        {
            // Интерполируем точки между началом и концом пути
            var pointPosition = Vector3.Lerp(startPos, endPos, (float)i / amount);
            var newPoint = Instantiate(endPoint, pointPosition, Quaternion.identity);
            newPoint.SetParent(pathHolder.transform);
            pathPoints.Add(newPoint); // Добавляем в список промежуточных точек
        }

        pathPoints.Add(endPoint);
        endPoint.SetParent(pathHolder.transform);

        // Сбрасываем текущий индекс точки
        currentPointIndex = -1;
    }

    private void Update()
    {
        if (pathPoints.Count == 0) return; // Если нет точек, ничего не делаем

        // Если достигли текущей точки, переходим к следующей
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex++;
            if (currentPointIndex < pathPoints.Count)
            {
                currentPoint = pathPoints[currentPointIndex];
                lastPointPosition = currentPoint.transform.position; // Сохраняем позицию текущей точки
                agent.SetDestination(lastPointPosition);
            }
        }

        // Проверяем, сдвинулась ли текущая точка
        if (currentPointIndex >= 0 && currentPointIndex < pathPoints.Count)
        {
            currentPoint = pathPoints[currentPointIndex];
            if (currentPoint != null && currentPoint.transform.position != lastPointPosition)
            {
                // Если позиция точки изменилась, обновляем цель
                lastPointPosition = currentPoint.transform.position;
                agent.SetDestination(lastPointPosition);
            }
        }
    }
}