using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("x", agent.velocity.x);
        //animator.SetFloat("y", agent.velocity.y);
        animator.SetFloat("Move", agent.velocity.magnitude);
    }
}
