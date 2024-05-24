using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreetingsUI : MonoBehaviour
{
	Image image;
	public Text text1;
	public Text text2;
	Color c1;
	Color c2;
	Color c3;

	float startTimer = 0;
	public float duration = 10f;

	void Start()
	{
		image = GetComponent<Image>();
		c1 = image.color;
		c1.a = .5f;
		image.color = c1;

		text1 = GameObject.Find("Welcome").GetComponent<Text>();
		c2 = text1.color;
		c2.a = 1f;
		text1.color = c2;

		text2 = GameObject.Find("About").GetComponent<Text>();
		c3 = text2.color;
		c3.a = 1f;
		text2.color = c3;
	}

	private void Update()
	{
		startTimer += Time.deltaTime;
		//Debug.Log(startTimer);
		if (startTimer > duration)
		{
			c1.a -= .5f / (duration * 5);
			image.color = c1;

			c2.a -= 1f / (duration * 5);
			text1.color = c2;

			c3.a -= 1f / (duration * 5);
			text2.color = c3;
		}
	}
}
