using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject effect;

    public float delay = 3f;
    public float radius = 5f;
    public float explosion = 500f;

    private float damage = 100f;

    float countdown;
    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !exploded)
		{
            Explode();
            exploded = true;
		}
    }

    void Explode()
	{
        Instantiate(effect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
		{
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            Enemy enemy = nearbyObject.GetComponent<Enemy>();

            if (rb != null)
			{
                rb.AddExplosionForce(explosion, transform.position, radius);
			}

            if (enemy != null)
			{
                enemy.TakeDamage(damage);
            }
		}

        Destroy(gameObject);
	}
}
