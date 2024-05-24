using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
	private Vector3 initialPosition;
	private Quaternion initialRotation;

	private void Start()
	{
		initialPosition = gameObject.transform.position;
		initialRotation = gameObject.transform.rotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			gameObject.transform.position = new Vector3(-50, gameObject.transform.position.y, -50);
			StartCoroutine(CloseDoor(2f));
		}
	}

	private IEnumerator CloseDoor(float sec)
	{
		yield return new WaitForSeconds(sec);
		gameObject.transform.position = initialPosition;
		gameObject.transform.rotation = initialRotation;
	}
}
