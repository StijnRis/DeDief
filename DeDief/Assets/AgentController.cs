using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private FieldOfView fov;
    private Animator animator;

    public GameObject target;
    public GameObject weapon;
    
    bool aggregated;
	
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        fov = gameObject.GetComponent<FieldOfView>();
        animator = gameObject.GetComponent<Animator>();

        agent.SetDestination(target.transform.position);
        weapon.SetActive(false);
        aggregated = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        aggregated = fov.canSeePlayer;
        if (Input.GetKeyDown("space"))
        {
            aggregated = !aggregated;
        }

        if (aggregated)
        {
            weapon.SetActive(true);
        } else
        {
            weapon.SetActive(false);
        }
        animator.SetBool("aggregated", aggregated);
        animator.SetFloat("Move", agent.velocity.magnitude);
    }
}
