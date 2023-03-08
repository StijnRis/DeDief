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
    public float agentDamage;
    public float shootCooldownSeconds;
    public GameObject seenAlertPrefab;

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
	
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        fov = gameObject.GetComponent<FieldOfView>();
        animator = gameObject.GetComponent<Animator>();
        // seenAlert = GameObject.FindGameObjectWithTag("SeenAlert").GetComponent<SeenAlert>();

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
            SeenAlert seenAlert = Instantiate(seenAlertPrefab).GetComponent<SeenAlert>();
            InventoryController inventoryController = Camera.main.GetComponent<InventoryController>();
            seenAlert.Init("by an agent", inventoryController);
            followPlayer = true;
            followTime = 0;

            //Rotate towards player
            if (agent.velocity.magnitude < 0.1f)
            {
                RotateToPlayer();
            }
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

            //Shoot every 2 seconds
            if (fov.canSeePlayer && lastShootingTime > shootCooldownSeconds && weapon != null)
            {
                lastShootingTime = 0;
                Shoot();
            }
            lastShootingTime += Time.deltaTime;
        }
        else
        {
            if(agent.remainingDistance < agent.stoppingDistance)
            {    
                NextWaypoint();
            }
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

    void RotateToPlayer()
    {
        int damping = 2;
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void Shoot()
    {
        target.GetComponent<PlayerHealth>().TakeDamage(agentDamage);
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            Destroy(agentObject);
            destroyed = true;
        }
    }
}
