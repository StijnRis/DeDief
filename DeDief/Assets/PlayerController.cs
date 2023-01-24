using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Animator animator;
    public GameObject weapon;
    bool aggregated;
	
    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(target.transform.position);
        weapon.SetActive(false);
        aggregated = false;
    }

    // Update is called once per frame
    void Update()
    {
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
