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
        if (readyToShoot && !shooting && !reloading && bulletsLeft > 0 && !PlayerInteract.inventoryOpen && Cursor.lockState == CursorLockMode.Locked) {
            Shoot();
        } else if (!PlayerInteract.inventoryOpen && !PlayerInteract.settingsOpen) {
            FindObjectOfType<AudioManager>().Play("PlayerGunEmpty");
        } 
    }

    private void Shoot()
    {
        readyToShoot = false;

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward);
            

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

            if (rayHit.collider.GetComponent<AgentWeapon>() != null)
            {
                AgentController destr = rayHit.collider.GetComponent<AgentWeapon>().agent.GetComponent<AgentController>();
                if (destr.destroyed == false)
                {
                    rayHit.collider.GetComponent<AgentWeapon>().agent.GetComponent<AgentController>().Destroy();
                    GameObject.FindGameObjectWithTag("Office").GetComponent<SceneController>().SpawnAgent();
                    office.SpawnAgent();
                }
            }
        }

        FindObjectOfType<AudioManager>().Play("PlayerShoot");

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
