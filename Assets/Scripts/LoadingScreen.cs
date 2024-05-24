using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    private void Start()
	{
		StartCoroutine(FadeLoadingScreen());
	}

	IEnumerator FadeLoadingScreen()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

		yield return new WaitForSeconds(7);

		while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime / 3;
			yield return null;
		}

		canvasGroup.interactable = false;

		if (canvasGroup.alpha == 0)
		{
			SceneManager.LoadScene("Story");
		}

		yield return null;
	}
}
