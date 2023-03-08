using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float fireRate, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //reference
    public Camera fpsCam;
    public Transform attachPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public ToolTip toolTip;

    public TextMeshProUGUI ammo;
    public SceneController office;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        reloading = false;
        shooting = false;
        office = GameObject.FindGameObjectWithTag("Office").GetComponent<SceneController>();
    }

    private void Update()
    {
        ammo.SetText(bulletsLeft + " / " + magazineSize);
    }

    public void Fire()
    {
        if (readyToShoot && !shooting && !reloading && bulletsLeft > 0 && !PlayerInteract.inventoryOpen) {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            

            if (rayHit.collider.CompareTag("Agent"))
            {
                AgentController destr = rayHit.collider.GetComponent<AgentController>();
                if (destr.destroyed == false)
                {
                    rayHit.collider.GetComponent<AgentController>().Destroy();
                    GameObject.FindGameObjectWithTag("Office").GetComponent<SceneController>().SpawnAgent();
                    office.SpawnAgent();
                }
            }
        }

        bulletsLeft--;
        Invoke("ResetShot", fireRate);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
