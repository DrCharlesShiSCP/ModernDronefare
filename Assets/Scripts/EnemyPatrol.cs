using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolRange = 10f;
    public float waitTime = 2f;
    private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        SetNewPatrolPoint();
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !isWaiting)
        {
            isWaiting = true;
            waitTimer = 0f;
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                SetNewPatrolPoint();
            }
        }
    }

    void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection += initialPosition;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}
