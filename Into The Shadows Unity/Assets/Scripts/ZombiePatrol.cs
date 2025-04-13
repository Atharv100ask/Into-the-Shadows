using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrol : MonoBehaviour
{
    public Transform[] waypoints; // Array to hold the 4 waypoints
    public float patrolSpeed = 2f; // Speed of the zombie when patrolling

    // Added attack variables:
    public Transform player;        
    public float attackRange = 2f;    
    public float attackCooldown = 2f; 

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private float attackTimer = 0f;
    private Animator anim;
    
    public HealthBar status;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); //gets animation components
        agent.speed = patrolSpeed;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position); //start patrolling to the first waypoint
        }
    }

    void Update()
    {
        // Update the attack timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Check if the player is assigned and within the attack range
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                // Switch to attack mode: follow the player
                anim.SetTrigger("Scream"); 
                anim.SetTrigger("Run");
                if (distanceToPlayer <= 2)
                {
                    anim.SetTrigger("handAttack");
                    
                    if (attackTimer <= 0f)
                    {
                        Debug.Log("Zombie attacks the player!");
                        status.TakeDamage(10);
                        attackTimer = attackCooldown;
                    }
                }
                agent.SetDestination(player.position);

                return; 
            }
        }        

        if (agent.remainingDistance <= agent.stoppingDistance)//handles patrol logic
        {
            anim.SetTrigger("Walk");//makes zombies to walk
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;//goes to next waypoint
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
