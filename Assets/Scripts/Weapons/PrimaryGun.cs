using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera cameraDirection;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public static int maxAmmo = 30;
    public static int currentAmmo = 30;
    private float timeToReload = 1.5f;
    private bool isReloading = false;

    public Animator animator;

    public Text displayAmmo;


	private void Start()
	{
        currentAmmo = maxAmmo;
	}

	// Update is called once per frame
	void Update()
    {
        if (isReloading)
		{
            return;
		}

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
		{
            nextTimeToFire = Time.time + 1f / fireRate;

            if (currentAmmo > 0)
			{
                Shoot();
			}
			else
			{
                StartCoroutine(Reload());
                return;
			}
			           
		}

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }
    }

    void Shoot()
	{
        muzzleFlash.Play();

        currentAmmo--;

        displayAmmo.text = currentAmmo + "/" + maxAmmo;

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
    }

    IEnumerator Reload()
	{
        isReloading = true;

        animator.SetBool("Reloading", true);

        displayAmmo.text = "-/" + maxAmmo;

        yield return new WaitForSeconds(timeToReload - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        displayAmmo.text = currentAmmo + "/" + maxAmmo;
        isReloading = false;
    }

	private void OnEnable()
	{
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
}
