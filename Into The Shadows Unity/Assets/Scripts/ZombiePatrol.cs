using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrol : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the 4 waypoints
    public float patrolSpeed = 2f; // Speed of the zombie when patrolling

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position); // Start patrolling to the first waypoint
        }
    }

    void Update()
    {
        // Patrol logic
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Move to the next waypoint when the zombie reaches the current one
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
