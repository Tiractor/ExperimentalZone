using UnityEngine;

public class NPCAI : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Chase,
        ReturnToStart,
        OutOfSight
    }

    [SerializeField] private Transform target;
    [SerializeField] private float sightRange;
    [SerializeField] private float chaseRange;
    [SerializeField] private float speedPatrol;
    [SerializeField] private float speedChase;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private State currentState;
    private int currentWaypointIndex = 0;
    private Vector3 startPos;
    private Vector3 currentDestination;

    private void Start()
    {
        currentState = State.Patrol;
        startPos = transform.position;
        currentDestination = waypoints[currentWaypointIndex].position;
        target = FindObjectOfType<PlayerController>().transform; // Example of finding the player
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

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
        if (distanceToTarget > sightRange && currentState != State.Patrol)
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

        MoveTowards(currentDestination);
    }

    private void Patrol()
    {
        if (waypoints.Length == 0)
            return;
        
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        currentDestination = waypoints[currentWaypointIndex].position;
    }

    private void Chase()
    {
        currentDestination = target.position;
    }

    private void ReturnToStart()
    {
        currentDestination = startPos;
        if (Vector3.Distance(transform.position, startPos) <= 0.1f)
        {
            currentState = State.Patrol;
        }
    }

    private void OutOfSight()
    {
        currentState = State.ReturnToStart;
    }

    private void MoveTowards(Vector3 destination)
    {
        // Move the NPC towards the destination
        float step = (currentState == State.Patrol ? speedPatrol : speedChase) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
    }
}