using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private FieldOfView fov;
    private Animator animator;
    private GameObject target;
    public GameObject weapon;
    
    bool aggregated;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.canSeePlayer)
            setTarget(target);
        aggregated = fov.canSeePlayer;
        if (Input.GetKeyDown("space"))
        {
            aggregated = !aggregated;
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
        if (agent.velocity.magnitude == 0 && !aggregated)
        {
            StartCoroutine("LookAround");
        }
        else
        {
            StopCoroutine("LookAround");
        }

        if(agent.remainingDistance < agent.stoppingDistance) 
        {
            agent.updateRotation = false;
            RotateTowards(target.transform);
        }
        else 
        {
            agent.updateRotation = true;
        }
        
        animator.SetFloat("Move", agent.velocity.magnitude);
    }

    public void setTarget(GameObject target)
    {
        agent.SetDestination(target.transform.position);
    }

    private void RotateTowards(Transform target) 
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    IEnumerator LookAround() 
    {
        float lookPeriod = 5f; // change look every 5 seconds
        float maxRotationSpeed = 90f; // turn no faster than 90 degrees per second

        while(true) 
        {
            float timeToNextLook = lookPeriod;

            while (timeToNextLook > 0) {
                // Get random offset from forward
                float targetYRotation = Random.Range(-90f, 90f);

                // calculate target rotation
                Quaternion targetRotation = Quaternion.LookRotation(transform.forward) 
                                        * Quaternion.Euler(0,targetYRotation,0);

                // rotate towards target limited by speed
                Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);

                timeToNextLook -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
