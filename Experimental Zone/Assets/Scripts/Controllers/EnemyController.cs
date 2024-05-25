using UnityEngine;
using UnityEngine.AI; // Äëÿ NavMeshAgent

[RequireComponent(typeof(NavMeshAgent))]
public class NPCAI : Controller
{
    private enum State
    {
        Patrol,
        Chase,
        ReturnToStart,
        OutOfSight
    }

    [SerializeField] private PlayerController target;
    [SerializeField] private float sightRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float speedPatrol;
    [SerializeField] private float speedChase;
    [SerializeField] private Transform[] waypoints;
    private State currentState;
    private int currentWaypointIndex = 0;
    private Vector3 startPos;
    private NavMeshAgent agent;

    private void Start()
    {
        currentState = State.Patrol;
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        target = PlayerController.Player;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.ReturnToStart:
                ReturnToStart();
                break;
            case State.OutOfSight:
                OutOfSight();
                break;
        }

        // Update the state
        if (distanceToTarget > sightRange)
        {
            currentState = State.OutOfSight;
        }
        else if (distanceToTarget < sightRange && distanceToTarget > chaseRange)
        {
            currentState = State.Chase;
        }
        else if (distanceToTarget <= chaseRange)
        {
            // If within attack range, you might set the state to Attack or PrepareAttack here
        }
    }

    private void Patrol()
    {
        if (waypoints.Length == 0)
            return;

        agent.speed = speedPatrol;
        if (agent.destination != waypoints[currentWaypointIndex].position)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void Chase()
    {
        agent.speed = speedChase;
        agent.SetDestination(target.transform.position);
    }

    private void ReturnToStart()
    {
        agent.SetDestination(startPos);
        if (Vector3.Distance(transform.position, startPos) <= agent.stoppingDistance)
        {
            currentState = State.Patrol;
        }
    }

    private void OutOfSight()
    {
        // Reset the agent's path if so desired
        agent.ResetPath();
        currentState = State.ReturnToStart;
    }

    // Additional code omitted
}