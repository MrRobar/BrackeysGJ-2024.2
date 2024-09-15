using UnityEngine;
using UnityEngine.AI;

public class HumanNPC : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 pointToMove; // Точка, куда пойдет NPC
    private Vector3 originPoint; // Начальная позиция NPC
    private NavMeshAgent agent; // NavMesh агент для перемещения
    private bool isWalking; // Флаг, ходит ли NPC
    private bool isInfected; 

    [SerializeField] private float stoppingDistance = 0.5f; // Минимальная дистанция до точки, чтобы считать, что NPC пришел

    private void Start()
    {
        originPoint = transform.position; // Сохраняем начальную позицию NPC
        agent = GetComponent<NavMeshAgent>();
        Init();
    }

    public void Init()
    {
        // Случайным образом выбираем состояние: стоит, ходит или заражен
        float randomValue = Random.value;

        if (randomValue < 0.2f)
        {
            // Состояние: стоит на месте (20% вероятность)
            isWalking = false;
            isInfected = false;
            animator.SetBool("Walk", false);
            animator.SetBool("Pain", false);
            agent.SetDestination(transform.position); // Оставляем на месте
        }
        else if (randomValue < 0.4f)
        {
            // Состояние: заражен (20% вероятность)
            isWalking = false;
            isInfected = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Pain", true);
            agent.SetDestination(transform.position); // Оставляем на месте
        }
        else
        {
            // Состояние: ходит (60% вероятность)
            isWalking = true;
            isInfected = false;
            animator.SetBool("Walk", true);
            animator.SetBool("Pain", false);

            // Случайно выбираем точку на NavMesh
            pointToMove = GetRandomNavMeshPoint();
            agent.SetDestination(pointToMove);
        }
    }

    private void Update()
    {
        // Если NPC должен ходить
        if (isWalking)
        {
            // Проверяем, дошел ли NPC до цели
            if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
            {
                // Если дошел до точки, меняем цель на начальную позицию или на новую точку
                if (agent.destination == pointToMove)
                {
                    agent.SetDestination(originPoint); // Идет к начальной точке
                }
                else
                {
                    agent.SetDestination(pointToMove); // Идет к случайной точке
                }
            }
        }
    }

    // Метод для получения случайной точки на NavMesh в радиусе от текущей позиции
    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100f; // Генерация случайного направления
        randomDirection += transform.position; // Смещаем относительно текущей позиции

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 100f, NavMesh.AllAreas))
        {
            return hit.position; // Возвращаем точку, если она найдена на NavMesh
        }

        return transform.position; // Если не удалось найти точку, возвращаем текущую позицию
    }
}
