using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    Animator animator;
    public static bool knifed = false;

    public float damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
            animator.Play("KnifeAnimation", -1, 0f);
            //FindObjectOfType<AudioManager>().Play("Knife_Swing");
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
            Enemy target = other.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
	}
}
