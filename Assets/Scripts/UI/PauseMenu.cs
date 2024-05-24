using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
	private bool PlayerUIHidden = false;


	public GameObject PlayerUI;
	public GameObject InstructionsUI;
	public GameObject PauseMenuUI;
	public GameObject OptionsMenuUI;

	public GameObject player;
    //public GameObject ControlsMenuUI;



	void Start(){
		PauseMenuUI.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (OptionsMenuUI.activeSelf)
			{
				PlayerUIHidden = true;
				HideControlsMenu();
				return;
			}

			if (GameIsPaused)
			{
				PlayerUIHidden = false;
				Resume();
			}
			else
			{
				PlayerUIHidden = true;
				Pause();
			}
		}

		if (PlayerUIHidden)
		{
			PlayerUI.SetActive(false);
			InstructionsUI.SetActive(false);
		}
		else
		{
			PlayerUI.SetActive(true);
			InstructionsUI.SetActive(true);
		}
    }


	public void Resume()
	{
		Time.timeScale = 1f;

		player.GetComponent<PlayerMovementTest>().enabled = true;
		player.GetComponent<PlayerLookTest>().enabled = true;
		player.transform.Find("Main Camera").transform.Find("WeaponHolder").gameObject.SetActive(true);

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		PauseMenuUI.SetActive(false);
		PlayerUIHidden = false;

		GameIsPaused = false;
	}

	void Pause()
	{
		Time.timeScale = 0f;

		player.GetComponent<PlayerMovementTest>().enabled = false;
		player.GetComponent<PlayerLookTest>().enabled = false;
		player.transform.Find("Main Camera").transform.Find("WeaponHolder").gameObject.SetActive(false);

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

		PauseMenuUI.SetActive(true);
		PlayerUIHidden = true;

		GameIsPaused = true;
	}

	void HideControlsMenu()
	{
		OptionsMenuUI.SetActive(false);
		PauseMenuUI.SetActive(true);

		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Options()
	{
		SceneManager.LoadScene("Menu");
	}

	public void LoadMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
