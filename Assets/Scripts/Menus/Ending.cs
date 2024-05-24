using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject sequence1;
    public GameObject sequence2;
    public GameObject sequence3;

    public float countdownS1 = 11;
    public float countdownS2 = 11;

    // Start is called before the first frame update
    void Start()
    {
        sequence1.SetActive(true);
        sequence2.SetActive(false);
        sequence3.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        countdownS1 -= Time.deltaTime;
        if (countdownS1 < 0)
		{
            sequence1.SetActive(false);
            sequence2.SetActive(true);

            countdownS2 -= Time.deltaTime;
            if (countdownS2 < 0)
			{
                sequence2.SetActive(false);
                sequence3.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void BackToMenu()
	{
        SceneManager.LoadScene("Menu");
	}
}
