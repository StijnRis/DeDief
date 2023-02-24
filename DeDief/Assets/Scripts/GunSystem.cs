
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

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        reloading = false;
        shooting = false;
    }

    private void Update()
    {
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range);
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            toolTip.Init("yes", "yes");
        }
    }

    public void Fire()
    {
        if (readyToShoot && !shooting && !reloading && bulletsLeft > 0) {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Debug.Log("test");

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log("test2");
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Agent"))
            {
                Debug.Log("test3");
            }

            if (rayHit.collider.GetComponent<AgentController>() != null)
            {
                Debug.Log("test4");
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
