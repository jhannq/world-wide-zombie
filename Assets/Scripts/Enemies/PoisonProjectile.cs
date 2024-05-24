using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
	private Transform player;

	public float speed = 5f;

	private Vector3 target;


	private void Start() {}

	private void Update()
	{
		gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}
