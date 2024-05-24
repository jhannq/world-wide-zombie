using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrow : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    public static int grenadeAmount = 3;

    public Text grenadesCounterDisplay;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
		{
            if (grenadeAmount > 0)
                ThrowGrenade();
		}
    }

    void ThrowGrenade()
	{
        grenadeAmount--;
        grenadesCounterDisplay.text = "x " + grenadeAmount;

        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
	}
}
