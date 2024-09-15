using UnityEngine;
using UnityEngine.AI;

public class SpawnNPCs : MonoBehaviour
{
    public HumanNPC[] humanPrefabs; // Массив префабов для рандомного выбора
    [SerializeField] private Transform holder;
    public float spawnHeightLimit = 10f; // Лимит по высоте
    public float spawnRadius = 20f; // Радиус поиска случайной точки
    public int maxAttempts = 10; // Максимум попыток найти подходящую точку

    [ContextMenu("SpawnNPCs")]
    public void SpawnObjects()
    {
        for (int i = 0; i < 50; i++) // Спавним 5 объектов для примера
        {
            TrySpawnObject();
        }
    }

    private void TrySpawnObject()
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // Генерация случайной позиции в радиусе
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;

            // Проверка на наличие точки на NavMesh в области Pedestrian
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, spawnRadius, NavMesh.AllAreas))
            {
                // Проверка высоты
                if (hit.position.y <= spawnHeightLimit)
                {
                    // Спавн объекта
                    var human = Instantiate(humanPrefabs[Random.Range(0, humanPrefabs.Length)], hit.position, Quaternion.identity);
                    human.transform.SetParent(holder);
                    Debug.Log($"Object spawned at {hit.position}");
                    return;
                }
            }
        }

        Debug.Log("No suitable point found within max attempts.");
    }
}