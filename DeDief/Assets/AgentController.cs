using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public GameObject weapon;
    public Vector3 currentWaypoint;
    public GameObject agentObject;
    public bool destroyed = false;

    private NavMeshAgent agent;
    private FieldOfView fov;
    private Animator animator;
    private GameObject target;

    private int waypointIndex = 0;
    private float followTime = 0;
    private float lastShootingTime = 0;
    
    bool aggregated;
    bool followPlayer;
    // bool idling;

    float lookPeriod = 5f;
	
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        fov = gameObject.GetComponent<FieldOfView>();
        animator = gameObject.GetComponent<Animator>();

        // setTarget(target);
        target = GameObject.FindGameObjectWithTag("Player");
        weapon.SetActive(false);
        weapon.GetComponent<AgentWeapon>().agent = gameObject;
        aggregated = false;
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        aggregated = fov.canSeePlayer;
        if (fov.canSeePlayer)
        {
            followPlayer = true;
            followTime = 0;
        }

        if (followPlayer)
        {
            aggregated = true;
            setTarget(target.transform.position);
            if (followTime < 5f)
            {
                followTime += Time.deltaTime;
            }
            else
            {
                followPlayer = false;
            }

            if (fov.canSeePlayer && lastShootingTime > 2f && weapon != null)
            {
                lastShootingTime = 0;
                Shoot();
            }
            lastShootingTime += Time.deltaTime;
        }
        else
        {
            setTarget(currentWaypoint);
        }

        
        if (weapon != null) {
            if (aggregated)
            {
                weapon.SetActive(true);
            } else
            {
                weapon.SetActive(false);
            }
            animator.SetBool("aggregated", aggregated);
        }
        else
        {
            animator.SetBool("aggregated", false);
        }

        if(agent.remainingDistance < agent.stoppingDistance) 
        {
            // agent.updateRotation = false;
            // RotateTowards(target.transform);

            NextWaypoint();
        }
        else 
        {
            agent.updateRotation = true;
        }
        
        animator.SetFloat("Move", agent.velocity.magnitude);
    }

    public void setTarget(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void NextWaypoint()
    {
        Waypoint[] waypoints = GetComponents<Waypoint>();
        if (waypointIndex + 1 >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        else
        {
            waypointIndex++;
        }
        this.currentWaypoint = waypoints[waypointIndex].location;
    }

    void Shoot()
    {
        target.GetComponent<PlayerHealth>().TakeDamage(20f);
    }

    private void RotateTowards(Transform target) 
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            DestroyObject(agentObject);
            destroyed = true;
        }
    }
}
