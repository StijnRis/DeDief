using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public GameObject eyes;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool log = false;

    public bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        //Debug.Log("shit is happenings");
        Vector3 offset = new Vector3(0, 10, 0);
        Collider[] rangeChecks = Physics.OverlapCapsule(eyes.transform.position - offset, eyes.transform.position + offset, radius, targetMask);
        // Gizmos.DrawWireSphere(eyes.transform.position, radius);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = eyes.transform.position - target.position;
            Vector3 eyeDirection = new Vector3(eyes.transform.forward.x, 0, eyes.transform.forward.z);

            if (log)
            {
                Debug.Log("angle between " + eyeDirection + " and " + directionToTarget + " gives: " + Vector3.Angle(eyeDirection, directionToTarget));
            }
            
            if (Vector3.Angle(eyeDirection, -directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(eyes.transform.position, target.position);

                if (!Physics.Raycast(eyes.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}