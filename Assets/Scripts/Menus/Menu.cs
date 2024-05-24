using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Tutorial()
	{
		SceneManager.LoadScene("Tutorial");
	}

	public void Play()
	{
		SceneManager.LoadScene("Loading Screen");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
