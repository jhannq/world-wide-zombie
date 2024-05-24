using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera cameraDirection;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public static int ammoInMagazine = 30;
    public static int ammoUsed = 0;
    public static int ammoPerRound = 10;
    public static int currentAmmo = 0;
    private float timeToReload = 1.5f;
    private bool isReloading = false;

    public Animator animator;

    public Text displayAmmo;


    private void Start()
    {
        currentAmmo = ammoPerRound;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        displayAmmo.text = currentAmmo + "/" + ammoInMagazine;

        if (Input.GetMouseButtonDown(0) && Time.time >= fireRate)
        {
            if (currentAmmo > 0)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                StartCoroutine(Reload());
                return;
            }
        }


        // Aiming animation
        if (Input.GetMouseButton(1))
        {
            animator.SetBool("Aiming_Pistol", true);

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("AimShoot_Pistol", true);
                animator.SetBool("Shooting_Pistol", false);

                if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
                {
                    animator.SetBool("AimShoot_Pistol", false);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("AimShoot_Pistol", false);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("Aiming_Pistol", false);

        }

        // Walking animation
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Walking_Pistol", true);

            // Walking and Aiming
            if (Input.GetMouseButton(1))
            {
                animator.SetBool("Walking_Pistol", false);
            }
            // Walking and Shooting
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Walking_Pistol", false);
            }
            // Walking and Reloading
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetBool("Walking_Pistol", false);
            }
        }
        else
        {
            animator.SetBool("Walking_Pistol", false);
        }


        // Running animation
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Running_Pistol", true);

            // Running and Aiming
            if (Input.GetMouseButton(1))
            {
                animator.SetBool("Running_Pistol", false);
            }
            // Running and Shooting
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("Running_Pistol", false);
            }
            // Running and Reloading
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetBool("Running_Pistol", false);
            }
        }
        else
        {
            animator.SetBool("Running_Pistol", false);
        }


        // Reloading animation
        if (currentAmmo <= 0 && ammoInMagazine > 0)
        {
            animator.SetBool("Aiming_Pistol", false);
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo != ammoPerRound && ammoInMagazine != 0)
            {
                animator.SetBool("Aiming_Pistol", false);
                StartCoroutine(Reload());
                return;
            }
        }
    }

    IEnumerator Shoot()
    {
        // sound
        FindObjectOfType<AudioManager>().Play("pistol_shooting");

        animator.SetBool("Shooting_Pistol", true);
        muzzleFlash.Play();

        currentAmmo--;
        ammoUsed++;

        displayAmmo.text = currentAmmo + "/" + ammoInMagazine;

        RaycastHit hit;
        if (Physics.Raycast(cameraDirection.transform.position, cameraDirection.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, .5f);
        }
        yield return new WaitForSeconds(0f);
        animator.SetBool("Shooting_Pistol", false);
    }

    IEnumerator Reload()
    {
        FindObjectOfType<AudioManager>().Play("pistol_reloading");
        isReloading = true;

        animator.SetBool("Reloading", true);

        displayAmmo.text = "-/" + ammoInMagazine;

        yield return new WaitForSeconds(timeToReload - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        if (ammoInMagazine > 0 && currentAmmo < ammoPerRound)
        {
            if (ammoInMagazine - ammoPerRound >= 0)
            {
                ammoInMagazine -= ammoPerRound - currentAmmo;
                currentAmmo = ammoPerRound;
            }
            else if (ammoInMagazine - ammoPerRound < 0)
            {
                //float valueForBoth = ammoPerRound - bulletsInTheGun;
                if (ammoInMagazine - ammoUsed < 0)
                {
                    currentAmmo += ammoInMagazine;
                    ammoInMagazine = 0;
                }
                else
                {
                    ammoInMagazine -= ammoUsed;
                    currentAmmo += ammoUsed;
                }
            }
        }

        ammoUsed = 0;
        displayAmmo.text = currentAmmo + "/" + ammoInMagazine;
        isReloading = false;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
}
