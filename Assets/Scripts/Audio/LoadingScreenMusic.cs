using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenMusic : MonoBehaviour
{
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
}
